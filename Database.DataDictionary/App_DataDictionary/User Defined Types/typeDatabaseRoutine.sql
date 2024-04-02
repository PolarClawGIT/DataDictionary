CREATE TYPE [App_DataDictionary].[typeDatabaseRoutine] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[RoutineId]              UniqueIdentifier Null,
	[DatabaseName]           SysName Null,
	[SchemaName]             SysName Null,
	[RoutineName]            SysName Null,
	[RoutineType]            [App_DataDictionary].[typeObjectSubType] Null
)
