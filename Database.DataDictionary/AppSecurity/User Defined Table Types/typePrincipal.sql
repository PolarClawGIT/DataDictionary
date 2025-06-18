CREATE TYPE [AppSecurity].[typePrincipal] AS TABLE
(
	[PrincipalId]         UniqueIdentifier Null,
	[PrincipalLogin]      SysName Not Null,
	[PrincipalName]       [AppGeneral].[typeTitle] Null,
	[PrincipalAnnotation] [AppGeneral].[typeDescription] Null
)
