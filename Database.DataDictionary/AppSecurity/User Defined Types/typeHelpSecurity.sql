CREATE TYPE [AppSecurity].[typeHelpSecurity] AS TABLE
(
	[HelpId]        UniqueIdentifier Not Null,
	[PrincipleId]   UniqueIdentifier Null, -- Owner
	[RoleId]        UniqueIdentifier Null,
	[IsGrant]       Bit Null,
	[IsDeny]        Bit Null,
	[AlterValue]	Bit Null,
	[AlterSecurity]	Bit Null
)
