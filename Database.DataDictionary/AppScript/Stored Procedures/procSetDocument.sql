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
		[TransformScript]		XML Null,
		[RootFolder]         NVarChar(100) Null,
		[InputDirectory]		NVarChar(250) Null,
		[InputFile]             NVarChar(100) Null, 
		[OutputDirectory]		NVarChar(250) Null,
		[OutputFile]            NVarChar(100) Null,
		Primary Key ([DocumentId]))
			
	Insert Into @Values
	Select	X.[DocumentId],
			NullIf(Trim(D.[DocumentTitle]),'') As [DocumentTitle],
			D.[TemplateId],
			Case
				When NullIf(D.[TransformScript],'') is Null Then Null
				When SubString(Trim(D.[TransformScript]),1,1) Not In ('<') Then Null -- First Character not a tag start
				When D.[TransformScript] Like '%encoding="utf-8"%' Then Try_Convert(XML,Convert(VarChar(Max),D.[TransformScript]),1) -- Handle UTF-8
				Else Try_Convert(XML,D.[TransformScript],1)
				End As [TransformScript],
			NullIf(Trim(D.[RootFolder]),'') As [RootFolder],
			NullIf(Trim(D.[InputDirectory]),'') As [InputDirectory],
			NullIf(Trim(D.[InputFile]),'') As [InputFile],
			NullIf(Trim(D.[OutputDirectory]),'') As [OutputDirectory],
			NullIf(Trim(D.[OutputFile]),'') As [OutputFile]
	From	@Data D
			Left Join [AppScript].[Document] O
			On	IsNull(D.[DocumentId], @DocumentId) = O.[DocumentId] And
				@ModelId = O.[ModelId] And
				D.[DocumentTitle] = O.[DocumentTitle]
			Cross Apply (
				Select	Coalesce(O.[DocumentId], NewId()) As [DocumentId]) X
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
		
	-- Apply Changes
	Delete From [AppScript].[Document]
	From	[AppScript].[Document] T
			Left Join @Values S
			On	T.[DocumentId] = S.[DocumentId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[DocumentId], 1)
	Where	S.[DocumentId] is Null And
			(@DocumentId is Not Null Or @ModelId is Not Null) And
			(@DocumentId is Null Or @DocumentId = T.[DocumentId])  And
			(@ModelId is Null Or @ModelId = T.[DocumentId])
	Print FormatMessage ('Delete [AppScript].[Document]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[DocumentId],
				[DocumentTitle],
				[TemplateId],
				Convert(NVarChar(Max),[TransformScript]) As [TransformScript],
				[RootFolder],
				[InputDirectory],
				[InputFile],
				[OutputDirectory],
				[OutputFile]
		From	@Values
		Except
		Select	[DocumentId],
				[DocumentTitle],
				[TemplateId],
				Convert(NVarChar(Max),[TransformScript]) As [TransformScript],
				[RootFolder],
				[InputDirectory],
				[InputFile],
				[OutputDirectory],
				[OutputFile]
		From	[AppScript].[Document])
	Update [AppScript].[Document]
		Set		[DocumentTitle] = S.[DocumentTitle],
				[TemplateId] = S.[TemplateId],
				[TransformScript] = S.[TransformScript],
				[RootFolder] = S.[RootFolder],
				[InputDirectory] = S.[InputDirectory],
				[InputFile] = S.[InputFile],
				[OutputDirectory] = S.[OutputDirectory],
				[OutputFile] = S.[OutputFile]
		From	[AppScript].[Document] T
				Inner Join [Delta] S
				On	T.[DocumentId] = S.[DocumentId]
	Print FormatMessage ('Update [AppScript].[Document]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[Document] (
			[DocumentId],
			[DocumentTitle],
			[ModelId],
			[TemplateId],
			[TransformScript],
			[RootFolder],
			[InputDirectory],
			[InputFile],
			[OutputDirectory],
			[OutputFile])
	Select	S.[DocumentId],
			S.[DocumentTitle],
			@ModelId,
			S.[TemplateId],
			S.[TransformScript],
			S.[RootFolder],
			S.[InputDirectory],
			S.[InputFile],
			S.[OutputDirectory],
			S.[OutputFile]
	From	@Values S
			Left Join [AppScript].[Document] T
			On	S.[DocumentId] = T.[DocumentId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[DocumentId], 1)
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
