CREATE SECURITY POLICY [AppSecurity].[policySecurityPrinciple]
    ADD BLOCK PREDICATE [AppSecurity].[funcSecurityPrinciple]()
		ON [AppSecurity].[SecurityPrinciple],
    ADD BLOCK PREDICATE [AppSecurity].[funcSecurityPrinciple]()
		ON [AppSecurity].[SecurityMembership],
    ADD BLOCK PREDICATE [AppSecurity].[funcSecurityPrinciple]()
		ON [AppSecurity].[SecurityRole],
    ADD BLOCK PREDICATE [AppSecurity].[funcSecurityPrinciple]()
		ON [AppSecurity].[SecurityPermission]
WITH (STATE = ON, SCHEMABINDING = ON)