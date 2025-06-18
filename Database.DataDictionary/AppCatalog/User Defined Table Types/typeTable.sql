CREATE TYPE [AppCatalog].[typeTable] AS TABLE
(
	[CatalogId]            UniqueIdentifier Null,
	[TableId]              UniqueIdentifier Null,
	[DatabaseName]         SysName          Null,
	[SchemaName]           SysName          Null,
	[TableName]            SysName          Null,
	[TableType]            [AppGeneral].[typeObjectType] Null,
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
