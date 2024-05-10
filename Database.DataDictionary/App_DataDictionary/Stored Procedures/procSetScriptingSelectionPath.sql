CREATE PROCEDURE [App_DataDictionary].[procSetScriptingSelectionPath]
		@ModelId UniqueIdentifier = Null,
		@SelectionId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeScriptingSelectionPath] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ScriptingSelectionPath.
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

	-- Clean Data
	Declare @Values Table (
		[SelectionId]       UniqueIdentifier Not Null,
		[NameSpaceId]       UniqueIdentifier Not Null,
		[ScopeName]         [App_DataDictionary].[typeScopeName] Null,
		Primary Key ([SelectionId], [NameSpaceId]))

	Declare @NameSpace [App_DataDictionary].[typeNameSpace]

	Insert Into @NameSpace
	Select	Null As [NameSpaceId],
			[SelectionPath] As [NameSpace]
	From	@Data
	Group By [SelectionPath]

	-- Need to create & assign the NameSpaceID's
	Exec [App_DataDictionary].[procSetModelNameSpace] @ModelId, @NameSpace

	;With [NameSpace] As (
		Select	M.[NameSpaceId],
				N.[NameSpace]
		From	[App_DataDictionary].[ModelNameSpace] M
				Cross Apply [App_DataDictionary].[funcGetNameSpace](M.[NameSpaceId]) N
		Where	(@ModelId is Null Or M.[ModelId] = @ModelId))
	Insert Into @Values
	Select	Coalesce(D.[SelectionId], @SelectionId, NewId()) As [SelectionId],
			N.[NameSpaceId],
			D.[ScopeName]
	From	@Data D
			Cross Apply [App_DataDictionary].[funcSplitNameSpace](D.[SelectionPath]) C
			Inner Join [NameSpace] N
			On	C.[NameSpace] = N.[NameSpace] And
				C.[IsBase] = 1

	-- Apply Changes
	Delete From [App_DataDictionary].[ScriptingSelectionPath]
	From	[App_DataDictionary].[ScriptingSelectionPath] T
			Left Join @Values V
			On	T.[SelectionId] = V.[SelectionId] And
				T.[NameSpaceId] = V.[NameSpaceId]
	Where	V.[SelectionId] is Null And
			T.[SelectionId] In (
			Select	A.[SelectionId]
			From	[App_DataDictionary].[ScriptingSelection] A
					Left Join [App_DataDictionary].[ModelScripting] C
					On	A.[SelectionId] = C.[SelectionId]
			Where	(@SelectionId is Null Or @SelectionId = A.[SelectionId]) And
					(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[ScriptingSelectionPath]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[SelectionId],
				[NameSpaceId],
				[ScopeName]
		From	@Values S
		Except
		Select	[SelectionId],
				[NameSpaceId],
				[ScopeName]
		From	[App_DataDictionary].[ScriptingSelectionPath])
	Update	[App_DataDictionary].[ScriptingSelectionPath]
	Set		[ScopeName] = S.[ScopeName]
	From	[Delta] S
			Inner Join [App_DataDictionary].[ScriptingSelectionPath] T
			On	S.[SelectionId] = T.[SelectionId] And
				S.[NameSpaceId] = T.[NameSpaceId]
	Print FormatMessage ('Update [App_DataDictionary].[ScriptingSelectionPath]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ScriptingSelectionPath] ([SelectionId], [NameSpaceId], [ScopeName])
	Select	V.[SelectionId],
			V.[NameSpaceId],
			V.[ScopeName]
	From	@Values V
			Left Join [App_DataDictionary].[ScriptingSelectionPath] T
			On	V.[SelectionId] = T.[SelectionId] And
				V.[NameSpaceId] = T.[NameSpaceId]
	Where	T.[SelectionId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[ScriptingSelectionPath]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
