CREATE PROCEDURE [AppScript].[procGetTemplate]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on Template.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDatetime())

Select	[TemplateId],
		[TemplateTitle],
		[TemplateDescription],
		[BreakOnScope],
		Convert(NVarChar(Max), [TransformScript]) As [TransformScript],
		[RootDirectory],
		[DocumentDirectory],
		[DocumentPrefix],
		[DocumentSuffix],
		[DocumentExtension],
		[ScriptAs],
		[ScriptDirectory],
		[ScriptPrefix],
		[ScriptSuffix],
		[ScriptExtension],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn], 
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppScript].[TemplateHs] D
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@TemplateId is Null Or @TemplateId = [TemplateId]) And
		(@ModelId is Null Or @ModelId In (
			Select	[ModelId]
			From	[AppScript].[ScriptingModel] -- TODO: For System_Time As of @AsOfUtcDate
			Where	D.[TemplateId] = [TemplateId]))
GO