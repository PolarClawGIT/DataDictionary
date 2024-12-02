CREATE SECURITY POLICY [AppSecurity].[policyCatalogSchema]
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogSchemaAuthorization]([SchemaId], 0)
		ON [AppCatalog].[Schema] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogSchemaAuthorization]([SchemaId], 0)
		ON [AppCatalog].[Schema] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogSchemaAuthorization]([SchemaId], 0)
		ON [AppCatalog].[Schema] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
