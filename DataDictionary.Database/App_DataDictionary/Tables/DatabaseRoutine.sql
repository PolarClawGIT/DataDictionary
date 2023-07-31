CREATE TABLE [App_DataDictionary].[DatabaseRoutine]
(
	[CatalogId]          UniqueIdentifier Not Null,
	[SchemaName]         SysName Not Null,
	[RoutineName]        SysName Not Null,
	[RoutineType]        NVarChar(60) Null, -- Known types: PROCEDURE, FUNCTION
	CONSTRAINT [PK_DatabaseRoutine] PRIMARY KEY CLUSTERED ([CatalogId] ASC, [SchemaName] ASC, [RoutineName] ASC),
	CONSTRAINT [FK_DatabaseRoutineCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
	CONSTRAINT [FK_DatabaseRoutineSchema] FOREIGN KEY ([CatalogId], [SchemaName]) REFERENCES [App_DataDictionary].[DatabaseSchema] ([CatalogId], [SchemaName]),
)
