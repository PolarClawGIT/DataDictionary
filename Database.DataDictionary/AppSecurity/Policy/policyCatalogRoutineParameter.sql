CREATE SECURITY POLICY [AppSecurity].[policyCatalogRoutine]
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogRoutineAuthorization]([RoutineId], 0)
		ON [AppCatalog].[Routine] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogRoutineAuthorization]([RoutineId], 0)
		ON [AppCatalog].[Routine] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogRoutineAuthorization]([RoutineId], 0)
		ON [AppCatalog].[Routine] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
