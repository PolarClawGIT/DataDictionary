CREATE TYPE [App_DataDictionary].[typeDatabaseTable] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[TableId]                UniqueIdentifier Null,
	[DatabaseName]           SysName          Null,
	[SchemaName]             SysName          Null,
	[TableName]              SysName          Null,
	[TableType]              [App_DataDictionary].[typeObjectType] Null
)
