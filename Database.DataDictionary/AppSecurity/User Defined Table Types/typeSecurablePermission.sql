CREATE TYPE [AppSecurity].[typeSecurablePermission] AS TABLE
(
	[RoleId]         UniqueIdentifier Null,
	[SecurableId]    UniqueIdentifier Null,
	[SecurableTitle] [AppGeneral].[typeTitle] Null,
	[IsGrant]        Bit Null,
	[IsDeny]         Bit Null
)
