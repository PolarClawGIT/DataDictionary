CREATE PROCEDURE [App_DataDictionary].[procGetScriptingAttribute]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ScriptingAttribute.
*/
Select	P.[TemplateId],
		D.[NodeId],
		D.[AttributeId],
		D.[AttributeName],
		D.[AttributeValue],
		D.[PropertyId],
		D.[AsCData]
From	[App_DataDictionary].[ScriptingAttribute] D
		Inner Join [App_DataDictionary].[ScriptingNode] P
		On	D.[NodeId] = P.[NodeId]
		Left Join [App_DataDictionary].[ModelScripting] M
		On	P.[TemplateId] = M.[TemplateId]
Where	(@ModelId is Null or @ModelId = M.[ModelId]) And
		(@TemplateId is Null or @TemplateId = P.[TemplateId])
GO
