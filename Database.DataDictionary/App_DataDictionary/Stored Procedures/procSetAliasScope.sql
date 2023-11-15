CREATE PROCEDURE [App_DataDictionary].[procSetAliasScope]
		@Data [App_DataDictionary].[typeApplicationScope] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on AliasScope.
*/

-- Transaction Handling
Declare	@TRN_IsNewTran Bit = 0 -- Indicates that the stored procedure started the transaction. Used to handle nested Transactions

Begin Try
	-- Begin Transaction
	If @@TranCount = 0
	  Begin -- Not in a nested/distributed transaction, need to start a transaction
		Begin Transaction
		Select	@TRN_IsNewTran = 1
	  End; -- Begin Transaction

	-- Performance Note:
	-- This works on a small data set but as the data set gets large, issues start coming up.
	-- The main issue is the joins on NVarChar(Max).
	-- This can be addressed by breaking the query up into a set of indexed Table Variables.
	-- Binary_CheckSum can be used to improve performance of the indexes.
	-- Another approach is to create a function that parses exactly one ScopeName and returns the parent/child names.
	;With [Parse] As (
		Select	Convert(NVarChar(Max), [ScopedName]) As [ScopeParentName],
				Convert(NVarChar(Max), Null) As [ScopeChildName]
		From	@Data
		Union All
		Select	N.[ScopeParentName],
				P.[ScopeParentName] As [ScopeChildName]
		From	[Parse] P
				Outer Apply (
					Select	Case
						-- Empty String
						When Len(P.[ScopeParentName]) <= 0 Then Null
						-- No more delimiters
						When CharIndex('.', Reverse(P.[ScopeParentName])) = 0  Then Null
						-- Period exists
						When CharIndex('.',Reverse(P.[ScopeParentName])) > 0
						Then Left(P.[ScopeParentName], Len(P.[ScopeParentName]) - CharIndex('.',Reverse(P.[ScopeParentName])) -0)

						Else Null
						End As [ScopeParentName] ) N
		Where	NullIf(P.[ScopeParentName],'') is Not Null),
	[Format] As (
		Select	Distinct
				[ScopeParentName],
				[ScopeChildName],
				IIF([ScopeParentName] is Null,
					[ScopeChildName],
					Right([ScopeChildName],Len([ScopeChildName]) - Len([ScopeParentName]) -1))
					As [ScopeElement]
		From	[Parse]
		Where	[ScopeChildName] is Not Null),
	[Tree] As (
		Select	[ScopeParentName],
				[ScopeChildName],
				[ScopeElement]
		From	[Format]
		Where	[ScopeParentName] is Null
		Union All
		Select	F.[ScopeParentName],
				F.[ScopeChildName],
				F.[ScopeElement]
		From	[Tree] T
				Inner Join [Format] F
				On	T.[ScopeChildName] = F.[ScopeParentName]),
	[Target] As (
		Select	[ScopeId],
				Convert(NVarChar(Max),[ScopeElement]) As [ScopedName]
		From	[App_DataDictionary].[ApplicationScope]
		Union All
		Select	S.[ScopeId],
				Convert(NVarChar(Max), FormatMessage('%s.%s',T.[ScopedName],S.[ScopeElement])) As [ScopedName]
		From	[Target] T
				Inner Join [App_DataDictionary].[ApplicationScope] S
				On	T.[ScopeId] = S.[ScopeParentId]),
	[RowId] As (
		Select	IsNull(A.[ScopeId],
					Row_Number() Over (Partition By A.[ScopeId] Order By T.[ScopeChildName]) +
					(Select IsNull(Max([ScopeId]),0) From [App_DataDictionary].[ApplicationScope]))
					As [ScopeId],
				T.[ScopeParentName],
				T.[ScopeChildName],
				T.[ScopeElement],
				D.[ScopeDescription]
		From	[Tree] T
				Left Join [Target] A
				On	T.[ScopeChildName] = A.[ScopedName]
				Left Join @Data D
				On	T.[ScopeChildName] = D.[ScopedName]),
	[Values] As (
		Select	C.[ScopeId],
				P.[ScopeId] As [ScopeParentId],
				C.[ScopeElement],
				C.[ScopeDescription]
		From	[RowId] C
				Left Join [RowId] P
				On	C.[ScopeParentName] = P.[ScopeChildName]),
	[Delta] As (
		Select	[ScopeId],
				[ScopeParentId],
				[ScopeElement],
				[ScopeDescription]
		From	[Values]
		Except
		Select	[ScopeId],
				[ScopeParentId],
				[ScopeElement],
				[ScopeDescription]
		From	[App_DataDictionary].[ApplicationScope]),
	[Data] As (
		Select	V.[ScopeId],
				V.[ScopeParentId],
				V.[ScopeElement],
				V.[ScopeDescription],
				IIF(D.[ScopeId] is Null,0,1) As [IsDiffrent]
		From	[Values] V
				Left Join [Delta] D
				On	V.[ScopeId] = D.[ScopeId])
	Merge [App_DataDictionary].[ApplicationScope] T
	Using [Data] S
	On	T.[ScopeId] = S.[ScopeId]
	When Matched And S.[IsDiffrent] = 1 Then Update
	Set	[ScopeParentId] = S.[ScopeParentId],
		[ScopeElement] = S.[ScopeElement],
		[ScopeDescription] = S.[ScopeDescription]
	When Not Matched by Target Then
		Insert ([ScopeId], [ScopeParentId], [ScopeElement], [ScopeDescription])
		Values ([ScopeId], [ScopeParentId], [ScopeElement], [ScopeDescription])
	When Not Matched by Source Then Delete;
	Print FormatMessage ('Merge [App_DataDictionary].[AliasScope]]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Commit Transaction
	If @TRN_IsNewTran = 1
	  Begin -- If this is the outer transaction, commit it
		If XAct_State() = -1 Throw 103930, 'The current transaction cannot be committed and cannot support operations that write to the log file. Roll back the transaction. (Msg- 3930)', 100
		Commit Transaction
		Print FormatMessage ('Commit Transaction Issued ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
	  End -- Commit Transaction
	  -- This is a nested transaction, must be committed by outer transaction
	Else Print FormatMessage ('Commit Transaction Pending ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
End Try
Begin Catch
	-- Debug Data
	Print FormatMessage ('*** Error Report: %s ***', Object_Name(@@ProcID))
	Print FormatMessage (' Message- %s', ERROR_MESSAGE())
	Print FormatMessage (' Number- %i', ERROR_NUMBER())
	Print FormatMessage (' Severity- %i', ERROR_SEVERITY())
	Print FormatMessage (' State- %i', ERROR_STATE())
	Print FormatMessage (' Procedure- %s', ERROR_PROCEDURE())
	Print FormatMessage (' Line- %i', ERROR_LINE())
	Print FormatMessage (' @@TranCount - %i', @@TranCount)
	Print FormatMessage (' @@NestLevel - %i', @@NestLevel)
	Print FormatMessage (' Original_Login - %s', Original_Login())
	Print FormatMessage (' Current_User - %s', Current_User)
	Print FormatMessage (' XAct_State - %i', XAct_State())
	Print '*** Debug Report ***'

	Print FormatMessage ('*** End Report: %s ***', Object_Name(@@ProcID))

	-- Rollback Transaction
	If @TRN_IsNewTran = 1
	  Begin -- If this is the outer transaction, roll it back
		Rollback Transaction
		Print FormatMessage ('Rollback Transaction Issued ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
	  End -- Rollback Transaction
	-- This is a nested transaction, must be rolled back by outer transaction
	Else Print FormatMessage ('Rollback Transaction Pending ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))

	If ERROR_SEVERITY() Not In (0, 11) Throw -- Re-throw the Error
End Catch
GO
