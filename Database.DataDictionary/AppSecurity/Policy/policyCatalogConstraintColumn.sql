CREATE SECURITY POLICY [AppSecurity].[policyCatalogConstraintColumn]
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogConstraintAuthorization]([ConstraintId], 0)
		ON [AppCatalog].[ConstraintColumn] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogConstraintAuthorization]([ConstraintId], 0)
		ON [AppCatalog].[ConstraintColumn] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogConstraintAuthorization]([ConstraintId], 0)
		ON [AppCatalog].[ConstraintColumn] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
