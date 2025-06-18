CREATE TYPE [AppSecurity].[typeRole] AS TABLE (
	[RoleId]          UniqueIdentifier Null,
	[RoleName]        [AppGeneral].[typeTitle] Not Null,
	[RoleDescription] [AppGeneral].[typeDescription] Null,
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
