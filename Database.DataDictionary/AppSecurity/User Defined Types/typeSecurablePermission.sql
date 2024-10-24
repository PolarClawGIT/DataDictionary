CREATE TYPE [AppSecurity].[typeSecurablePermission] AS TABLE
(
	[RoleId]         UniqueIdentifier Null,
	[SecurableId]    UniqueIdentifier Null,
	[SecurableTitle] [App_DataDictionary].[typeTitle] Null,
	[IsGrant]        Bit Null,
	[IsDeny]         Bit Null,
	[AlterValue]	 Bit Null,
	[AlterSecurity]	 Bit Null
)
