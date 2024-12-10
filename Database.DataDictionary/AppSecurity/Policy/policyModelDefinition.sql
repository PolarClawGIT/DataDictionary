CREATE SECURITY POLICY [AppSecurity].[policyModelDefinition]
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[ModelDefinition] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[ModelDefinition] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[ModelDefinition] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
