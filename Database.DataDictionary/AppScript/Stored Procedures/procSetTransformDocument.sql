CREATE PROCEDURE [AppScript].[procSetTransformDocument]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@Data [AppScript].[udttTransformDocument] ReadOnly
AS
-- Transaction Handling
Declare	@TRN_IsNewTran Bit = 0 -- Indicates that the stored procedure started the transaction. Used to handle nested Transactions

Begin Try
	-- Begin Transaction
	If @@TranCount = 0
	  Begin -- Not in a nested/distributed transaction, need to start a transaction
		Begin Transaction
		Select	@TRN_IsNewTran = 1
	  End; -- Begin Transaction

	-- Validation
	If Exists (
		Select	1
		From	@Data D
				Cross Apply [AppSecurity].[funcScriptingAuthorization]([TemplateId], 0))
	Throw 601020, 'Template Not Authorized', 2;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[DocumentId]		UniqueIdentifier Not Null,
		[TemplateId]		UniqueIdentifier Not Null,
		[TransformId]		UniqueIdentifier Not Null,
		[SchemaDocumentId]	UniqueIdentifier Not Null,
		[FileName]			[AppGeneral].[uddtFileName] Null,
		Primary Key([DocumentId]))

	Insert Into @Values
	Select	X.[DocumentId],
			D.[TemplateId],
			D.[TransformId],
			D.[SchemaDocumentId],
			NullIf(Trim(D.[FileName]),'') As [FileName]
	From	@Data D
			Left Join [AppScript].[TemplateModel] M
			On	D.[TemplateId] = M.[TemplateId] And
				@ModelId = M.[ModelId]
			Cross Apply (
				Select	Coalesce(D.[DocumentId], NewId()) As [DocumentId]) X
	Where	(@TemplateId is Null Or @TemplateId = D.[TemplateId]) And
			(@ModelId is Null Or M.[ModelId] is Not Null)
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppScript].[TransformDocument]
	From	[AppScript].[TransformDocument] T
			Left Join @Values S
			On	T.[DocumentId] = S.[DocumentId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[TemplateId], 1)
	Where	S.[DocumentId] is Null And
			(@TemplateId is Not Null Or @ModelId is Not Null) And
			(@TemplateId is Null Or @TemplateId = T.[TemplateId])  And
			(@ModelId is Null Or T.[TemplateId] In (
				Select	[TemplateId]
				From	[AppScript].[TemplateModel]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppScript].[TransformDocument]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[DocumentId],
				[TemplateId],
				[TransformId],
				[SchemaDocumentId],
				[FileName]
		From	@Values
		Except
		Select	[DocumentId],
				[TemplateId],
				[TransformId],
				[SchemaDocumentId],
				[FileName]
		From	[AppScript].[TransformDocument])
	Update [AppScript].[TransformDocument]
	Set		--[TemplateId] = S.[TemplateId],
			[TransformId] = S.[TransformId],
			[SchemaDocumentId] = S.[SchemaDocumentId]
	From	[AppScript].[TransformDocument] T
			Inner Join [Delta] S
			On	T.[DocumentId] = S.[DocumentId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[TemplateId], 1)
	Print FormatMessage ('Update [AppScript].[TransformDocument]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[TransformDocument] (
			[DocumentId],
			[TemplateId],
			[TransformId],
			[SchemaDocumentId],
			[FileName])
	Select	S.[DocumentId],
			S.[TemplateId],
			S.[TransformId],
			S.[SchemaDocumentId],
			S.[FileName]
	From	@Values S
			Left Join [AppScript].[TransformDocument] T
			On	S.[DocumentId] = T.[DocumentId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[TemplateId], 1)
	Where	T.[DocumentId] is Null
	Print FormatMessage ('Insert [AppScript].[Document]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
