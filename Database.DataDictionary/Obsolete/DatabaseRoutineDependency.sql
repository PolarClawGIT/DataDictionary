CREATE TABLE [App_DataDictionary].[DatabaseRoutineDependency]
(
	-- [INFORMATION_SCHEMA] does not contain a list of Columns referenced by Procedures or Functions.
	-- The best source of this is sys.dm_sql_referenced_entities.
	-- This is not a function that is 100% accurate.
	-- Simple routines return good information but the more complex the routine the less reliable the values are.
	-- The system function will also return exceptions for routines not specifically called for.
	[DependencyId]        UniqueIdentifier Not Null CONSTRAINT [DF_DatabaseRoutineDependencyId] DEFAULT (newid()),
	[RoutineId]           UniqueIdentifier Not Null,
	-- Source has Reference objects as null-able and may not be up to date.
	[ReferenceSchemaName] SysName Null,
	[ReferenceObjectName] SysName Null,
	[ReferenceObjectType] NVarChar(60) Null,
	[ReferenceColumnName] SysName Null,
	[IsCallerDependent]   Bit Null,
	[IsAmbiguous]         Bit Null,
	[IsSelected]          Bit Null,
	[IsUpdated]           Bit Null,
	[IsSelectAll]         Bit Null,
	[IsAllColumnsFound]   Bit Null,
	[IsInsertAll]         BIT Null,
	[IsIncomplete]        BIT NULL,
	-- TODO: Add System Version later once the schema is locked down. Not needed for Db Schema?
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DatabaseRoutineDependency_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DatabaseRoutineDependency_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DatabaseRoutineDependency_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	-- Because sys.dm_sql_referenced_entities can return Null for References Objects, a natural PK cannot be defined. Instead a Unique Key is used.
	CONSTRAINT [PK_DatabaseRoutineColumn] PRIMARY KEY CLUSTERED ([DependencyId] ASC),
	CONSTRAINT [FK_DatabaseRoutineDependencyRoutine] FOREIGN KEY ([RoutineId]) REFERENCES [App_DataDictionary].[DatabaseRoutine] ([RoutineId]),
)
GO
-- Nulls are possible for references. 
CREATE UNIQUE INDEX [UK_DatabaseRoutineDependency]
    ON [App_DataDictionary].[DatabaseRoutineDependency] ([ReferenceSchemaName] ASC, [ReferenceObjectName] ASC, [ReferenceColumnName] ASC, [RoutineId] ASC);
GO
