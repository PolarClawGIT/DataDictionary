CREATE SECURITY POLICY [AppSecurity].[policyCatalogTable]
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogTableAuthorization]([TableId], 0)
		ON [AppCatalog].[Table] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogTableAuthorization]([TableId], 0)
		ON [AppCatalog].[Table] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogTableAuthorization]([TableId], 0)
		ON [AppCatalog].[Table] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
