CREATE VIEW [App_DataDictionary].[DatabaseRoutineParameter_AK]
WITH SCHEMABINDING AS
-- Enforces Natural Key for the Database Routine Parameter
-- Key components exist over multiple tables. Keys on the tables only partially enforce unique names.
Select	C.[CatalogId],
		S.[SchemaId],
		T.[RoutineId],
		P.[ParameterId],
		C.[SourceDatabaseName] As [DatabaseName],
		S.[SchemaName],
		T.[RoutineName],
		P.[ParameterName]
From	[App_DataDictionary].[DatabaseCatalog] C
		Inner Join [App_DataDictionary].[DatabaseSchema] S
		On	C.[CatalogId] = S.[CatalogId]
		Inner Join [App_DataDictionary].[DatabaseRoutine] T
		On	S.[SchemaId] = T.[SchemaId]
		Inner Join [App_DataDictionary].[DatabaseRoutineParameter] P
		On	T.[RoutineId] = P.[RoutineId]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_DatabaseRoutineParameter]
    ON [App_DataDictionary].[DatabaseRoutineParameter_AK]([ParameterId])
GO
CREATE UNIQUE INDEX [AK_DatabaseRoutineParameter]
    ON [App_DataDictionary].[DatabaseRoutineParameter_AK]([DatabaseName] ASC, [SchemaName] ASC, [RoutineName] ASC, [ParameterName] ASC, [CatalogId] ASC)
GO