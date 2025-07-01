CREATE TYPE [AppCatalog].[udttConstraintColumn] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[ConstraintColumnId]     UniqueIdentifier Null,
	[DatabaseName]           SysName Null,
	[SchemaName]             SysName Null,
	[TableName]              SysName Null,
	[ConstraintName]         SysName Null,
	[ColumnName]             SysName Null,
	[OrdinalPosition]        Int Null,
	[ReferencedSchemaName]   SysName Null,
	[ReferencedTableName]    SysName Null,
	[ReferencedColumnName]   SysName Null,
	-- Temporal Data
	[CreatedOn]            DateTime2 (7) Null,
	[CreatedBy]            NVarChar(4000) Null,
	[RemovedOn]            DateTime2 (7) Null,
	[RemovedBy]            NVarChar(4000) Null,
	[IsInserted]           Bit Null,
	[IsUpdated]            Bit Null,
	[IsDeleted]            Bit Null,
	[IsCurrent]            Bit Null
)
