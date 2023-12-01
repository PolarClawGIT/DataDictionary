CREATE TYPE [App_DataDictionary].[typeDatabaseConstraint] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[SchemaId]               UniqueIdentifier Null,
	[ConstraintId]           UniqueIdentifier Null,
	[DatabaseName]           SysName Null,
	[SchemaName]             SysName Null,
	[ConstraintName]         SysName Null,
	[TableName]              SysName Null,
	[ScopeName]              [App_DataDictionary].[typeScopeName] Null,
	[ConstraintType]         NVarChar(60) Null
)
