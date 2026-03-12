Begin Try;
	Begin Transaction;
	Set NoCount On;

	Declare @ModelId UniqueIdentifier = (Select [ModelId] From [AppModel].[Model] Where [ModelTitle] = 'Unit Test')

	-- Testing [AppScript].[Template] --
	Declare	@TemplateId UniqueIdentifier = Null,
			@Template [AppScript].[udttTemplate],
			@Empty [AppScript].[udttTemplate]

	Print '-- Validate [udttTemplate] --'
	Insert Into @Template
	Exec [AppScript].[procGetTemplate]
	Print FormatMessage ('udttTemplate: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into @Template ([TemplateId], [TemplateTitle], [TemplateDescription])
	Values (@TemplateId, 'Unit Test', 'Testing'),
			(NewId(),'Test 2', Null)

	Print '--- Add Template Without Model or ID --'
	Exec [AppScript].[procSetTemplate] @ModelId = Null, @TemplateId = @TemplateId, @Data = @Template

	Print '-- Delete Template --'
	Set @TemplateId = (Select [TemplateId] From [AppScript].[Template] Where [TemplateTitle] In (Select [TemplateTitle] From @Template Where [TemplateId] is Null))
	Exec [AppScript].[procSetTemplate] @ModelId = Null, @TemplateId = @TemplateId, @Data = @Empty
	
	Print '-- Add Template to Model --'
	Set @TemplateId = newId()
	Update @Template Set [TemplateId] = @TemplateId Where [TemplateId] is Null

	Exec [AppScript].[procSetTemplate] @ModelId = Null, @TemplateId = Null, @Data = @Template
	Exec [AppScript].[procSetTemplate] @ModelId = @ModelId, @TemplateId = Null, @Data = @Template

	Print '-- Remove Template From Model --'
	Exec [AppScript].[procSetTemplate] @ModelId = @ModelId, @TemplateId = Null, @Data = @Empty

	Print '-- Update Template --'
	Update @Template Set [TemplateDescription] = 'Update Testing'
	Exec [AppScript].[procSetTemplate] @ModelId = Null, @TemplateId = Null, @Data = @Template

	Print '-- Remove Template --'
	Exec [AppScript].[procSetTemplate] @ModelId = Null, @TemplateId = @TemplateId, @Data = @Empty


	Print '-- Setup Template (Expected state) --'
	Exec [AppScript].[procSetTemplate] @ModelId = @ModelId, @TemplateId = @TemplateId, @Data = @Template

	-- Testing [AppScript].[SchemaDefinition] --
	Declare	@Schema [AppScript].[udttSchemaDefinition],
			@SchemaId UniqueIdentifier = NewId()

	Print '-- Validate udttSchemaDefinition --'
	Insert Into @Schema
	Exec [AppScript].[procGetSchemaDefinition]
	Print FormatMessage ('udttSchemaDefinition: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into @Schema ([SchemaId], [TemplateId], [SchemaTitle])
	Values	(@SchemaId, @TemplateId, 'Unit Test Schema')

	Print '-- Setup SchemaDefinition (Expected state) --'
	Exec [AppScript].[procSetSchemaDefinition] @ModelId = @ModelId, @TemplateId = @TemplateId, @Data = @Schema

	-- Testing [AppScript].[SchemaNode]
	Declare @Node [AppScript].[udttSchemaNode],
			@NodeId01 UniqueIdentifier = NewId(),
			@NodeId02 UniqueIdentifier = NewId()

	Print '-- Validate udttSchemaNode --'
	Insert Into @Node
	Exec [AppScript].[procGetSchemaNode]
	Print FormatMessage ('udttSchemaNode: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into @Node ([NodeId], [SchemaId], [TemplateId], [NodeName])
	Values	(@NodeId01, @SchemaId, @TemplateId, 'UnitTestNode'),
			(@NodeId02, @SchemaId, @TemplateId, 'UnitTestNodeChild')

	Print '-- Setup SchemaNode (Expected state) --'
	Exec [AppScript].[procSetSchemaNode] @ModelId = @ModelId, @TemplateId = @TemplateId, @Data = @Node

	-- Testing [AppScript].[SchemaNodeOwner]
	Declare @Owner [AppScript].[udttSchemaNodeOwner]

	Print '-- Validate udttSchemaNodeOwner --'
	Insert Into @Owner
	Exec [AppScript].[procGetSchemaNodeOwner]
	Print FormatMessage ('udttSchemaNodeOwner: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into @Owner ([NodeId], [NodeOwnerId], [SchemaId], [TemplateId])
	Values (@NodeId02, @NodeId01, @SchemaId, @TemplateId)

	Print '-- Setup SchemaDefinition (Expected state) --'
	Exec [AppScript].[procSetSchemaNodeOwner] @ModelId = @ModelId, @TemplateId = @TemplateId, @Data = @Owner



	--[AppScript].[udttSchemaNodeOwner]

	-- Check Results
	Select	'[TemplateModel]', *
	From	[AppScript].[TemplateModel]

	Select	'[TemplateHS]', *
	From	[AppScript].[TemplateHS] For System_Time All

	Select	'[SchemaDefinitionHS]', *
	From	[AppScript].[SchemaDefinitionHS] For System_Time All

	Select	'[SchemaNodeHS]', *
	From	[AppScript].[SchemaNodeHS] For System_Time All

	Select	'[SchemaNodeOwnerHS]', *
	From	[AppScript].[SchemaNodeOwnerHS] For System_Time All

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
	If(ERROR_NUMBER()<> 50000) Throw;
End Catch;