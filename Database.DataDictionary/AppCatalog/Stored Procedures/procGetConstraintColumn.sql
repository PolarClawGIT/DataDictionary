CREATE PROCEDURE [AppCatalog].[procGetConstraintColumn]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@ConstraintId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseConstraintColumn.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDateTime())

Select	[CatalogId],
		[ConstraintColumnId],
		[DatabaseName],
		[SchemaName],
		[TableName],
		[ConstraintName],
		[ColumnName],
		[OrdinalPosition],
		[ReferencedSchemaName],
		[ReferencedTableName],
		[ReferencedColumnName],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn],
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppCatalog].[ConstraintColumnHs] For System_Time All
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@ConstraintId is Null Or @ConstraintId = [ConstraintId]) And
		(@CatalogId is Null Or @CatalogId = [CatalogId]) And
		(@ModelId is Null Or [CatalogId] In (
			Select	[CatalogId]
			From	[AppModel].[CatalogModelHs]
			Where	@ModelId = [ModelId]))
GO
