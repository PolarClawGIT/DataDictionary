CREATE TYPE [AppSecurity].[udttRole] AS TABLE (
	[RoleId]          UniqueIdentifier Null,
	[RoleName]        [AppGeneral].[uddtTitle] Not Null,
	[RoleDescription] [AppGeneral].[uddtDescription] Null,
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
	[IsScriptOwner]   Bit Null
)
