CREATE SECURITY POLICY [AppSecurity].[policyModelRelationship]
	ADD BLOCK PREDICATE [AppSecurity].[funcModelRelationshipAuthorization] (Null, Null, Null) ON [AppModel].[Relationship],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelRelationshipAuthorization] (Null, Null, Null) ON [AppModel].[RelationshipAlias],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelRelationshipAuthorization] (Null, Null, Null) ON [AppModel].[RelationshipAttribute],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelRelationshipAuthorization] (Null, Null, Null) ON [AppModel].[RelationshipDefinition],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelRelationshipAuthorization] (Null, Null, Null) ON [AppModel].[RelationshipProperty],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelRelationshipAuthorization] (Null, Null, Null) ON [AppModel].[RelationshipSubjectArea]
	WITH (STATE = ON, SCHEMABINDING = ON)
GO