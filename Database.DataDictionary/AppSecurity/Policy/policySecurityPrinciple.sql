CREATE SECURITY POLICY [AppSecurity].[policyPrinciple]
    ADD BLOCK PREDICATE [AppSecurity].[funcPrincipleAuthorization]()
		ON [AppSecurity].[Principle],
    ADD BLOCK PREDICATE [AppSecurity].[funcPrincipleAuthorization]()
		ON [AppSecurity].[RoleMembership],
    ADD BLOCK PREDICATE [AppSecurity].[funcPrincipleAuthorization]()
		ON [AppSecurity].[Role]
WITH (STATE = ON, SCHEMABINDING = ON)