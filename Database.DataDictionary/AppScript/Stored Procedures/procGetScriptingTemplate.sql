CREATE PROCEDURE [AppScript].[procGetScriptingTemplate]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@TemplateTitle [AppGeneral].[typeTitle] = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ScriptingTemplate.
*/
Select	D.[TemplateId],
		D.[TemplateTitle],
		D.[TemplateDescription],
		D.[BreakOnScope],
		D.[TransformScript],
		D.[RootDirectory],
		D.[DocumentDirectory],
		D.[DocumentPrefix],
		D.[DocumentSuffix],
		D.[DocumentExtension],
		D.[ScriptAs],
		D.[ScriptDirectory],
		D.[ScriptPrefix],
		D.[ScriptSuffix],
		D.[ScriptExtension]
From	[AppScript].[ScriptingTemplate] D
		Left Join [AppScript].[ScriptingModel] M
		On	D.[TemplateId] = M.[TemplateId]
Where	(@ModelId is Null Or @ModelId = M.[ModelId]) And
		(@TemplateId is Null Or @TemplateId = D.[TemplateId]) And
		(@TemplateTitle is Null Or @TemplateTitle = D.[TemplateTitle])
GO