CREATE TYPE [AppCatalog].[typeCatalog] AS TABLE (
	-- TIP: This matches the C# DataTable structure and results of the Get method.
	[CatalogId]            UniqueIdentifier Null,
	[CatalogTitle]         [App_DataDictionary].[typeTitle] Null,
	[CatalogDescription]   [App_DataDictionary].[typeDescription] Null,
	[SourceServerName]     SysName          Null,
	[SourceDatabaseName]   SysName          Null,
	[SourceDate]           DateTime         Null,
	-- Temporal Data
	[CreatedOn]            DateTime2 (7) Null,
	[CreatedBy]            NVarChar(4000) Null,
	[RemovedOn]            DateTime2 (7) Null,
	[RemovedBy]            NVarChar(4000) Null,
	[IsInserted]           Bit Null,
	[IsUpdated]            Bit Null,
	[IsDeleted]            Bit Null,
	[IsCurrent]            Bit Null
);
