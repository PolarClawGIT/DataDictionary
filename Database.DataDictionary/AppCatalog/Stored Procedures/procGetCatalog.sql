CREATE PROCEDURE [AppCatalog].[procGetCatalog]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0, -- History is included, @AsOfUtcDate and @IncludeDeleted is ignored
		@IncludeDeleted Bit = 0  -- Include Deleted rows. @AsOfUtcDate is ignored
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on Catalog.
*/

Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysDatetime())

Select	[CatalogId],
		[DatabaseName],
		[ModifiedBy],
		[SysStart] As [ModifiedOn],
		Convert(Bit, IIF([PriorDate] is Null Or [PriorDate] <> [SysStart],1,0)) As [IsInserted],
		Convert(Bit, IIF([PriorDate] = [SysStart], 1, 0)) As [IsUpdated],
		Convert(Bit, IIF([NextDate] <> [SysEnd], 1, 0)) As [IsDeleted],
		Convert(Bit, IIF(@AsOfUtcDate >= [SysStart] And @AsOfUtcDate < [SysEnd],1,0)) As [IsCurrent]
From	[AppCatalog].[CatalogAK] For System_Time All
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@CatalogId is Null Or @CatalogId = [CatalogId]) And
		(@ModelId is Null Or [CatalogId] In (
			Select	[CatalogId]
			From	[App_DataDictionary].[ModelCatalog]
			Where	@ModelId = [ModelId]))
GO