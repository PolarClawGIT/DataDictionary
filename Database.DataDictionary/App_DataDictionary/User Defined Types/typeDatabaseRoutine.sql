CREATE TYPE [App_DataDictionary].[typeDatabaseRoutine] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[DatabaseName]           SysName Null,
	[SchemaName]             SysName Null,
	[RoutineName]            SysName Null,
	[ScopeName]              [App_DataDictionary].[typeScopeName] Null,
	[RoutineType]            NVarChar(60) Null
)
