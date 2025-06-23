CREATE TYPE [AppCatalog].[udttRoutine] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[RoutineId]              UniqueIdentifier Null,
	[DatabaseName]           SysName Null,
	[SchemaName]             SysName Null,
	[RoutineName]            SysName Null,
	[RoutineType]            [AppGeneral].[uddtObjectType] Null,
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
