CREATE SECURITY POLICY [AppSecurity].[policyScripting]
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] ([TemplateId], 1) ON [AppScript].[Template],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[TemplateNode],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[TemplateNodeOwner],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[DataSource],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[DataObject],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[DataObjectName],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[Document],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[DocumentFile],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[ScriptingModel]
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
