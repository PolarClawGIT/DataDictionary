CREATE SECURITY POLICY [AppSecurity].[policyObject]
    ADD BLOCK PREDICATE [AppSecurity].[funcObjectAuthorization]([ObjectId])
		ON [AppSecurity].[ObjectOwner],
    ADD BLOCK PREDICATE [AppSecurity].[funcObjectAuthorization]([ObjectId])
		ON [AppSecurity].[ObjectPermission]
	WITH (STATE = ON, SCHEMABINDING = ON)
GO