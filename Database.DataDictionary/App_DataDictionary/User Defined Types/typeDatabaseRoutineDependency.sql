CREATE TYPE [App_DataDictionary].[typeDatabaseRoutineDependency] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[SchemaId]               UniqueIdentifier Null,
	[RoutineId]              UniqueIdentifier Null,
	[DependencyId]           UniqueIdentifier Null,
	[DatabaseName]           SysName Null,
	[SchemaName]             SysName Null,
	[RoutineName]            SysName Null,
	[ReferenceSchemaName]    SysName Null,
	[ReferenceObjectName]    SysName Null,
	[ReferenceObjectType]    NVarChar(60) Null,
	[ReferenceColumnName]    SysName Null,
	[IsCallerDependent]      Bit Null,
	[IsAmbiguous]            Bit Null,
	[IsSelected]             Bit Null,
	[IsUpdated]              Bit Null,
	[IsSelectAll]            Bit Null,
	[IsAllColumnsFound]      Bit Null,
	[IsInsertAll]            BIT Null,
	[IsIncomplete]           BIT NULL
)
