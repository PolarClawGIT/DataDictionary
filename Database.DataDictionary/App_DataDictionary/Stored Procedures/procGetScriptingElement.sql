CREATE PROCEDURE [App_DataDictionary].[procGetScriptingElement]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ScriptingElement.
*/
Select	D.[ElementId],
		D.[TemplateId],
		D.[PropertyScope],
		D.[PropertyName],
		D.[AsElement],
		D.[ElementName],
		D.[ElementType],
		D.[DataAs]
From	[App_DataDictionary].[ScriptingElement] D
		Left Join [App_DataDictionary].[ModelScripting] M
		On	D.[TemplateId] = M.[TemplateId]
Where	(@ModelId is Null or @ModelId = M.[ModelId]) And
		(@TemplateId is Null or @TemplateId = D.[TemplateId])
GO
