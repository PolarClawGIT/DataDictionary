CREATE TYPE [App_DataDictionary].[typeDatabaseConstraint] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[DatabaseName]           SysName Null,
	[SchemaName]             SysName Null,
	[ConstraintName]         SysName Null,
	[TableName]              SysName Null,
	[ScopeName]              [App_DataDictionary].[typeScopeName] Null,
	[ConstraintType]         NVarChar(60) Null
)
