CREATE TABLE [App_DataDictionary].[DatabaseRoutineColumn]
(
	-- [INFORMATION_SCHEMA] does not contain a list of Columns referenced by Procedures or Functions.
	-- The best source of this is sys.dm_sql_referenced_entities.
	-- This is not a function that is 100% accurate.
	-- Simple routines return good information but the more complex the routine the less reliable the values are.
	-- The system function will also return exceptions for routines not specifically called for.
	[CatalogId]           UniqueIdentifier Not Null,
	[SchemaName]          SysName Not Null,
	[RoutineName]         SysName Not Null,
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

	-- Because sys.dm_sql_referenced_entities can return Null for References Objects, a natural PK cannot be defined. Instead a Unique Key is used.
	--CONSTRAINT [PK_DatabaseRoutineColumn] PRIMARY KEY CLUSTERED ([CatalogId] ASC, [SchemaName] ASC, [RoutineName] ASC, [RefrenceSchemaName] ASC, [RefrenceTableName] ASC, [RefrenceColumnName] ASC),
--	CONSTRAINT [FK_DatabaseRoutineColumnCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
	CONSTRAINT [FK_DatabaseRoutineColumnRoutine] FOREIGN KEY ([CatalogId], [SchemaName], [RoutineName]) REFERENCES [App_DataDictionary].[DatabaseRoutine] ([CatalogId], [SchemaName], [RoutineName]),
)
GO
CREATE UNIQUE CLUSTERED INDEX [PK_DatabaseRoutineColumn]
    ON [App_DataDictionary].[DatabaseRoutineColumn] ([CatalogId] ASC, [SchemaName] ASC, [RoutineName] ASC, [ReferenceSchemaName] ASC, [ReferenceObjectName] ASC, [ReferenceColumnName] ASC);
GO
