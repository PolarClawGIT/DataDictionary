CREATE PROCEDURE [App_DataDictionary].[procSetScriptingTemplate]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeScriptingTemplate] ReadOnly
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

	-- Clean the Data, helps performance
	Declare @Values Table (
			[TemplateId]            UniqueIdentifier NOT NULL,
			[TemplateTitle]			[App_DataDictionary].[typeTitle] Not Null,
			[TemplateDescription]	[App_DataDictionary].[typeDescription] Null,
			[BreakOnScope]			[App_DataDictionary].[typeScopeName] NULL, 
			[TransformScript]		XML Null , 
			[TransformAsText]       Bit Null, 
			[RootDirectory]         NVarChar(100) Null,
			[SolutionDirectory]		NVarChar(250) Null,
			[DocumentDirectory]		NVarChar(250) Null,
			[DocumentPrefix]		NVarChar(50) Null,
			[DocumentSuffix]		NVarChar(50) Null,
			[DocumentExtension]		NVarChar(10) Null,
			[ScriptDirectory]		NVarChar(250) Null,
			[ScriptPrefix]			NVarChar(50) Null,
			[ScriptSuffix]			NVarChar(50) Null,
			[ScriptExtension]		NVarChar(10) Null,
		Primary Key ([TemplateId]))

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
			D.[TransformAsText],
			NullIf(Trim(D.[RootDirectory]),'') As [RootDirectory],
			NullIf(Trim(D.[SolutionDirectory]),'') As [SolutionDirectory],
			NullIf(Trim(D.[DocumentDirectory]),'') As [DocumentDirectory],
			NullIf(Trim(D.[DocumentPrefix]),'') As [DocumentPrefix],
			NullIf(Trim(D.[DocumentSuffix]),'') As [DocumentSuffix],
			NullIf(Trim(D.[DocumentExtension]),'') As [DocumentExtension],
			NullIf(Trim(D.[ScriptDirectory]),'') As [ScriptDirectory],
			NullIf(Trim(D.[ScriptPrefix]),'') As [ScriptPrefix],
			NullIf(Trim(D.[ScriptSuffix]),'') As [ScriptSuffix],
			NullIf(Trim(D.[ScriptExtension]),'') As [ScriptExtension]
	From	@Data D
			Cross apply (Select	Coalesce(D.[TemplateId], @TemplateId, NewId()) As [TemplateId]) X

	Insert Into @Delete
	Select	T.[TemplateId]
	From	[App_DataDictionary].[ScriptingTemplate] T
			Left Join @Values S
			On	T.[TemplateId] = S.[TemplateId]
	Where	S.[TemplateId] is Null And
			T.[TemplateId] In (
				Select	[TemplateId]
				From	[App_DataDictionary].[ModelScripting]
				Where	[ModelId] = @ModelId
				Union
				Select	@TemplateId
				Where	@TemplateId is Not Null)

	-- Apply Changes
	Delete From [App_DataDictionary].[ScriptingPath]
	From	[App_DataDictionary].[ScriptingPath] T
			Left Join @Values S
			On	T.[TemplateId] = S.[TemplateId]
	Where	S.[TemplateId] is Null And
			T.[TemplateId] In (
				Select	[TemplateId]
				From	@Delete)
	Print FormatMessage ('Delete [App_DataDictionary].[ScriptingPath]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[ScriptingElement]
	From	[App_DataDictionary].[ScriptingElement] T
			Left Join @Values S
			On	T.[TemplateId] = S.[TemplateId]
	Where	S.[TemplateId] is Null And
			T.[TemplateId] In (
				Select	[TemplateId]
				From	@Delete)
	Print FormatMessage ('Delete [App_DataDictionary].[ScriptingElement]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[ModelScripting]
	From	[App_DataDictionary].[ModelScripting] T
			Left Join @Values S
			On	T.[TemplateId] = S.[TemplateId] And
				T.[ModelId] = @ModelId
	Where	S.[TemplateId] is Null And
			T.[TemplateId] In (
				Select	[TemplateId]
				From	@Delete)
	Print FormatMessage ('Delete [App_DataDictionary].[ModelScripting]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[ScriptingTemplate]
	From	[App_DataDictionary].[ScriptingTemplate] T
			Left Join @Values S
			On	T.[TemplateId] = S.[TemplateId]
	Where	S.[TemplateId] is Null And
			T.[TemplateId] In (
				Select	[TemplateId]
				From	@Delete)
	Print FormatMessage ('Delete [App_DataDictionary].[ScriptingTemplate]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[TemplateId],
				[TemplateTitle],
				[TemplateDescription],
				[BreakOnScope],
				Convert(NVarChar(Max),[TransformScript]) As [TransformScript],
				[TransformAsText],
				[RootDirectory],
				[SolutionDirectory],
				[DocumentDirectory],
				[DocumentPrefix],
				[DocumentSuffix],
				[DocumentExtension],
				[ScriptDirectory],
				[ScriptPrefix],
				[ScriptSuffix],
				[ScriptExtension]
		From	@Values
		Except
		Select	[TemplateId],
				[TemplateTitle],
				[TemplateDescription],
				[BreakOnScope],
				Convert(NVarChar(Max),[TransformScript]) As [TransformScript],
				[TransformAsText],
				[RootDirectory],
				[SolutionDirectory],
				[DocumentDirectory],
				[DocumentPrefix],
				[DocumentSuffix],
				[DocumentExtension],
				[ScriptDirectory],
				[ScriptPrefix],
				[ScriptSuffix],
				[ScriptExtension]
		From	[App_DataDictionary].[ScriptingTemplate])
	Update [App_DataDictionary].[ScriptingTemplate]
		Set		[TemplateTitle] = S.[TemplateTitle],
				[TemplateDescription] = S.[TemplateDescription],
				[BreakOnScope] = S.[BreakOnScope],
				[TransformScript] = S.[TransformScript],
				[TransformAsText] = S.[TransformAsText],
				[RootDirectory] = S.[RootDirectory],
				[SolutionDirectory] = S.[SolutionDirectory],
				[DocumentDirectory] = S.[DocumentDirectory],
				[DocumentPrefix] = S.[DocumentPrefix],
				[DocumentSuffix] = S.[DocumentSuffix],
				[DocumentExtension] = S.[DocumentExtension],
				[ScriptDirectory] = S.[ScriptDirectory],
				[ScriptPrefix] = S.[ScriptPrefix],
				[ScriptSuffix] = S.[ScriptSuffix],
				[ScriptExtension] = S.[ScriptExtension]
		From	[App_DataDictionary].[ScriptingTemplate] T
				Inner Join [Delta] S
				On	T.[TemplateId] = S.[TemplateId]
	Print FormatMessage ('Update [App_DataDictionary].[ScriptingTemplate]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ScriptingTemplate] (
			[TemplateId],
			[TemplateTitle],
			[TemplateDescription],
			[BreakOnScope],
			[TransformScript],
			[TransformAsText],
			[RootDirectory],
			[SolutionDirectory],
			[DocumentDirectory],
			[DocumentPrefix],
			[DocumentSuffix],
			[DocumentExtension],
			[ScriptDirectory],
			[ScriptPrefix],
			[ScriptSuffix],
			[ScriptExtension])
	Select	S.[TemplateId],
			S.[TemplateTitle],
			S.[TemplateDescription],
			S.[BreakOnScope],
			S.[TransformScript],
			S.[TransformAsText],
			S.[RootDirectory],
			S.[SolutionDirectory],
			S.[DocumentDirectory],
			S.[DocumentPrefix],
			S.[DocumentSuffix],
			S.[DocumentExtension],
			S.[ScriptDirectory],
			S.[ScriptPrefix],
			S.[ScriptSuffix],
			S.[ScriptExtension]
	From	@Values S
			Left Join [App_DataDictionary].[ScriptingTemplate] T
			On	S.[TemplateId] = T.[TemplateId]
	Where	T.[TemplateId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[ScriptingTemplate]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ModelScripting] (
			[ModelId],
			[TemplateId])
	Select	@ModelId As [ModelId],
			S.[TemplateId]
	From	@Values S
			Left Join [App_DataDictionary].[ModelScripting] T
			On	S.[TemplateId] = T.[TemplateId] And
				@ModelId = T.[ModelId]
	Where	T.[TemplateId] Is Null
	Print FormatMessage ('Insert [App_DataDictionary].[ModelScripting]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));


	-- Tracking statement, example
	Print FormatMessage ('Set [App_DataDictionary].[ScriptingTemplate]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
/*
Begin Try;
	Begin Transaction;
	Set NoCount On;

	Declare @ModelID UniqueIdentifier = (Select [ModelId] from [App_DataDictionary].[Model] Where [ModelTitle] = 'Unit Test')
	Declare @Data [App_DataDictionary].[typeScriptingTemplate]

	Insert Into @Data ([TemplateId], [TemplateTitle])
	Values (NewId(),'Test Template')

	Exec [App_DataDictionary].[procSetScriptingTemplate] @ModelID = @ModelID, @Data = @Data

	Update @Data
	Set	[TransformScript] = '<Good/>'

	Exec [App_DataDictionary].[procSetScriptingTemplate] @ModelID = @ModelID, @Data = @Data

	Delete From @Data

	Exec [App_DataDictionary].[procSetScriptingTemplate] @ModelID = @ModelID, @Data = @Data

	Select	*
	From	[App_DataDictionary].[ScriptingTemplate]


	-- By default, throw and error and exit without committing
;	Throw 50000, 'Abort process, comment out this line when ready to actual Commit the transaction',255;
	
	Commit Transaction;
	Print 'Commit Issued';
End Try
Begin Catch
	Print FormatMessage ('*** Error Report: %s ***', Object_Name(@@ProcID));
	Print FormatMessage (' Message- %s', ERROR_MESSAGE());
	Print FormatMessage (' Number- %i', ERROR_NUMBER());
	Print FormatMessage (' Severity- %i', ERROR_SEVERITY());
	Print FormatMessage (' State- %i', ERROR_STATE());
	Print FormatMessage (' Procedure- %s', ERROR_PROCEDURE());
	Print FormatMessage (' Line- %i', ERROR_LINE());
	Print FormatMessage (' @@TranCount - %i', @@TranCount);
	Print FormatMessage (' @@NestLevel - %i', @@NestLevel);
	Print FormatMessage (' Original_Login - %s', Original_Login());
	Print FormatMessage (' Current_User - %s', Current_User);
	Print FormatMessage (' XAct_State - %i', XAct_State());
	Print '--- Debug Data ---';

	-- Rollback Transaction
	Print 'Rollback Issued';
	Rollback Transaction;
	--Throw;
End Catch;
*/