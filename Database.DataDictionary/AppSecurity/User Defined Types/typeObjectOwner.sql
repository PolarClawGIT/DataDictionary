CREATE TYPE [AppSecurity].[typeObjectOwner] AS TABLE
(
	[PrincipleId]   UniqueIdentifier Null,
	[ObjectId]      UniqueIdentifier Null,
	[ObjectTitle]   [App_DataDictionary].[typeTitle] Null,
	[AlterValue]	Bit Null,
	[AlterSecurity]	Bit Null
)
