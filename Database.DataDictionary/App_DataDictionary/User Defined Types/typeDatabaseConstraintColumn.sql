﻿CREATE TYPE [App_DataDictionary].[typeDatabaseConstraintColumn] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[ConstraintColumnId]     UniqueIdentifier Null,
	[DatabaseName]           SysName Null,
	[SchemaName]             SysName Null,
	[ConstraintName]         SysName Null,
	[TableName]              SysName Null,
	[ColumnName]             SysName Null,
	[OrdinalPosition]        Int Null,
	[ReferenceSchemaName]    SysName Null,
	[ReferenceTableName]     SysName Null,
	[ReferenceColumnName]    SysName Null
)
