CREATE PROCEDURE [AppCatalog].[procGetRoutineParameter]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@RoutineId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0, -- History is included, @AsOfUtcDate and @IncludeDeleted is ignored
		@IncludeDeleted Bit = 0  -- Include Deleted rows. @AsOfUtcDate is ignored
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseRoutineParameter.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDateTime())

Select	[CatalogId],
		[RoutineParameterId],
		[DatabaseName],
		[SchemaName],
		[RoutineName],
		[RoutineType],
		[ParameterName],
		[OrdinalPosition],
		[DataType],
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
		[DomainCatalog],
		[DomainSchema],
		[DomainName],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn],
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppCatalog].[RoutineParameterHs] For System_Time All
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@RoutineId is Null Or @RoutineId = [RoutineId]) And
		(@CatalogId is Null Or @CatalogId = [CatalogId]) And
		(@ModelId is Null Or [CatalogId] In (
			Select	[CatalogId]
			From	[AppModel].[ModelCatalogAK]
			Where	@ModelId = [ModelId]))
GO