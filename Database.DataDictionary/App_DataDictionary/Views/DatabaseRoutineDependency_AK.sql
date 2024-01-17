CREATE VIEW [App_DataDictionary].[DatabaseRoutineDependency_AK]
WITH SCHEMABINDING AS
Select	C.[CatalogId],
		S.[SchemaId],
		R.[RoutineId],
		D.[DependencyId],
		C.[SourceDatabaseName] As [DatabaseName],
		S.[SchemaName],
		R.[RoutineName],
		D.[ReferenceSchemaName],
		D.[ReferenceObjectName],
		D.[ReferenceColumnName]
From	[App_DataDictionary].[DatabaseCatalog] C
		Inner Join [App_DataDictionary].[DatabaseSchema] S
		On	C.[CatalogId] = S.[CatalogId]
		Inner Join [App_DataDictionary].[DatabaseRoutine] R
		On	S.[SchemaId] = R.[SchemaId]
		Inner Join [App_DataDictionary].[DatabaseRoutineDependency] D
		On	R.[RoutineId] = D.[RoutineId]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_DatabaseRoutineDependency]
    ON [App_DataDictionary].[DatabaseRoutineDependency_AK]([DependencyId])
GO
CREATE UNIQUE INDEX [AK_DatabaseRoutineDependency]
    ON [App_DataDictionary].[DatabaseRoutineDependency_AK]([DatabaseName] ASC, [SchemaName] ASC, [RoutineName] ASC, [ReferenceSchemaName] ASC, [ReferenceObjectName] ASC, [ReferenceColumnName] ASC)
GO