CREATE TYPE [AppCatalog].[typeRoutine] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[RoutineId]              UniqueIdentifier Null,
	[DatabaseName]           SysName Null,
	[SchemaName]             SysName Null,
	[RoutineName]            SysName Null,
	[RoutineType]            [App_DataDictionary].[typeObjectType] Null,
	-- Temporal Data
	[CreatedOn]              DateTime2 (7) Null,
	[CreatedBy]              NVarChar(4000) Null,
	[RemovedOn]              DateTime2 (7) Null,
	[RemovedBy]              NVarChar(4000) Null,
	[IsInserted]             Bit Null,
	[IsUpdated]              Bit Null,
	[IsDeleted]              Bit Null,
	[IsCurrent]              Bit Null
)
