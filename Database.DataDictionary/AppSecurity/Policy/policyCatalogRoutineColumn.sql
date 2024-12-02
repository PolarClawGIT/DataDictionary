CREATE SECURITY POLICY [AppSecurity].[policyCatalogRoutineColumn]
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogRoutineAuthorization]([RoutineId], 0)
		ON [AppCatalog].[RoutineColumn] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogRoutineAuthorization]([RoutineId], 0)
		ON [AppCatalog].[RoutineColumn] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogRoutineAuthorization]([RoutineId], 0)
		ON [AppCatalog].[RoutineColumn] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
