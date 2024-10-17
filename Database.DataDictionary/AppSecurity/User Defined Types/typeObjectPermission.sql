CREATE TYPE [AppSecurity].[typeObjectPermission] AS TABLE
(
	[RoleId]        UniqueIdentifier Null,
	[ObjectId]      UniqueIdentifier Null,
	[ObjectTitle]   [App_DataDictionary].[typeTitle] Null,
	[IsGrant]       Bit Null,
	[IsDeny]        Bit Null,
	[AlterValue]	Bit Null,
	[AlterSecurity]	Bit Null
)
