CREATE PROCEDURE [App_DataDictionary].[procSetApplicationScope]
		@Data [App_DataDictionary].[typeApplicationScope] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ApplicationScope.
*/

-- Transaction Handling
Declare	@TRN_IsNewTran Bit = 0 -- Indicates that the stored procedure started the transaction. Used to handle nested Transactions
Declare @Delimiter NVarChar(10) = '.'

Begin Try
	-- Begin Transaction
	If @@TranCount = 0
	  Begin -- Not in a nested/distributed transaction, need to start a transaction
		Begin Transaction
		Select	@TRN_IsNewTran = 1
	  End; -- Begin Transaction

	;With [NameSpace] As (
		Select	Distinct 
				[NameSpace],
				[ParentNameSpace],
				[ElementName]
		From	@Data S
				Outer Apply [App_DataDictionary].[funcSplitNameSpace](S.[ScopedName], @Delimiter)),
	[Scope] As (
		Select	IsNull(M.[ScopeId],
					(Select	IsNull(Max([ScopeId]),0) From [App_DataDictionary].[ApplicationScope]) +
					Row_Number() Over (
						Partition By (M.[ScopeId])
						Order By (N.[NameSpace])))
						As [ScopeId],
				N.[NameSpace],
				N.[ParentNameSpace],
				N.[ElementName]
		From	[NameSpace] N
				Left Join [App_DataDictionary].[ModelScope] M
				On	N.[NameSpace] = M.[ScopeName]),
	[Values] As (
		Select	C.[ScopeId],
				P.[ScopeId] As [ScopeParentId],
				C.[ElementName] As [ScopedElementName],
				D.[ScopeDescription]
		From @Data D
				Left Join [Scope] C
				On	D.[ScopedName] = C.[NameSpace]
				Left Join [Scope] P
				On	C.[ParentNameSpace] = P.[NameSpace]),
	[Delta] As (
		Select	[ScopeId],
				[ScopeParentId],
				[ScopedElementName],
				[ScopeDescription]
		From	[Values]
		Except
		Select	[ScopeId],
				[ScopeParentId],
				[ScopedElementName],
				[ScopeDescription]
		From	[App_DataDictionary].[ApplicationScope]),
	[Data] As (
		Select	V.[ScopeId],
				V.[ScopeParentId],
				V.[ScopedElementName],
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
		[ScopedElementName] = S.[ScopedElementName],
		[ScopeDescription] = S.[ScopeDescription]
	When Not Matched by Target Then
		Insert ([ScopeId], [ScopeParentId], [ScopedElementName], [ScopeDescription])
		Values ([ScopeId], [ScopeParentId], [ScopedElementName], [ScopeDescription])
	When Not Matched by Source Then Delete;
	Print FormatMessage ('Merge [App_DataDictionary].[ApplicationScope]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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