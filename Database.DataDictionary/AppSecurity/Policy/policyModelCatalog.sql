CREATE SECURITY POLICY [AppSecurity].[policyModelCatalog]
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 1)
		ON [AppModel].[ModelCatalog] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[ModelCatalog] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 1)
		ON [AppModel].[ModelCatalog] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
