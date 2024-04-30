CREATE PROCEDURE [App_DataDictionary].[procSetScriptingSchema]
		@SchemaId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeScriptingSchema] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ScriptingSchema.
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

	-- Clean the Data, helps performance
	Declare @Values Table (
			[SchemaId]             UniqueIdentifier NOT NULL,
			[SchemaTitle]          [App_DataDictionary].[typeTitle] Not Null,
			[SchemaDescription]    [App_DataDictionary].[typeDescription] Null,
			Primary Key ([SchemaId]))

	Insert Into @Values
	Select	X.[SchemaId],
			NullIf(Trim(D.[SchemaTitle]),'') As [SchemaTitle],
			NullIf(Trim(D.[SchemaDescription]),'') As [SchemaDescription]
	From	@Data D
			Cross apply (
				Select	Coalesce(D.[SchemaId], @SchemaId, NewId()) As [SchemaId]) X
	Where	(@SchemaId is Null or D.[SchemaId] = @SchemaId)

	-- Apply Changes
	Delete From [App_DataDictionary].[ScriptingSchemaElement]
	From	[App_DataDictionary].[ScriptingSchemaElement] T
			Left Join @Values S
			On	T.[SchemaId] = S.[SchemaId]
	Where	S.[SchemaId] is Null And
			(@SchemaId is Null or T.[SchemaId] = @SchemaId)
	Print FormatMessage ('Delete [App_DataDictionary].[ScriptingSchemaElement]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[ScriptingSchema]
	From	[App_DataDictionary].[ScriptingSchema] T
			Left Join @Values S
			On	T.[SchemaId] = S.[SchemaId]
	Where	S.[SchemaId] is Null And
			(@SchemaId is Null or T.[SchemaId] = @SchemaId)
	Print FormatMessage ('Delete [App_DataDictionary].[ScriptingSchema]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[SchemaId],
				[SchemaTitle],
				[SchemaDescription]
		From	@Values
		Except
		Select	[SchemaId],
				[SchemaTitle],
				[SchemaDescription]
		From	[App_DataDictionary].[ScriptingSchema])
	Update [App_DataDictionary].[ScriptingSchema]
	Set		[SchemaTitle] = S.[SchemaTitle],
			[SchemaDescription] = S.[SchemaDescription]
	From	[App_DataDictionary].[ScriptingSchema] T
			Inner Join [Delta] S
			On	T.[SchemaId] = S.[SchemaId]
	Print FormatMessage ('Update [App_DataDictionary].[ScriptingSchema]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ScriptingSchema] (
			[SchemaId],
			[SchemaTitle],
			[SchemaDescription])
	Select	S.[SchemaId],
			S.[SchemaTitle],
			S.[SchemaDescription]
	From	@Values S
			Left Join [App_DataDictionary].[ScriptingSchema] T
			On	S.[SchemaId] = T.[SchemaId]
	Where	T.[SchemaId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[ScriptingSchema]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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