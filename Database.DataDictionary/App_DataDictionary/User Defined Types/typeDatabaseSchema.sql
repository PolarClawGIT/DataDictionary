CREATE TYPE [App_DataDictionary].[typeDatabaseSchema] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[DatabaseName]           SysName          Null,
	[SchemaName]             SysName          Null
)
