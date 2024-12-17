CREATE SECURITY POLICY [AppSecurity].[policyModelProcessSubjectArea]
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[ProcessSubjectArea] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[ProcessSubjectArea] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[ProcessSubjectArea] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
