CREATE PROCEDURE [App_DataDictionary].[procSetScriptingSelection]
		@ModelId UniqueIdentifier = Null,
		@SelectionId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeScriptingSelection] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ScriptingSelection.
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

	Declare @NullPlaceHolder UniqueIdentifier = NewId() -- Used for IsNull as a null place holder

	Declare @Values Table (
		[SelectionId]          UniqueIdentifier NOT NULL,
		[SelectionTitle]       [App_DataDictionary].[typeTitle] Not Null,
		[SelectionDescription] [App_DataDictionary].[typeDescription] Null,
		[SchemaId]             UniqueIdentifier NULL,
		[TransformId]          UniqueIdentifier NULL,
		Primary Key ([SelectionId]))

	Insert Into @Values
	Select	X.[SelectionId],
			NullIf(Trim(D.[SelectionTitle]),'') As [SelectionTitle],
			NullIf(Trim(D.[SelectionDescription]),'') As [SelectionDescription],
			D.[SchemaId],
			D.[TransformId]
	From	@Data D
			Cross apply (
				Select	Coalesce(D.[SelectionId], @SelectionId, NewId()) As [SelectionId]) X
	Where	X.[SelectionId] = @SelectionId or
			X.[SelectionId] In (
			Select	A.[SelectionId]
			From	[App_DataDictionary].[ScriptingSelection] A
					Left Join [App_DataDictionary].[ModelScripting] C
					On	A.[SelectionId] = C.[SelectionId]
			Where	(@SelectionId is Null Or @SelectionId = A.[SelectionId]) And
					(@ModelId is Null Or @ModelId = C.[ModelId]))

	-- Apply Changes
	Delete From [App_DataDictionary].[ModelScripting]
	From	[App_DataDictionary].[ModelScripting] T
			Left Join @Values S
			On	T.[SelectionId] = S.[SelectionId] And
				IsNull(T.[SchemaId], @NullPlaceHolder) = IsNull(S.[SchemaId], @NullPlaceHolder) And
				IsNull(T.[TransformId], @NullPlaceHolder) = IsNull(S.[TransformId], @NullPlaceHolder)
	Where	S.[SelectionId] is Null And
			T.[SelectionId] In (
				Select	A.[SelectionId]
				From	[App_DataDictionary].[ScriptingSelection] A
						Left Join [App_DataDictionary].[ModelScripting] C
						On	A.[SelectionId] = C.[SelectionId]
				Where	(@SelectionId is Null Or @SelectionId = A.[SelectionId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[ModelScripting]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[ScriptingSelection]
	From	[App_DataDictionary].[ScriptingSelection] T
			Left Join @Values S
			On	T.[SelectionId] = S.[SelectionId]
	Where	S.[SelectionId] is Null And
			T.[SelectionId] In (
				Select	A.[SelectionId]
				From	[App_DataDictionary].[ScriptingSelection] A
						Left Join [App_DataDictionary].[ModelScripting] C
						On	A.[SelectionId] = C.[SelectionId]
				Where	(@SelectionId is Null Or @SelectionId = A.[SelectionId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[ScriptingSelection]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[SelectionId],
				[SelectionTitle],
				[SelectionDescription]
		From	@Values
		Except
		Select	[SelectionId],
				[SelectionTitle],
				[SelectionDescription]
		From	[App_DataDictionary].[ScriptingSelection])
	Update [App_DataDictionary].[ScriptingSelection]
	Set		[SelectionTitle] = S.[SelectionTitle],
			[SelectionDescription] = S.[SelectionDescription]
	From	[App_DataDictionary].[ScriptingSelection] T
			Inner Join [Delta] S
			On	T.[SelectionId] = S.[SelectionId]
	Print FormatMessage ('Update [App_DataDictionary].[ScriptingSelection]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ScriptingSelection] (
			[SelectionId],
			[SelectionTitle],
			[SelectionDescription])
	Select	S.[SelectionId],
			S.[SelectionTitle],
			S.[SelectionDescription]
	From	@Values S
			Left Join [App_DataDictionary].[ScriptingSelection] T
			On	S.[SelectionId] = T.[SelectionId]
	Where	T.[SelectionId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[ScriptingSelection]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ModelScripting] ([ModelId], [SelectionId], [SchemaId], [TransformId])
	Select	@ModelId As [ModelId],
			S.[SelectionId],
			S.[SchemaId],
			S.[TransformId]
	from	@Values S
			Left Join [App_DataDictionary].[ModelScripting] T
			On	S.[SelectionId] = T.[SelectionId] And
				@ModelId = T.[ModelId]
	Where	T.[ModelId] is Null And
			@ModelId is Not Null
	Print FormatMessage ('Insert [App_DataDictionary].[ModelScripting]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
