CREATE TYPE [App_DataDictionary].[typeDatabaseConstraint] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[CatalogName]            SysName Null,
	[SchemaName]             SysName Null,
	[ConstraintName]         SysName Null,
	[TableName]              SysName Null,
	[ConstraintType]         NVarChar(60) Null
)
