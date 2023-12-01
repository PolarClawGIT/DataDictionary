CREATE TYPE [App_DataDictionary].[typeDatabaseRoutine] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[SchemaId]               UniqueIdentifier Null,
	[RoutineId]              UniqueIdentifier Null,
	[DatabaseName]           SysName Null,
	[SchemaName]             SysName Null,
	[RoutineName]            SysName Null,
	[ScopeName]              [App_DataDictionary].[typeScopeName] Null,
	[RoutineType]            NVarChar(60) Null
)
