CREATE TYPE [AppSecurity].[typeRoleMembership] AS TABLE
(
	[RoleId]          UniqueIdentifier Null,
	[PrincipleId]     UniqueIdentifier Null,
	[AlterValue]	  Bit Null,
	[AlterSecurity]	  Bit Null
)
