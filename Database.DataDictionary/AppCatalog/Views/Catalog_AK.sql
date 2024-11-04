CREATE VIEW [AppCatalog].[Catalog_AK]
WITH SCHEMABINDING AS
-- Enforces Natural Key for the Database
-- Not required. Exists for consistency.
Select	C.[CatalogId],
		C.[SourceDatabaseName] As [DatabaseName]
From	[AppCatalog].[Catalog] C
GO
CREATE UNIQUE CLUSTERED INDEX [PK_Catalog]
    ON [AppCatalog].[Catalog_AK]([CatalogId])
GO