CREATE SECURITY POLICY [AppSecurity].[policyModelProperty]
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[ModelProperty] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[ModelProperty] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[ModelProperty] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
