CREATE SECURITY POLICY [AppSecurity].[policyModelEntity]
	ADD BLOCK PREDICATE [AppSecurity].[funcModelEntityAuthorization] (Null, Null, Null) ON [AppModel].[Entity],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelEntityAuthorization] (Null, Null, Null) ON [AppModel].[EntityAlias],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelEntityAuthorization] (Null, Null, Null) ON [AppModel].[EntityDefinition],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelEntityAuthorization] (Null, Null, Null) ON [AppModel].[EntityProperty],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelEntityAuthorization] (Null, Null, Null) ON [AppModel].[EntitySubjectArea],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelEntityAuthorization] (Null, Null, Null) ON [AppModel].[EntityAttribute]
	WITH (STATE = ON, SCHEMABINDING = ON)
GO