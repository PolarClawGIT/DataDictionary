CREATE SECURITY POLICY [AppSecurity].[policyModelAdminstrator]
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAdministrator]()
		ON [AppModel].[DefinitionEnumeration] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAdministrator]()
		ON [AppModel].[PropertyEnumeration] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAdministrator]()
		ON [AppModel].[DefinitionEnumeration] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAdministrator]()
		ON [AppModel].[PropertyEnumeration] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcModelAdministrator]()
		ON [AppModel].[DefinitionEnumeration] BEFORE DELETE,
	ADD BLOCK PREDICATE [AppSecurity].[funcModelAdministrator]()
		ON [AppModel].[PropertyEnumeration] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO