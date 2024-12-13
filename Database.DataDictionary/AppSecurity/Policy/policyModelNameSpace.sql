CREATE SECURITY POLICY [AppSecurity].[policyModelNameSpace]
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 1)
		ON [AppModel].[NameSpaceHierarchy] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 0)
		ON [AppModel].[NameSpaceHierarchy] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization]([ModelId], 1)
		ON [AppModel].[NameSpaceHierarchy] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
