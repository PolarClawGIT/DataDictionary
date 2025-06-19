CREATE PROCEDURE [AppScript].[procGetScriptingAttribute]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ScriptingAttribute.
*/
Select	[AttributeId],
		[NodeId],
		[TemplateId],
		[TemplateTitle],
		[AttributeName],
		[AttributeValue],
		[PropertyId],
		-- Temporal Data
		[SysStart],
		[SysEnd],
		[CreatedOn],
		[CreatedBy],
		[RemovedOn],
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppScript].[ScriptingAttributeHs] D -- TODO: For System_Time All D
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@TemplateId is Null Or @TemplateId = [TemplateId]) And
		(@AttributeId is Null Or @AttributeId = [AttributeId]) And
		(@ModelId is Null Or @ModelId In (
			Select	[ModelId]
			From	[AppScript].[ScriptingModel] -- TODO: For System_Time As of @AsOfUtcDate
			Where	D.[TemplateId] = [TemplateId]))
GO
