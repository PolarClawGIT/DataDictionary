CREATE PROCEDURE [AppCatalog].[procGetRoutineColumn]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@RoutineId UniqueIdentifier = Null,
		@ColumnId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseRoutineColumn.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDateTime())

Select	[CatalogId],
		[RoutineColumnId],
		[DatabaseName],
		[SchemaName],
		[RoutineName],
		[RoutineType],
		[ColumnName],
		[OrdinalPosition],
		[ColumnDefault],
		[IsNullable],
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
		[IsIdentity],
		--[IsHidden],
		[IsComputed],
		[ComputedDefinition],
		--[GeneratedAlwayType],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn],
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppCatalog].[RoutineColumnHs] For System_Time All
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@RoutineId is Null Or @RoutineId = [RoutineId]) And
		(@ColumnId is Null Or @ColumnId = [RoutineColumnId]) And
		(@CatalogId is Null Or @CatalogId = [CatalogId]) And
		(@ModelId is Null Or [CatalogId] In (
			Select	[CatalogId]
			From	[AppModel].[CatalogModelHs]
			Where	@ModelId = [ModelId]))
Print FormatMessage ('Select: %i, %s', @@RowCount, Convert(VarChar,GetDate()));
GO