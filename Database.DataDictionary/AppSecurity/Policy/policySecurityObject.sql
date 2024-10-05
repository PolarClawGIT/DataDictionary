CREATE SECURITY POLICY [AppSecurity].[policySecurityObject]
    ADD BLOCK PREDICATE [AppSecurity].[funcSecurityObject]([ObjectId])
		ON [AppSecurity].[SecurityOwner],
    ADD BLOCK PREDICATE [AppSecurity].[funcSecurityObject]([ObjectId])
		ON [AppSecurity].[SecurityPermission]
	WITH (STATE = ON, SCHEMABINDING = ON)
GO