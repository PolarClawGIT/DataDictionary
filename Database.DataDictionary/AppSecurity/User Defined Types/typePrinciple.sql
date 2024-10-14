CREATE TYPE [AppSecurity].[typePrinciple] AS TABLE
(
	[PrincipleId]         UniqueIdentifier Null,
	[PrincipleLogin]      SysName Not Null,
	[PrincipleName]       [App_DataDictionary].[typeTitle] Null,
	[PrincipleAnnotation] [App_DataDictionary].[typeDescription] Null,
	[AlterValue]	      Bit Null,
	[AlterSecurity]	      Bit Null
)
