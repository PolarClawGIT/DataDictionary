CREATE SECURITY POLICY [AppSecurity].[policyCatalogTableColumn]
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogTableAuthorization]([TableId], 0)
		ON [AppCatalog].[TableColumn] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogTableAuthorization]([TableId], 0)
		ON [AppCatalog].[TableColumn] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogTableAuthorization]([TableId], 0)
		ON [AppCatalog].[TableColumn] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
