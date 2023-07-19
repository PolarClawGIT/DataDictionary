CREATE TYPE [App_DataDictionary].[typeDatabaseSchema] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[CatalogName]            SysName          Null,
	[SchemaName]             SysName          Null
)
