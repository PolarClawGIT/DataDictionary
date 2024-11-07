CREATE VIEW [App_DataDictionary].[DatabaseRoutine_AK]
WITH SCHEMABINDING AS
-- Enforces Natural Key for the Database Routine (Procedure and Functions)
-- Key components exist over multiple tables. Keys on the tables only partially enforce unique names.
Select	C.[CatalogId],
		S.[SchemaId],
		T.[RoutineId],
		C.[SourceDatabaseName] As [DatabaseName],
		S.[SchemaName],
		T.[RoutineName],
		T.[RoutineType]
From	[AppCatalog].[Catalog] C
		Inner Join [AppCatalog].[Schema] S
		On	C.[CatalogId] = S.[CatalogId]
		Inner Join [App_DataDictionary].[DatabaseRoutine] T
		On	S.[SchemaId] = T.[SchemaId]
GO
/*
-- Policy and Indexed Views are not compatible.
CREATE UNIQUE CLUSTERED INDEX [PK_DatabaseRoutine]
    ON [App_DataDictionary].[DatabaseRoutine_AK]([RoutineId])
GO
CREATE UNIQUE INDEX [AK_DatabaseRoutine]
    ON [App_DataDictionary].[DatabaseRoutine_AK]([DatabaseName] ASC, [SchemaName] ASC, [RoutineName] ASC, [CatalogId] ASC)
GO
*/