CREATE TYPE [AppSecurity].[typeSecurableOwner] AS TABLE
(
	[PrincipalId]    UniqueIdentifier Null,
	[SecurableId]    UniqueIdentifier Null,
	[SecurableTitle] [AppGeneral].[dtTitle] Null
)
