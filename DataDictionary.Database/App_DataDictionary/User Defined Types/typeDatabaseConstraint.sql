CREATE TYPE [App_DataDictionary].[typeDatabaseConstraint] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[CatalogName]            SysName Null,
	[SchemaName]             SysName Null,
	[ConstraintName]         SysName Not Null,
	[TableName]              SysName Not Null,
	[ConstraintType]         NVarChar(60) Null
)
