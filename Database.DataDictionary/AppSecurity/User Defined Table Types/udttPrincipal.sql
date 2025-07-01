CREATE TYPE [AppSecurity].[udttPrincipal] AS TABLE
(
	[PrincipalId]         UniqueIdentifier Null,
	[PrincipalLogin]      SysName Not Null,
	[PrincipalName]       [AppGeneral].[uddtTitle] Null,
	[PrincipalAnnotation] [AppGeneral].[uddtDescription] Null
)
