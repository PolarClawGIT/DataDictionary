CREATE TYPE [AppSecurity].[typeSecurityMembership] AS TABLE
(
	[RoleId]          UniqueIdentifier Null,
	[PrincipleId]     UniqueIdentifier Null,
	[AlterValue]	  Bit Null,
	[AlterSecurity]	  Bit Null
)
