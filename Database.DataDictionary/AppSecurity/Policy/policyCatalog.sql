CREATE SECURITY POLICY [AppSecurity].[policyCatalog]
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogAuthorization]([CatalogId], 1)
		ON [AppCatalog].[Catalog] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogAuthorization]([CatalogId], 0)
		ON [AppCatalog].[Catalog] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogAuthorization]([CatalogId], 1)
		ON [AppCatalog].[Catalog] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO

-- TODO: Cannot have an Indexed View and Policy on the same table.
-- Indexed view will need to be re-worked to detect duplicates but not fail.