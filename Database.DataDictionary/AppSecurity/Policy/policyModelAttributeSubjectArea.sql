CREATE SECURITY POLICY [AppSecurity].[policyModelAttributeSubjectArea]
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[AttributeSubjectArea] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[AttributeSubjectArea] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[AttributeSubjectArea] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
