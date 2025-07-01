CREATE SECURITY POLICY [AppSecurity].[policyScripting]
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] ([TemplateId], 1) ON [AppScript].[ScriptingTemplate],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[ScriptingPath],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[ScriptingNode],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[ScriptingNameSpace],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[ScriptingModel],
	ADD BLOCK PREDICATE [AppSecurity].[funcScriptingAuthorization] (Null, Null) ON [AppScript].[ScriptingAttribute]
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
