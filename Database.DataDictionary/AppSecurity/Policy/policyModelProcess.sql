CREATE SECURITY POLICY [AppSecurity].[policyModelProcess]
	ADD BLOCK PREDICATE [AppSecurity].[funcModelProcessAuthorization] (Null, Null, Null) ON [AppModel].[Process],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelProcessAuthorization] (Null, Null, Null) ON [AppModel].[ProcessAlias],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelProcessAuthorization] (Null, Null, Null) ON [AppModel].[ProcessDataFlow],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelProcessAuthorization] (Null, Null, Null) ON [AppModel].[ProcessDefinition],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelProcessAuthorization] (Null, Null, Null) ON [AppModel].[ProcessProperty],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelProcessAuthorization] (Null, Null, Null) ON [AppModel].[ProcessSubjectArea]
	WITH (STATE = ON, SCHEMABINDING = ON)
GO