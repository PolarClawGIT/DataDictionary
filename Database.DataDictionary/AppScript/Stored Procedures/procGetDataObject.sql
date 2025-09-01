CREATE PROCEDURE [AppScript].[procGetDataObject]
		@ModelId UniqueIdentifier = Null,
		@DataSourceId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
/* Description: Performs Get on DataObject.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDatetime())

Select	[DataSourceId],
		[ObjectScope],
		[ObjectPath],
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
		(@ModelId is Null Or 
		 [DataSourceId] In (
			Select	[DataSourceId]
			From	[AppScript].[ScriptingModel]
			Where	@ModelId = [ModelId]))
Go
