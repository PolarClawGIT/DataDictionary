CREATE TYPE [AppSecurity].[typeSecurableOwner] AS TABLE
(
	[PrincipalId]    UniqueIdentifier Null,
	[SecurableId]    UniqueIdentifier Null,
	[SecurableTitle] [App_DataDictionary].[typeTitle] Null,
	[AlterValue]	 Bit Null,
	[AlterSecurity]	 Bit Null
)
