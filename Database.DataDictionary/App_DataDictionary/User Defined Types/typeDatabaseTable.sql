CREATE TYPE [App_DataDictionary].[typeDatabaseTable] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[CatalogName]            SysName          Null,
	[SchemaName]             SysName          Null,
	[TableName]              SysName          Null,
	[TableType]              NVarChar(60)     Null
)
