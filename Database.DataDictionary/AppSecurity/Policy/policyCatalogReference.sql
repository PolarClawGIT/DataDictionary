CREATE SECURITY POLICY [AppSecurity].[policyCatalogReference]
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogReferenceAuthorization]([ReferenceId], 0)
		ON [AppCatalog].[Reference] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogReferenceAuthorization]([ReferenceId], 0)
		ON [AppCatalog].[Reference] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogReferenceAuthorization]([ReferenceId], 0)
		ON [AppCatalog].[Reference] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
