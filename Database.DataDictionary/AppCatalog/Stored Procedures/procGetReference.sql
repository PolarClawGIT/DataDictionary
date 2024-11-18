CREATE PROCEDURE [AppCatalog].[procGetReference]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@ReferenceId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0, -- History is included, @AsOfUtcDate and @IncludeDeleted is ignored
		@IncludeDeleted Bit = 0  -- Include Deleted rows. @AsOfUtcDate is ignored
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseReference.
*/
Select	[CatalogId],
		[ReferenceId],
		[DatabaseName],
		[SchemaName],
		[ObjectName],
		[ReferencedDatabaseName],
		[ReferencedSchemaName],
		[ReferencedObjectName],
		[ReferencedColumnName],
		[ReferencedType],
		[IsCallerDependent],
		[IsAmbiguous],
		[IsSelected],
		[IsModified],
		[IsSelectAll],
		[IsAllColumnsFound],
		[IsInsertAll],
		[IsIncomplete],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn],
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppCatalog].[ReferenceHs] For System_Time All
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@ReferenceId is Null Or @ReferenceId = [ReferenceId]) And
		(@CatalogId is Null Or @CatalogId = [CatalogId]) And
		(@ModelId is Null Or [CatalogId] In (
			Select	[CatalogId]
			From	[AppModel].[ModelCatalogAK]
			Where	@ModelId = [ModelId]))
GO