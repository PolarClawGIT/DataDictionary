CREATE PROCEDURE [AppScript].[procSetTemplate]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@Data [AppScript].[udttTemplate] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ScriptingTemplate.
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
				Cross Apply [AppSecurity].[funcScriptingAuthorization](IsNull([TemplateId],@TemplateId), 0))
	Throw 601020, 'Template Not Authorized', 2;

	-- Clean the Data, helps performance
	Declare @Values Table (
			[TemplateId]            UniqueIdentifier NOT NULL,
			[TemplateTitle]			[AppGeneral].[uddtTitle] Not Null,
			[TemplateDescription]	[AppGeneral].[uddtDescription] Null,
			[BreakOnScope]			[AppGeneral].[uddtScopeName] NULL, 
			[TransformScript]		XML Null , 
			[RootFolder]			[AppGeneral].[uddtFileRoot] Null,
			[DocumentDirectory]		[AppGeneral].[uddtFilePath] Null,
			[DocumentPrefix]		[AppGeneral].[uddtFileAffix] Null,
			[DocumentSuffix]		[AppGeneral].[uddtFileAffix] Null,
			[DocumentExtension]		[AppGeneral].[uddtFileExtension] Null,
			[ScriptAs]              NVarChar(10) Not Null,
			[ScriptDirectory]		[AppGeneral].[uddtFilePath] Null,
			[ScriptPrefix]			[AppGeneral].[uddtFileAffix] Null,
			[ScriptSuffix]			[AppGeneral].[uddtFileAffix] Null,
			[ScriptExtension]		[AppGeneral].[uddtFileExtension] Null,
		Primary Key ([TemplateId]))

	Declare @Files Table (
		[TemplateId]			UniqueIdentifier NOT Null,
		[RelativePath]			[AppGeneral].[uddtFilePath] Null,
		[FileObject]			[AppGeneral].[uddtFileName] Null,
		[FilePrefix]			[AppGeneral].[uddtFileAffix] Null,
		[FileSuffix]			[AppGeneral].[uddtFileAffix] Null,
		[FileExtension]			[AppGeneral].[uddtFileExtension] Null,
		[IsInput]				Bit Not Null,
		[IsProcess]				Bit Not Null,
		[IsOutput]				Bit Not Null,
		Primary Key ([TemplateId], [IsInput], [IsProcess], [IsOutput]))

	Declare @Delete Table ([TemplateId] UniqueIdentifier NOT NULL)

	Insert Into @Values
	Select	X.[TemplateId],
			NullIf(Trim(D.[TemplateTitle]),'') As [TemplateTitle],
			NullIf(Trim(D.[TemplateDescription]),'') As [TemplateDescription],
			D.[BreakOnScope],
			Case
				When NullIf(D.[TransformScript],'') is Null Then Null
				When SubString(Trim(D.[TransformScript]),1,1) Not In ('<') Then Null -- First Character not a tag start
				When D.[TransformScript] Like '%encoding="utf-8"%' Then Try_Convert(XML,Convert(VarChar(Max),D.[TransformScript]),1) -- Handle UTF-8
				Else Try_Convert(XML,D.[TransformScript],1)
				End As [TransformScript],
			NullIf(Trim(D.[RootFolder]),'') As [RootFolder],
			NullIf(Trim(D.[DocumentDirectory]),'') As [DocumentDirectory],
			NullIf(Trim(D.[DocumentPrefix]),'') As [DocumentPrefix],
			NullIf(Trim(D.[DocumentSuffix]),'') As [DocumentSuffix],
			NullIf(Trim(D.[DocumentExtension]),'') As [DocumentExtension],
			IsNull(Trim(D.[ScriptAs]),'Text') As [ScriptAs],
			NullIf(Trim(D.[ScriptDirectory]),'') As [ScriptDirectory],
			NullIf(Trim(D.[ScriptPrefix]),'') As [ScriptPrefix],
			NullIf(Trim(D.[ScriptSuffix]),'') As [ScriptSuffix],
			NullIf(Trim(D.[ScriptExtension]),'') As [ScriptExtension]
	From	@Data D
			Cross apply (Select	Coalesce(D.[TemplateId], @TemplateId, NewId()) As [TemplateId]) X
	Where	(@TemplateId is Null And X.[TemplateId] is Not Null) Or
			(@TemplateId is Not Null And IsNull(X.[TemplateId], @TemplateId) = @TemplateId)
	Print FormatMessage ('Insert @Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into @Files
	Select	[TemplateId],
			[DocumentDirectory] As [RelativePath],
			Convert(NVarChar, Null) As [FileObject],
			[DocumentPrefix] As [FilePrefix],
			[DocumentSuffix] As [FileSuffix],
			[DocumentExtension] As [FileExtension],
			1 As [IsInput],
			0 As [IsProcess],
			0 As [IsOutput]
	From	@Values
	Where	[DocumentDirectory] is not null Or
			[DocumentPrefix] is not null Or
			[DocumentSuffix] is not null Or
			[DocumentExtension] is not null
	Union
	-- Process is currently hard-coded as there is no UI element for it.
	Select	[TemplateId],
			Convert(NVarChar, Null) As [RelativePath],
			[TemplateTitle] As [FileObject],
			Convert(NVarChar, Null) As [FilePrefix],
			Convert(NVarChar, Null) As [FileSuffix],
			Convert(NVarChar, 'xslt') As [FileExtension],
			0 As [IsInput],
			1 As [IsProcess],
			0 As [IsOutput]
	From	@Values
	Union
	Select	[TemplateId],
			[ScriptDirectory] As [RelativePath],
			Convert(NVarChar, Null) As [FileObject],
			[ScriptPrefix] As [FilePrefix],
			[ScriptSuffix] As [FileSuffix],
			[ScriptExtension] As [FileExtension],
			0 As [IsInput],
			0 As [IsProcess],
			1 As [IsOutput]
	From	@Values
	Where	[ScriptDirectory] is not null Or
			[ScriptPrefix] is not null Or
			[ScriptSuffix] is not null Or
			[ScriptExtension] is not null
	Print FormatMessage ('@Files: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into @Delete
	Select	T.[TemplateId]
	From	[AppScript].[Template] T
			Left Join @Values S
			On	T.[TemplateId] = S.[TemplateId]
	Where	S.[TemplateId] is Null And
			T.[TemplateId] In (
				Select	[TemplateId]
				From	[AppScript].[ScriptingModel]
				Where	[ModelId] = @ModelId
				Union
				Select	@TemplateId
				Where	@TemplateId is Not Null)
	Print FormatMessage ('Insert @Delete: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Apply Changes
	Delete From [AppScript].[TemplateFile]
	From	[AppScript].[TemplateFile] T
			Left Join @Files S
			On	T.[TemplateId] = S.[TemplateId] And
				T.[IsInput] = S.[IsInput] And
				T.[IsProcess] = S.[IsProcess] And
				T.[IsOutput] = S.[IsOutput]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[TemplateId], 1)
	Where	S.[TemplateId] is Null And
			T.[TemplateId] In (
				Select	[TemplateId]
				From	@Delete)
	Print FormatMessage ('Delete [AppScript].[TemplateFile]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppScript].[TemplateNodeOwner]
	From	[AppScript].[TemplateNodeOwner] T
			Left Join @Values S
			On	T.[TemplateId] = S.[TemplateId]
	Where	S.[TemplateId] is Null And
			T.[TemplateId] In (
				Select	[TemplateId]
				From	@Delete)
	Print FormatMessage ('Delete [AppScript].[TemplateNodeeOwner]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppScript].[TemplateAttribute]
	From	[AppScript].[TemplateAttribute] T
			Left Join @Values S
			On	T.[TemplateId] = S.[TemplateId]
	Where	S.[TemplateId] is Null And
			T.[TemplateId] In (
				Select	[TemplateId]
				From	@Delete)
	Print FormatMessage ('Delete [AppScript].[TemplateAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppScript].[TemplateElement]
	From	[AppScript].[TemplateElement] T
			Left Join @Values S
			On	T.[TemplateId] = S.[TemplateId]
	Where	S.[TemplateId] is Null And
			T.[TemplateId] In (
				Select	[TemplateId]
				From	@Delete)
	Print FormatMessage ('Delete [AppScript].[TemplateElement]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Update	[AppScript].[Document]
	Set		[TemplateId] = Null
	From	[AppScript].[Document] T
			Left Join @Values S
			On	T.[TemplateId] = S.[TemplateId]
	Where	S.[TemplateId] is Null And
			T.[TemplateId] In (
				Select	[TemplateId]
				From	@Delete)
	Print FormatMessage ('Update [AppScript].[Document]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppScript].[ScriptingModel]
	From	[AppScript].[ScriptingModel] T
			Left Join @Values S
			On	T.[TemplateId] = S.[TemplateId] And
				T.[ModelId] = @ModelId
	Where	S.[TemplateId] is Null And
			T.[TemplateId] In (
				Select	[TemplateId]
				From	@Delete)
	Print FormatMessage ('Delete [AppScript].[ScriptingModel]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppScript].[Template]
	From	[AppScript].[Template] T
			Left Join @Values S
			On	T.[TemplateId] = S.[TemplateId]
	Where	S.[TemplateId] is Null And
			T.[TemplateId] In (
				Select	[TemplateId]
				From	@Delete)
	Print FormatMessage ('Delete [AppScript].[Template]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[TemplateId],
				[TemplateTitle],
				[TemplateDescription],
				[BreakOnScope],
				Convert(NVarChar(Max),[TransformScript]) As [TransformScript],
				[RootFolder],
				[ScriptAs]
		From	@Values
		Except
		Select	[TemplateId],
				[TemplateTitle],
				[TemplateDescription],
				[BreakOnScope],
				Convert(NVarChar(Max),[TransformScript]) As [TransformScript],
				[RootFolder],
				[ScriptAs]
		From	[AppScript].[Template])
	Update [AppScript].[Template]
		Set		[TemplateTitle] = S.[TemplateTitle],
				[TemplateDescription] = S.[TemplateDescription],
				[BreakOnScope] = S.[BreakOnScope],
				[TransformScript] = S.[TransformScript],
				[RootFolder] = S.[RootFolder],
				[ScriptAs] = S.[ScriptAs]
		From	[AppScript].[Template] T
				Inner Join [Delta] S
				On	T.[TemplateId] = S.[TemplateId]
	Print FormatMessage ('Update [AppScript].[Template]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[TemplateId],
				[RelativePath],
				[FileObject],
				[FilePrefix],
				[FileSuffix],
				[FileExtension],
				[IsInput],
				[IsProcess],
				[IsOutput]
		From	@Files
		Except
		Select	[TemplateId],
				[RelativePath],
				[FileObject],
				[FilePrefix],
				[FileSuffix],
				[FileExtension],
				[IsInput],
				[IsProcess],
				[IsOutput]
		From	[AppScript].[TemplateFile])
	Update [AppScript].[TemplateFile]
		Set		[RelativePath] = S.[RelativePath],
				[FileObject] = S.[FileObject],
				[FilePrefix] = S.[FilePrefix],
				[FileSuffix] = S.[FileSuffix],
				[FileExtension] = S.[FileExtension]
		From	[AppScript].[TemplateFile] T
				Inner Join [Delta] S
				On	T.[TemplateId] = S.[TemplateId] And
					T.[IsInput] = S.[IsInput] And
					T.[IsProcess] = S.[IsProcess] And
					T.[IsOutput] = S.[IsOutput]
	Print FormatMessage ('Update [AppScript].[TemplateFile]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[Template] (
			[TemplateId],
			[TemplateTitle],
			[TemplateDescription],
			[BreakOnScope],
			[TransformScript],
			[RootFolder],
			[ScriptAs])
	Select	S.[TemplateId],
			S.[TemplateTitle],
			S.[TemplateDescription],
			S.[BreakOnScope],
			S.[TransformScript],
			S.[RootFolder],
			S.[ScriptAs]
	From	@Values S
			Left Join [AppScript].[Template] T
			On	S.[TemplateId] = T.[TemplateId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[TemplateId], 1)
	Where	T.[TemplateId] is Null
	Print FormatMessage ('Insert [AppScript].[Template]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[TemplateFile] (
			[TemplateId],
			[RelativePath],
			[FileObject],
			[FilePrefix],
			[FileSuffix],
			[FileExtension],
			[IsInput],
			[IsProcess],
			[IsOutput])
	Select	S.[TemplateId],
			S.[RelativePath],
			S.[FileObject],
			S.[FilePrefix],
			S.[FileSuffix],
			S.[FileExtension],
			S.[IsInput],
			S.[IsProcess],
			S.[IsOutput]
	From	@Files S
			Left Join [AppScript].[TemplateFile] T
			On	S.[TemplateId] = T.[TemplateId] And
				S.[IsInput] = T.[IsInput] And
				S.[IsProcess] = T.[IsProcess] And
				S.[IsOutput] = T.[IsOutput]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[TemplateId], 1)
	Where	T.[TemplateId] is Null
	Print FormatMessage ('Insert [AppScript].[TemplateFile]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[ScriptingModel] (
			[ModelId],
			[TemplateId])
	Select	@ModelId As [ModelId],
			S.[TemplateId]
	From	@Values S
			Left Join [AppScript].[ScriptingModel] T
			On	S.[TemplateId] = T.[TemplateId] And
				@ModelId = T.[ModelId]
	Where	T.[TemplateId] Is Null And
			@ModelId is Not Null
	Print FormatMessage ('Insert [AppScript].[ScriptingModel]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
