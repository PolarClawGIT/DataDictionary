CREATE TYPE [AppSecurity].[typeRoleMembership] AS TABLE
(
	[RoleId]          UniqueIdentifier Null,
	[PrincipalId]     UniqueIdentifier Null
)
