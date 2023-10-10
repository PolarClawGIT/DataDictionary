CREATE TYPE [App_DataDictionary].[typeDatabaseCatalog] AS TABLE (
	-- TIP: This matches the C# DataTable structure
	[CatalogId]            UniqueIdentifier Null,
	[CatalogTitle]         [App_DataDictionary].[typeTitle] Null,
	[CatalogDescription]   [App_DataDictionary].[typeDescription] Null,
	[SourceServerName]     SysName          Null,
	[SourceDatabaseName]   SysName          Null,
	[SourceDate]           DateTime         Null,
    [SysStart]             DATETIME2 (7)    NULL
);
