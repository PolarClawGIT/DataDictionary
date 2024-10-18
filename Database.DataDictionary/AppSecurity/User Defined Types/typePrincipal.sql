CREATE TYPE [AppSecurity].[typePrincipal] AS TABLE
(
	[PrincipalId]         UniqueIdentifier Null,
	[PrincipalLogin]      SysName Not Null,
	[PrincipalName]       [App_DataDictionary].[typeTitle] Null,
	[PrincipalAnnotation] [App_DataDictionary].[typeDescription] Null,
	[AlterValue]	      Bit Null,
	[AlterSecurity]	      Bit Null
)
