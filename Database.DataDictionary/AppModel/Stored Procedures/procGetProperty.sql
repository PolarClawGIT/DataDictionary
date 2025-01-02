CREATE PROCEDURE [AppModel].[procGetProperty]
		@ModelId UniqueIdentifier = Null,
		@PropertyId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainProperty.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDateTime())

Select	[PropertyId],
		[PropertyTitle],
		[PropertyDescription],
		[IsCommon],
		[DataType],
		[PropertyData],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn],
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppModel].[PropertyHs]
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@PropertyId is Null Or @PropertyId = [PropertyId]) And
		(@ModelId is Null Or 
		 [IsCommon] = 1 Or
		 [PropertyId] In (
			Select	[PropertyId]
			From	[AppModel].[ModelPropertyHs]
			Where	@ModelId = [ModelId]))
GO