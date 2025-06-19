CREATE PROCEDURE [AppScript].[procGetScriptingPath]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ScriptingPath.
*/
Select	D.[TemplateId],
		N.[NameSpace] As [PathName],
		D.[PathScope]
From	[AppScript].[ScriptingPath] D
		Cross Apply [AppModel].[funcGetNameSpaceById](D.[NameSpaceId]) N
		Left Join [AppScript].[ScriptingModel] M
		On	D.[TemplateId] = M.[TemplateId]
		Left Join [AppModel].[NameSpaceHierarchy] S
		On	D.[NameSpaceId] = S.[NameSpaceId] And
			M.[ModelId] = S.[ModelId]
Where	(@ModelId is Null or (@ModelId = M.[ModelId] And @ModelId = S.[ModelId])) And -- NameSpace must also be for the Model Specified
		(@TemplateId is Null or @TemplateId = D.[TemplateId])
GO
