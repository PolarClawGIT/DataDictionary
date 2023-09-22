CREATE TYPE [App_DataDictionary].[typeDatabaseCatalog] AS TABLE (
	-- TIP: This matches the C# DataTable structure
	[CatalogId]            UniqueIdentifier Null,
	[CatalogName]          SysName          Null,
	[SourceServerName]     SysName          Null,
    [SysStart]             DATETIME2 (7)    NULL
);
