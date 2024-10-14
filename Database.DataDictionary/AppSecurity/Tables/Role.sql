CREATE TABLE [AppSecurity].[Role]
(
	[RoleId] UniqueIdentifier NOT NULL CONSTRAINT [DF_RoleId] DEFAULT (newid()),
	[RoleName] [App_DataDictionary].[typeTitle] Not Null,
	[RoleDescription] [App_DataDictionary].[typeDescription] Null,
	-- Permission List
	-- Admin: 0 = not an administrator (could be an administrator in a different role)
	--        1 = administrator for type of objects
	-- Owner: 0 = Contributor who can work with child objects they have permission too
	--        1 = Can create/delete objects they own (create/delete Security Object records)
	-- Row-Level security can be applied using [SecurityPermission].
	--
	-- Consideration: Adding/Removing a Permission requires structure change.
	--    This is an intentional de-normalization.
	--    Code must be changed in order to implement a new permission or change an existing one.
	--    The alternative is to use "magic strings" that may be missed or not configured.
	[IsSecurityAdmin] Bit Not Null,
	[IsHelpAdmin] Bit Not Null,
	[IsHelpOwner] Bit Not Null,
	[IsCatalogAdmin] Bit Not Null,
	[IsCatalogOwner] Bit Not Null,
	[IsLibraryAdmin] Bit Not Null,
	[IsLibraryOwner] Bit Not Null,
	[IsModelAdmin] Bit Not Null,
	[IsModelOwner] Bit Not Null,
	[IsScriptAdmin] Bit Not Null,
	[IsScriptOwner] Bit Not Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_Role_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Role_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Role_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([RoleId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Role]
    ON [AppSecurity].[Role]([RoleName] ASC);
GO