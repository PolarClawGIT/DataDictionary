CREATE TYPE [App_DataDictionary].[typeDatabaseReference] AS TABLE
(
	[CatalogId]               UniqueIdentifier Null,
	[ReferenceId]             UniqueIdentifier Null,
	[DatabaseName]            SysName Null,
	[SchemaName]              SysName Null,
	[ObjectName]              SysName Null,
	[ReferencingType]	      [App_DataDictionary].[typeObjectSubType] Null,
	[ReferencedServerName]    SysName Null,
	[ReferencedDatabaseName]  SysName Null,
	[ReferencedSchemaName]    SysName Null,
	[ReferencedObjectName]    SysName Null,
	[ReferencedColumnName]    SysName Null,
	[ReferencedType]          [App_DataDictionary].[typeObjectSubType] Null,
	[IsCallerDependent]       Bit Null,
	[IsAmbiguous]             Bit Null,
	[IsSelected]              Bit Null,
	[IsUpdated]               Bit Null,
	[IsSelectAll]             Bit Null,
	[IsAllColumnsFound]       Bit Null,
	[IsInsertAll]             Bit Null,
	[IsIncomplete]            Bit NULL
)
