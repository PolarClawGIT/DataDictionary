CREATE TYPE [App_DataDictionary].[typeDatabaseCatalog] AS TABLE (
	-- TIP: This matches the C# DataTable structure
	[CatalogId]            UniqueIdentifier Null,
	[CatalogTitle]         [App_DataDictionary].[typeTitle] Null,
	[CatalogDescription]   [App_DataDictionary].[typeDescription] Null,
	[ScopeName]            [App_DataDictionary].[typeScopeName] Null,
	[SourceServerName]     SysName          Null,
	[SourceDatabaseName]   SysName          Null,
	[SourceDate]           DateTime         Null
);
