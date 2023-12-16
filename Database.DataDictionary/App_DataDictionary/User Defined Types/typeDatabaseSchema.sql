CREATE TYPE [App_DataDictionary].[typeDatabaseSchema] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[SchemaId]               UniqueIdentifier Null,
	[DatabaseName]           SysName          Null,
	[SchemaName]             SysName          Null,
	[ScopeName]              [App_DataDictionary].[typeScopeName] Null
)
