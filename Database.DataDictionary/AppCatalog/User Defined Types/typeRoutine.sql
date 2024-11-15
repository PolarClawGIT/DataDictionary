CREATE TYPE [AppCatalog].[typeRoutine] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[RoutineId]              UniqueIdentifier Null,
	[DatabaseName]           SysName Null,
	[SchemaName]             SysName Null,
	[RoutineName]            SysName Null,
	[RoutineType]            [App_DataDictionary].[typeObjectType] Null
)
