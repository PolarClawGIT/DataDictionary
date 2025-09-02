CREATE PROCEDURE [AppScript].[procGetTemplateInput]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@DataSourceId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
/* Description: Performs Get on TemplateInput.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDatetime())

Select	[TemplateId],
		[DataSourceId],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn], 
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppScript].[TemplateInputHs] D
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@TemplateId is Null Or @TemplateId = [TemplateId]) And
		(@DataSourceId is Null Or @DataSourceId = [DataSourceId]) And
		(@ModelId is Null Or @ModelId = [ModelId])
Go
