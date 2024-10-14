CREATE TYPE [AppSecurity].[typeRole] AS TABLE (
	[RoleId]          UniqueIdentifier Null,
	[RoleName]        [App_DataDictionary].[typeTitle] Not Null,
	[RoleDescription] [App_DataDictionary].[typeDescription] Null,
	[IsSecurityAdmin] Bit Null,
	[IsHelpAdmin]     Bit Null,
	[IsHelpOwner]     Bit Null,
	[IsCatalogAdmin]  Bit Null,
	[IsCatalogOwner]  Bit Null,
	[IsLibraryAdmin]  Bit Null,
	[IsLibraryOwner]  Bit Null,
	[IsModelAdmin]    Bit Null,
	[IsModelOwner]    Bit Null,
	[IsScriptAdmin]   Bit Null,
	[IsScriptOwner]   Bit Null,
	[AlterValue]	  Bit Null,
	[AlterSecurity]	  Bit Null
)
