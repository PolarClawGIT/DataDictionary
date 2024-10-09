CREATE TYPE [AppSecurity].[typeSecurityPrinciple] AS TABLE
(
	[PrincipleId]         UniqueIdentifier Null,
	[PrincipleLogin]      SysName Not Null,
	[PrincipleName]       [App_DataDictionary].[typeTitle] Null,
	[PrincipleAnnotation] [App_DataDictionary].[typeDescription] Null,
	[PrincipalType]       [App_DataDictionary].[typeObjectType] Null,
	[IsDbWriter]          Bit Null,
	[IsApplicationUser]   Bit Null,
	[AlterValue]	      Bit Null,
	[AlterSecurity]	      Bit Null
)
