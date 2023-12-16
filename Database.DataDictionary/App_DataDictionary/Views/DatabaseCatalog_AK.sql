CREATE VIEW [App_DataDictionary].[DatabaseCatalog_AK]
WITH SCHEMABINDING AS
-- Enforces Natural Key for the Database
-- Not required. Exists for consistency.
Select	C.[CatalogId],
		C.[SourceDatabaseName] As [DatabaseName]
From	[App_DataDictionary].[DatabaseCatalog] C
GO
CREATE UNIQUE CLUSTERED INDEX [PK_DatabaseRoutineParameter]
    ON [App_DataDictionary].[DatabaseCatalog_AK]([CatalogId])
GO
CREATE UNIQUE INDEX [AK_DatabaseRoutineParameter]
    ON [App_DataDictionary].[DatabaseCatalog_AK]([DatabaseName] ASC, [CatalogId] ASC)
GO