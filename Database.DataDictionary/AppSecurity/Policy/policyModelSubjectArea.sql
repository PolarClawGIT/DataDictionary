CREATE SECURITY POLICY [AppSecurity].[policyModelSubjectArea]
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 1)
		ON [AppModel].[SubjectArea] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[SubjectArea] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 1)
		ON [AppModel].[SubjectArea] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
