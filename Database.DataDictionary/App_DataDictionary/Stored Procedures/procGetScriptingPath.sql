CREATE PROCEDURE [App_DataDictionary].[procGetScriptingPath]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ScriptingPath.
*/
Select	D.[TemplateId],
		N.[NameSpace] As [PathName],
		D.[PathScope]
From	[App_DataDictionary].[ScriptingPath] D
		Cross Apply [App_DataDictionary].[funcGetNameSpace](D.[NameSpaceId]) N
		Left Join [App_DataDictionary].[ModelScripting] M
		On	D.[TemplateId] = M.[TemplateId]
		Left Join [App_DataDictionary].[ModelNameSpace] S
		On	D.[NameSpaceId] = S.[NameSpaceId] And
			M.[ModelId] = S.[ModelId]
Where	(@ModelId is Null or (@ModelId = M.[ModelId] And @ModelId = S.[ModelId])) And -- NameSpace must also be for the Model Specified
		(@TemplateId is Null or @TemplateId = D.[TemplateId])
GO
