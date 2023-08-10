CREATE TYPE [App_DataDictionary].[typeDatabaseConstraintColumn] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[CatalogName]            SysName Null,
	[SchemaName]             SysName Null,
	[ConstraintName]         SysName Null,
	[ReferenceSchemaName]    SysName Null,
	[ReferenceTableName]     SysName Null,
	[ReferenceColumnName]    SysName Null,
	[OrdinalPosition]        Int Null
)
