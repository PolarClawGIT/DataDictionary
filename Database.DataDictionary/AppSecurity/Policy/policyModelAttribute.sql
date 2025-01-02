CREATE SECURITY POLICY [AppSecurity].[policyModelAttribute]
	ADD BLOCK PREDICATE [AppSecurity].[funcModelAttributeAuthorization] (Null, Null, Null) ON [AppModel].[Attribute],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelAttributeAuthorization] (Null, Null, Null) ON [AppModel].[AttributeAlias],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelAttributeAuthorization] (Null, Null, Null) ON [AppModel].[AttributeDefinition],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelAttributeAuthorization] (Null, Null, Null) ON [AppModel].[AttributeProperty],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelAttributeAuthorization] (Null, Null, Null) ON [AppModel].[AttributeSubjectArea]
	WITH (STATE = ON, SCHEMABINDING = ON)
GO