CREATE SECURITY POLICY [AppSecurity].[policySecurable]
    ADD BLOCK PREDICATE [AppSecurity].[funcSecurableAuthorization]([SecurableId])
		ON [AppSecurity].[SecurableOwner],
    ADD BLOCK PREDICATE [AppSecurity].[funcSecurableAuthorization]([SecurableId])
		ON [AppSecurity].[SecurablePermission]
	WITH (STATE = ON, SCHEMABINDING = ON)
GO