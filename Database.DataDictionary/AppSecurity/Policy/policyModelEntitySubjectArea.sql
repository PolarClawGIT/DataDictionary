CREATE SECURITY POLICY [AppSecurity].[policyModelEntitySubjectArea]
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[EntitySubjectArea] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[EntitySubjectArea] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[EntitySubjectArea] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
