CREATE PROCEDURE [AppScript].[procSetSchemaDefinition]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@Data [AppScript].[udttSchemaDefinition] ReadOnly
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
		[SchemaId]				UniqueIdentifier Not Null,
		[TemplateId]            UniqueIdentifier Not Null,
		[SchemaTitle]			[AppGeneral].[uddtTitle] Not Null,
		[ForEachScope]			[AppGeneral].[uddtScopeName] Not Null,
		[RootNodeName]			[AppGeneral].[uddtMember] Null,
		[RootFolder]			[AppGeneral].[uddtFileRoot] Null,
		[RelativePath]			[AppGeneral].[uddtFilePath] Null,
		[FilePrefix]			[AppGeneral].[uddtFileAffix] Null,
		[FileSuffix]			[AppGeneral].[uddtFileAffix] Null,
		[FileExtension]			[AppGeneral].[uddtFileExtension] Null,
		Primary Key([SchemaId]))

	Insert Into @Values
	Select	X.[SchemaId],
			D.[TemplateId],
			NullIf(Trim(D.[SchemaTitle]),'') As [SchemaTitle],
			IsNull(NullIf(Trim(D.[ForEachScope]),''),'Model') As [ForEachScope],
			NullIf(Trim(D.[RootNodeName]),'') As [RootNodeName],
			NullIf(Trim(D.[RootFolder]),'') As [RootFolder],
			NullIf(Trim(D.[RelativePath]),'') As [RelativePath],
			NullIf(Trim(D.[FilePrefix]),'') As [FilePrefix],
			NullIf(Trim(D.[FileSuffix]),'') As [FileSuffix],
			NullIf(Trim(D.[FileExtension]),'') As [FileExtension]
	From	@Data D
			Left Join [AppScript].[TemplateModel] M
			On	D.[TemplateId] = M.[TemplateId] And
				@ModelId = M.[ModelId]
			Cross Apply (
				Select	Coalesce(D.[SchemaId], NewId()) As [SchemaId]) X
	Where	(@TemplateId is Null Or @TemplateId = D.[TemplateId]) And
			(@ModelId is Null Or M.[ModelId] is Not Null)
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppScript].[SchemaDefinition]
	From	[AppScript].[SchemaDefinition] T
			Left Join @Values S
			On	T.[SchemaId] = S.[SchemaId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[TemplateId], 1)
	Where	S.[SchemaId] is Null And
			(@TemplateId is Not Null Or @ModelId is Not Null) And
			(@TemplateId is Null Or @TemplateId = T.[TemplateId])  And
			(@ModelId is Null Or T.[TemplateId] In (
				Select	[TemplateId]
				From	[AppScript].[TemplateModel]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppScript].[SchemaDefinition]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	-- TODO: Add child table delete


	;With [Delta] As (
		Select	[SchemaId],
				[TemplateId],
				[SchemaTitle],
				[ForEachScope],
				[RootNodeName],
				[RootFolder],
				[RelativePath],
				[FilePrefix],
				[FileSuffix],
				[FileExtension]
		From	@Values
		Except
		Select	[SchemaId],
				[TemplateId],
				[SchemaTitle],
				[ForEachScope],
				[RootNodeName],
				[RootFolder],
				[RelativePath],
				[FilePrefix],
				[FileSuffix],
				[FileExtension]
		From	[AppScript].[SchemaDefinition])
	Update [AppScript].[SchemaDefinition]
	Set		[SchemaTitle] = S.[SchemaTitle],
			[ForEachScope] = S.[ForEachScope],
			[RootNodeName] = S.[RootNodeName],
			[RootFolder] = S.[RootFolder],
			[RelativePath] = S.[RelativePath],
			[FilePrefix] = S.[FilePrefix],
			[FileSuffix] = S.[FileSuffix],
			[FileExtension] = S.[FileExtension]
	From	[AppScript].[SchemaDefinition] T
			Inner Join [Delta] S
			On	T.[SchemaId] = S.[SchemaId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[SchemaId], 1)
	Print FormatMessage ('Update [AppScript].[SchemaDefinition]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[SchemaDefinition] (
			[SchemaId],
			[TemplateId],
			[SchemaTitle],
			[ForEachScope],
			[RootNodeName],
			[RootFolder],
			[RelativePath],
			[FilePrefix],
			[FileSuffix],
			[FileExtension])
	Select	S.[SchemaId],
			S.[TemplateId],
			S.[SchemaTitle],
			S.[ForEachScope],
			S.[RootNodeName],
			S.[RootFolder],
			S.[RelativePath],
			S.[FilePrefix],
			S.[FileSuffix],
			S.[FileExtension]
	From	@Values S
			Left Join [AppScript].[SchemaDefinition] T
			On	S.[SchemaId] = T.[SchemaId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[SchemaId], 1)
	Where	T.[SchemaId] is Null
	Print FormatMessage ('Insert [AppScript].[SchemaDefinition]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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

