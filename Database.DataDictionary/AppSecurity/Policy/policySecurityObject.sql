CREATE SECURITY POLICY [AppSecurity].[policySecurityObject]
    ADD BLOCK PREDICATE [AppSecurity].[funcSecurityObject]([ObjectId], 1)
		ON [AppSecurity].[SecurityOwner],
    ADD BLOCK PREDICATE [AppSecurity].[funcSecurityObject]([ObjectId], 1)
		ON [AppSecurity].[SecurityPermission]
	WITH (STATE = ON, SCHEMABINDING = ON)
GO