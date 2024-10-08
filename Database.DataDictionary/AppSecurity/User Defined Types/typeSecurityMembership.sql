CREATE TYPE [AppSecurity].[typeSecurityMembership] AS TABLE
(
	[RoleId]          UniqueIdentifier Null,
	[PrincipleId]     UniqueIdentifier Null
)
