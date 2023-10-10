CREATE TYPE [App_DataDictionary].[typeDatabaseRoutine] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[DatabaseName]           SysName Null,
	[SchemaName]             SysName Null,
	[RoutineName]            SysName Null,
	[RoutineType]            NVarChar(60) Null
)
