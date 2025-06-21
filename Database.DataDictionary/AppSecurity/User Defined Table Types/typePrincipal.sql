CREATE TYPE [AppSecurity].[typePrincipal] AS TABLE
(
	[PrincipalId]         UniqueIdentifier Null,
	[PrincipalLogin]      SysName Not Null,
	[PrincipalName]       [AppGeneral].[dtTitle] Null,
	[PrincipalAnnotation] [AppGeneral].[dtDescription] Null
)
