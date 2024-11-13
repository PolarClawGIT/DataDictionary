CREATE SECURITY POLICY [AppSecurity].[policyCatalogProperty]
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogPropertyAuthroization]([PropertyId], 0)
		ON [AppCatalog].[Property] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogPropertyAuthroization]([PropertyId], 0)
		ON [AppCatalog].[Property] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogPropertyAuthroization]([PropertyId], 0)
		ON [AppCatalog].[Property] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
