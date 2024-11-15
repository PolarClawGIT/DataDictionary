CREATE SECURITY POLICY [AppSecurity].[policyCatalogRoutineParameter]
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogRoutineAuthorization]([RoutineId], 0)
		ON [AppCatalog].[RoutineParameter] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogRoutineAuthorization]([RoutineId], 0)
		ON [AppCatalog].[RoutineParameter] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogRoutineAuthorization]([RoutineId], 0)
		ON [AppCatalog].[RoutineParameter] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
