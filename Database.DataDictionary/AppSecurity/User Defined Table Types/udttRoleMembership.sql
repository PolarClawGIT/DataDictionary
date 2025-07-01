CREATE TYPE [AppSecurity].[udttRoleMembership] AS TABLE
(
	[RoleId]          UniqueIdentifier Null,
	[PrincipalId]     UniqueIdentifier Null
)
