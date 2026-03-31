CREATE PROCEDURE [AppCatalog].[procGetProperty]
		@ModelId        UniqueIdentifier = Null,
		@CatalogId      UniqueIdentifier = Null,
		@PropertyId     UniqueIdentifier = Null,
		@AsOfUtcDate    DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseExtendedProperty.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDateTime())

Select	[CatalogId],
		[PropertyId],
		[DatabaseName],
		[Level0Type],
		[Level0Name],
		[Level1Type],
		[Level1Name],
		[Level2Type],
		[Level2Name],
		[PropertyName],
		[PropertyValue],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn],
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppCatalog].[PropertyHs] For System_Time All
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@PropertyId is Null Or @PropertyId = [PropertyId]) And
		(@CatalogId is Null Or @CatalogId = [CatalogId]) And
		(@ModelId is Null Or [CatalogId] In (
			Select	[CatalogId]
			From	[AppModel].[CatalogModelHs]
			Where	@ModelId = [ModelId]))
Print FormatMessage ('Select: %i, %s', @@RowCount, Convert(VarChar,GetDate()));
GO