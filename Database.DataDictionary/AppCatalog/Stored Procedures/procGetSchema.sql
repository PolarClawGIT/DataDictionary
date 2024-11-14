CREATE PROCEDURE [AppCatalog].[procGetSchema]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@SchemaId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included, @AsOfUtcDate and @IncludeDeleted is ignored
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on Catalog Schema.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDateTime())

Select	[CatalogId],
		[SchemaId],
		[DatabaseName],
		[SchemaName],
		[CreatedBy],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn],
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppCatalog].[SchemaHs] For System_Time All
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@SchemaId is Null Or @SchemaId = [SchemaId]) And
		(@CatalogId is Null Or @CatalogId = [CatalogId]) And
		(@ModelId is Null Or [CatalogId] In (
			Select	[CatalogId]
			From	[App_DataDictionary].[ModelCatalog]
			Where	@ModelId = [ModelId]))
GO