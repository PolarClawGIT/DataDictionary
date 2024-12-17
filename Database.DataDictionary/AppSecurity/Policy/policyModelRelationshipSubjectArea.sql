CREATE SECURITY POLICY [AppSecurity].[policyModelRelationshipSubjectArea]
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[RelationshipSubjectArea] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[RelationshipSubjectArea] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[RelationshipSubjectArea] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
