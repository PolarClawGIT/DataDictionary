CREATE TYPE [AppSecurity].[udttSecurableOwner] AS TABLE
(
	[PrincipalId]    UniqueIdentifier Null,
	[SecurableId]    UniqueIdentifier Null,
	[SecurableTitle] [AppGeneral].[uddtTitle] Null
)
