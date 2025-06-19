CREATE PROCEDURE [AppScript].[procGetScriptingNode]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ScriptingNode.
*/
Select	D.[NodeId],
		D.[TemplateId],
		D.[PropertyScope],
		D.[PropertyName],
		D.[NodeName],
		D.[NodeValueAs]
From	[AppScript].[ScriptingNode] D
		Left Join [AppScript].[ScriptingModel] M
		On	D.[TemplateId] = M.[TemplateId]
Where	(@ModelId is Null or @ModelId = M.[ModelId]) And
		(@TemplateId is Null or @TemplateId = D.[TemplateId])
GO
