CREATE SECURITY POLICY [AppSecurity].[policyPrincipal]
    ADD BLOCK PREDICATE [AppSecurity].[funcPrincipalAuthorization]()
		ON [AppSecurity].[Principal],
    ADD BLOCK PREDICATE [AppSecurity].[funcPrincipalAuthorization]()
		ON [AppSecurity].[RoleMembership],
    ADD BLOCK PREDICATE [AppSecurity].[funcPrincipalAuthorization]()
		ON [AppSecurity].[Role]
WITH (STATE = ON, SCHEMABINDING = ON)