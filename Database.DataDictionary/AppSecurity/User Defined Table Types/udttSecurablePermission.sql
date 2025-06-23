CREATE TYPE [AppSecurity].[udttSecurablePermission] AS TABLE
(
	[RoleId]         UniqueIdentifier Null,
	[SecurableId]    UniqueIdentifier Null,
	[SecurableTitle] [AppGeneral].[uddtTitle] Null,
	[IsGrant]        Bit Null,
	[IsDeny]         Bit Null
)
