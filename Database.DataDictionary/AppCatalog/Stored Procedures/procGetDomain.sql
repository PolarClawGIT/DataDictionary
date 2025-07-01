CREATE PROCEDURE [AppCatalog].[procGetDomain]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@DomainId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseDomain.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDateTime())

Select	[CatalogId],
		[DomainId],
		[DatabaseName],
		[SchemaName],
		[DomainName],
		[DataType],
		[DomainDefault],
		[CharacterMaximumLength],
		[CharacterOctetLength],
		[NumericPrecision],
		[NumericPrecisionRadix],
		[NumericScale],
		[DateTimePrecision],
		[CharacterSetCatalog],
		[CharacterSetSchema],
		[CharacterSetName],
		[CollationCatalog],
		[CollationSchema],
		[CollationName],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn],
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppCatalog].[DomainHs] For System_Time All
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@DomainId is Null Or @DomainId = [DomainId]) And
		(@CatalogId is Null Or @CatalogId = [CatalogId]) And
		(@ModelId is Null Or [CatalogId] In (
			Select	[CatalogId]
			From	[AppModel].[CatalogModelHs]
			Where	@ModelId = [ModelId]))

GO