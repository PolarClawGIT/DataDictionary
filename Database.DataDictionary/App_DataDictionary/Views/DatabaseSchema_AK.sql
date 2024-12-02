CREATE VIEW [App_DataDictionary].[DatabaseSchema_AK]
WITH SCHEMABINDING AS
-- Enforces Natural Key for the Database Schema
-- Key components exist over multiple tables. Keys on the tables only partially enforce unique names.
Select	C.[CatalogId],
		S.[SchemaId],
		C.[DatabaseName] As [DatabaseName],
		S.[SchemaName]
From	[AppCatalog].[Catalog] C
		Inner Join [AppCatalog].[Schema] S
		On	C.[CatalogId] = S.[CatalogId]
GO
/*
-- Policy and Indexed Views are not compatible.
CREATE UNIQUE CLUSTERED INDEX [PK_DatabaseSchema]
    ON [App_DataDictionary].[DatabaseSchema_AK]([SchemaId] ASC)
GO
CREATE UNIQUE INDEX [AK_DatabaseSchema]
    ON [App_DataDictionary].[DatabaseSchema_AK]([DatabaseName] ASC, [SchemaName] ASC, [CatalogId] ASC)
GO
*/