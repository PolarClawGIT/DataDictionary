-- Replace with Check Constraint using a Function
CREATE VIEW [App_DataDictionary].[DatabaseCatalog_AK]
WITH SCHEMABINDING AS
-- Enforces Natural Key for the Database
-- Not required. Exists for consistency.
Select	C.[CatalogId],
		C.[SourceDatabaseName] As [DatabaseName]
From	[AppCatalog].[Catalog] C


GO
/*
-- Policy and Indexed Views are not compatible.
CREATE UNIQUE CLUSTERED INDEX [PK_Catalog]
    ON [AppCatalog].[Catalog_AK]([CatalogId])
GO
*/