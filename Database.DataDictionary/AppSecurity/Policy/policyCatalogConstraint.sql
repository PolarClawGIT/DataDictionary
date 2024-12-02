CREATE SECURITY POLICY [AppSecurity].[policyCatalogConstraint]
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogConstraintAuthorization]([ConstraintId], 0)
		ON [AppCatalog].[Constraint] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogConstraintAuthorization]([ConstraintId], 0)
		ON [AppCatalog].[Constraint] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogConstraintAuthorization]([ConstraintId], 0)
		ON [AppCatalog].[Constraint] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
