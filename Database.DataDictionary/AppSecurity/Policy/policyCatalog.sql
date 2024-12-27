CREATE SECURITY POLICY [AppSecurity].[policyCatalog]
	ADD BLOCK PREDICATE [AppSecurity].[funcApplicationAuthorization] () ON [AppCatalog].[Property],
	ADD BLOCK PREDICATE [AppSecurity].[funcApplicationAuthorization] () ON [AppCatalog].[Catalog],
	ADD BLOCK PREDICATE [AppSecurity].[funcApplicationAuthorization] () ON [AppCatalog].[Schema],
	ADD BLOCK PREDICATE [AppSecurity].[funcApplicationAuthorization] () ON [AppCatalog].[Domain],
	ADD BLOCK PREDICATE [AppSecurity].[funcApplicationAuthorization] () ON [AppCatalog].[Table],
	ADD BLOCK PREDICATE [AppSecurity].[funcApplicationAuthorization] () ON [AppCatalog].[TableColumn],
	ADD BLOCK PREDICATE [AppSecurity].[funcApplicationAuthorization] () ON [AppCatalog].[Constraint],
	ADD BLOCK PREDICATE [AppSecurity].[funcApplicationAuthorization] () ON [AppCatalog].[ConstraintColumn],
	ADD BLOCK PREDICATE [AppSecurity].[funcApplicationAuthorization] () ON [AppCatalog].[Routine],
	ADD BLOCK PREDICATE [AppSecurity].[funcApplicationAuthorization] () ON [AppCatalog].[RoutineParameter],
	ADD BLOCK PREDICATE [AppSecurity].[funcApplicationAuthorization] () ON [AppCatalog].[RoutineColumn],
	ADD BLOCK PREDICATE [AppSecurity].[funcApplicationAuthorization] () ON [AppCatalog].[Reference]
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
