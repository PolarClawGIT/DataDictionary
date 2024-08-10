CREATE TYPE [App_DataDictionary].[typeDatabaseReference] AS TABLE
(
	[CatalogId]               UniqueIdentifier Null,
	[ReferenceId]             UniqueIdentifier Null,
	[DatabaseName]            SysName Null,
	[SchemaName]              SysName Null,
	[ObjectName]              SysName Null,
	[ObjectType]		      [App_DataDictionary].[typeObjectType] Null,
	[ReferencedDatabaseName]  SysName Null,
	[ReferencedSchemaName]    SysName Null,
	[ReferencedObjectName]    SysName Null,
	[ReferencedColumnName]    SysName Null,
	[ReferencedType]          [App_DataDictionary].[typeObjectType] Null,
	[IsCallerDependent]       Bit Null,
	[IsAmbiguous]             Bit Null,
	[IsSelected]              Bit Null,
	[IsUpdated]               Bit Null,
	[IsSelectAll]             Bit Null,
	[IsAllColumnsFound]       Bit Null,
	[IsInsertAll]             Bit Null,
	[IsIncomplete]            Bit NULL
)
