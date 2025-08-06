CREATE PROCEDURE [AppScript].[procGetDataObject]
		@ModelId UniqueIdentifier = Null,
		@DataSourceId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
/* Description: Performs Get on DataItem.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDatetime())

Select	[DataSourceId],
		[DataPath],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn], 
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppScript].[DataObjectHs] D
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@DataSourceId is Null Or @DataSourceId = [DataSourceId]) And
		Exists(
			Select	1
			From	[AppScript].[ScriptingModel] -- TODO: For System_Time As of @AsOfUtcDate
			Where	D.[DataSourceId] = [DataSourceId] And
					(@ModelId is Null Or @ModelId = [ModelId]) And
					(@TemplateId is Null Or @TemplateId = [TemplateId]) And
					-- Temporal, multiple rows could be returned.
					((D.[SysStart] >= [SysStart] And D.[SysStart] < [SysEnd]) Or
					([SysStart] >= D.[SysStart] And [SysStart] < D.[SysEnd])))
Go
