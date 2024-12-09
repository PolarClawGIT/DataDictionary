CREATE SECURITY POLICY [AppSecurity].[policyModel]
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 1)
		ON [AppModel].[Model] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[Model] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 1)
		ON [AppModel].[Model] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
