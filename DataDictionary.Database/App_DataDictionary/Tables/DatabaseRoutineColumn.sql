CREATE TABLE [App_DataDictionary].[DatabaseRoutineColumn]
(
	-- [INFORMATION_SCHEMA] does not contain a list of Columns referenced by Procedures or Functions.
	-- The best source of this is sys.dm_sql_referenced_entities.
	-- This is not a function that is 100% accurate.
	-- Simple routines return good information but the more complex the routine the less reliable the values are.
	-- The system function will also return exceptions for routines not specifically called for.
	[CatalogId]          UniqueIdentifier Not Null,
	[SchemaName]         SysName Not Null,
	[RoutineName]        SysName Not Null,
	[RefrenceSchemaName] SysName Null,
	[RefrenceTableName]  SysName Null, -- Expect this to always be a Table or View
	[RefrenceColumnName] SysName Null,
	[RefrenceObjectType] NVarChar(60) Null, -- Known types: USER_TABLE
	[IsCallerDependent]  Bit Null,
	[IsAmbiguous]        Bit Null,
	[IsSelected]         Bit Null,
	[IsUpdated]          Bit Null,
	[IsSelectAll]        Bit Null,
	[IsAllColumnsFound]  Bit Null,
	[IsInsertAll]        BIT Null,
	[IsIncomplete]       BIT NULL,

	-- Because sys.dm_sql_referenced_entities can return Null for References Objects, a natural PK cannot be defined. Instead a Unique Key is used.
	--CONSTRAINT [PK_DatabaseRoutineColumn] PRIMARY KEY CLUSTERED ([CatalogId] ASC, [SchemaName] ASC, [RoutineName] ASC, [RefrenceSchemaName] ASC, [RefrenceTableName] ASC, [RefrenceColumnName] ASC),
--	CONSTRAINT [FK_DatabaseRoutineColumnCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
	CONSTRAINT [FK_DatabaseRoutineColumnRoutine] FOREIGN KEY ([CatalogId], [SchemaName], [RoutineName]) REFERENCES [App_DataDictionary].[DatabaseRoutine] ([CatalogId], [SchemaName], [RoutineName]),
	CONSTRAINT [FK_DatabaseRoutineColumnTableColumn] FOREIGN KEY ([CatalogId], [RefrenceSchemaName], [RefrenceTableName], [RefrenceColumnName]) REFERENCES [App_DataDictionary].[DatabaseTableColumn] ([CatalogId], [SchemaName], [TableName], [ColumnName])
)
GO
CREATE UNIQUE CLUSTERED INDEX [PK_DatabaseRoutineColumn]
    ON [App_DataDictionary].[DatabaseRoutineColumn] ([CatalogId] ASC, [SchemaName] ASC, [RoutineName] ASC, [RefrenceSchemaName] ASC, [RefrenceTableName] ASC, [RefrenceColumnName] ASC);
GO
