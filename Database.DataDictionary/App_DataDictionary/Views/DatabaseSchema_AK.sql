CREATE VIEW [App_DataDictionary].[DatabaseSchema_AK]
WITH SCHEMABINDING AS
-- Enforces Natural Key for the Database Schema
-- Key components exist over multiple tables. Keys on the tables only partially enforce unique names.
Select	C.[CatalogId],
		S.[SchemaId],
		C.[SourceDatabaseName] As [DatabaseName],
		S.[SchemaName]
From	[App_DataDictionary].[DatabaseCatalog] C
		Inner Join [App_DataDictionary].[DatabaseSchema] S
		On	C.[CatalogId] = S.[CatalogId]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_DatabaseSchema]
    ON [App_DataDictionary].[DatabaseSchema_AK]([SchemaId] ASC)
GO
CREATE UNIQUE INDEX [AK_DatabaseSchema]
    ON [App_DataDictionary].[DatabaseSchema_AK]([DatabaseName] ASC, [SchemaName] ASC, [CatalogId] ASC)
GO