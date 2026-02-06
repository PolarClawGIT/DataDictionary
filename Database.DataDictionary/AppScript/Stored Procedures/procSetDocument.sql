CREATE PROCEDURE [AppScript].[procSetDocument]
		@ModelId UniqueIdentifier = Null,
		@DocumentId UniqueIdentifier = Null,
		@Data [AppScript].[udttDocument] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on Document.
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

	If Exists (
		Select	1
		From	@Data D
				Cross Apply [AppSecurity].[funcScriptingAuthorization]([DocumentId], 0))
	Throw 601020, 'DataSource Not Authorized', 2;

	Declare @Values Table (
		[DocumentId]			UniqueIdentifier NOT Null,
		[DocumentTitle]			[AppGeneral].[uddtTitle] Null,
		[TemplateId]            UniqueIdentifier NULL,
		[RootFolder]			[AppGeneral].[uddtFileRoot] Null,
		[InputPath]				[AppGeneral].[uddtFilePath] Null,
		[InputFile]             [AppGeneral].[uddtFileName] Null, 
		[ProcessPath]			[AppGeneral].[uddtFilePath] Null,
		[ProcessFile]           [AppGeneral].[uddtFileName] Null, 
		[OutputPath]			[AppGeneral].[uddtFilePath] Null,
		[OutputFile]            [AppGeneral].[uddtFileName] Null,
		Primary Key ([DocumentId]))

	Declare @Files Table (
		[DocumentId]			UniqueIdentifier NOT Null,
		[RelativePath]			[AppGeneral].[uddtFilePath] Null,
		[FileName]				[AppGeneral].[uddtFileName] Not Null,
		[IsInput]				Bit Not Null,
		[IsProcess]				Bit Not Null,
		[IsOutput]				Bit Not Null,
		Primary Key ([DocumentId], [IsInput], [IsProcess], [IsOutput]))

	Declare @Delete Table ([DocumentId] UniqueIdentifier NOT NULL)

	Insert Into @Values
	Select	X.[DocumentId],
			NullIf(Trim(D.[DocumentTitle]),'') As [DocumentTitle],
			D.[TemplateId],
			NullIf(Trim(D.[RootFolder]),'') As [RootFolder],
			NullIf(Trim(D.[InputPath]),'') As [InputPath],
			NullIf(Trim(D.[InputFile]),'') As [InputFile],
			NullIf(Trim(D.[ProcessPath]),'') As [ProcessPath],
			NullIf(Trim(D.[ProcessFile]),'') As [ProcessFile],
			NullIf(Trim(D.[OutputPath]),'') As [OutputPath],
			NullIf(Trim(D.[OutputFile]),'') As [OutputFile]
	From	@Data D
			Left Join [AppScript].[Document] O
			On	IsNull(D.[DocumentId], @DocumentId) = O.[DocumentId] And
				@ModelId = O.[ModelId] And
				D.[DocumentTitle] = O.[DocumentTitle]
			Cross Apply (
				Select	Coalesce(O.[DocumentId], NewId()) As [DocumentId]) X
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into @Files
	Select	[DocumentId],
			[InputPath] As [RelativePath],
			[InputFile] As [FileName],
			1 As [IsInput],
			0 As [IsProcess],
			0 As [IsOutput]
	From	@Values
	Where	[InputFile] is Not Null
	Union
	Select	[DocumentId],
			[ProcessPath] As [RelativePath],
			[ProcessFile] As [FileName],
			0 As [IsInput],
			1 As [IsProcess],
			0 As [IsOutput]
	From	@Values
	Where	[ProcessFile] is Not Null
	Union
	Select	[DocumentId],
			[OutputPath] As [RelativePath],
			[OutputFile] As [FileName],
			0 As [IsInput],
			0 As [IsProcess],
			1 As [IsOutput]
	From	@Values
	Where	[OutputFile] is Not Null
	Print FormatMessage ('@Files: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into @Delete
	Select	T.[DocumentId]
	From	[AppScript].[Document] T
			Left Join @Values S
			On	T.[DocumentId] = S.[DocumentId]
	Where	S.[DocumentId] is Null And
			T.[DocumentId] In (
				Select	[DocumentId]
				From	[AppScript].[Document]
				Where	[ModelId] = @ModelId
				Union
				Select	@DocumentId
				Where	@DocumentId is Not Null)
	Print FormatMessage ('Insert @Delete: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
		
	-- Apply Changes
	Delete From [AppScript].[DocumentFile]
	From	[AppScript].[DocumentFile] T
			Left Join @Files S
			On	T.[DocumentId] = S.[DocumentId] And
				T.[IsInput] = S.[IsInput] And
				T.[IsProcess] = S.[IsProcess] And
				T.[IsOutput] = S.[IsOutput]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[DocumentId], 1)
	Where	S.[DocumentId] is Null And
			T.[DocumentId] In (
				Select	[DocumentId]
				From	@Delete)
	Print FormatMessage ('Delete [AppScript].[DocumentFile]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppScript].[Document]
	From	[AppScript].[Document] T
			Left Join @Values S
			On	T.[DocumentId] = S.[DocumentId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[DocumentId], 1)
	Where	S.[DocumentId] is Null And
			T.[DocumentId] In (
				Select	[DocumentId]
				From	@Delete)
	Print FormatMessage ('Delete [AppScript].[Document]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[DocumentId],
				[DocumentTitle],
				[TemplateId],
				[RootFolder]
		From	@Values
		Except
		Select	[DocumentId],
				[DocumentTitle],
				[TemplateId],
				[RootFolder]
		From	[AppScript].[Document])
	Update [AppScript].[Document]
		Set		[DocumentTitle] = S.[DocumentTitle],
				[TemplateId] = S.[TemplateId],
				[RootFolder] = S.[RootFolder]
		From	[AppScript].[Document] T
				Inner Join [Delta] S
				On	T.[DocumentId] = S.[DocumentId]
	Print FormatMessage ('Update [AppScript].[Document]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[DocumentId],
				[RelativePath],
				[FileName],
				[IsInput],
				[IsProcess],
				[IsOutput]
		From	@Files
		Except
		Select	[DocumentId],
				[RelativePath],
				[FileName],
				[IsInput],
				[IsProcess],
				[IsOutput]
		From	[AppScript].[DocumentFile])
	Update [AppScript].[DocumentFile]
		Set		[RelativePath] = S.[RelativePath],
				[FileName] = S.[FileName]
		From	[AppScript].[DocumentFile] T
				Inner Join [Delta] S
				On	T.[DocumentId] = S.[DocumentId] And
					T.[IsInput] = S.[IsInput] And
					T.[IsProcess] = S.[IsProcess] And
					T.[IsOutput] = S.[IsOutput]
	Print FormatMessage ('Update [AppScript].[DocumentFile]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[Document] (
			[DocumentId],
			[DocumentTitle],
			[ModelId],
			[TemplateId],
			[RootFolder])
	Select	S.[DocumentId],
			S.[DocumentTitle],
			@ModelId,
			S.[TemplateId],
			S.[RootFolder]
	From	@Values S
			Left Join [AppScript].[Document] T
			On	S.[DocumentId] = T.[DocumentId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[DocumentId], 1)
	Where	T.[DocumentId] is Null
	Print FormatMessage ('Insert [AppScript].[Document]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[DocumentFile] (
			[DocumentId],
			[RelativePath],
			[FileName],
			[IsInput],
			[IsProcess],
			[IsOutput])
	Select	S.[DocumentId],
			S.[RelativePath],
			S.[FileName],
			S.[IsInput],
			S.[IsProcess],
			S.[IsOutput]
	From	@Files S
			Left Join [AppScript].[DocumentFile] T
			On	S.[DocumentId] = T.[DocumentId] And
				S.[IsInput] = T.[IsInput] And
				S.[IsProcess] = T.[IsProcess] And
				S.[IsOutput] = T.[IsOutput]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[DocumentId], 1)
	Where	T.[DocumentId] is Null
	Print FormatMessage ('Insert [AppScript].[DocumentFile]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
