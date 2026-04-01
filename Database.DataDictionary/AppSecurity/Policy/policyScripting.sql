CREATE SECURITY POLICY [AppSecurity].[policyScripting]
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] ([TemplateId], 1) ON [AppScript].[Template],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[TemplateModel],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[Transform],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[SchemaDefinition],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[SchemaNode],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[SchemaNodeOwner],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[Document],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[TemplateObject]
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
