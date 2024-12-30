CREATE SECURITY POLICY [AppSecurity].[policyCatalog]
	ADD BLOCK PREDICATE [AppSecurity].[funcCatalogAuthorization] ([CatalogId], 1) ON [AppCatalog].[Catalog],
	ADD BLOCK PREDICATE [AppSecurity].[funcCatalogAuthorization] (Null, Null) ON [AppCatalog].[Property],
	ADD BLOCK PREDICATE [AppSecurity].[funcCatalogAuthorization] (Null, Null) ON [AppCatalog].[Schema],
	ADD BLOCK PREDICATE [AppSecurity].[funcCatalogAuthorization] (Null, Null) ON [AppCatalog].[Domain],
	ADD BLOCK PREDICATE [AppSecurity].[funcCatalogAuthorization] (Null, Null) ON [AppCatalog].[Table],
	ADD BLOCK PREDICATE [AppSecurity].[funcCatalogAuthorization] (Null, Null) ON [AppCatalog].[TableColumn],
	ADD BLOCK PREDICATE [AppSecurity].[funcCatalogAuthorization] (Null, Null) ON [AppCatalog].[Constraint],
	ADD BLOCK PREDICATE [AppSecurity].[funcCatalogAuthorization] (Null, Null) ON [AppCatalog].[ConstraintColumn],
	ADD BLOCK PREDICATE [AppSecurity].[funcCatalogAuthorization] (Null, Null) ON [AppCatalog].[Routine],
	ADD BLOCK PREDICATE [AppSecurity].[funcCatalogAuthorization] (Null, Null) ON [AppCatalog].[RoutineParameter],
	ADD BLOCK PREDICATE [AppSecurity].[funcCatalogAuthorization] (Null, Null) ON [AppCatalog].[RoutineColumn],
	ADD BLOCK PREDICATE [AppSecurity].[funcCatalogAuthorization] (Null, Null) ON [AppCatalog].[Reference]
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
