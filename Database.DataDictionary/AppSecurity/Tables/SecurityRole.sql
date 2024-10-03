CREATE TABLE [AppSecurity].[SecurityRole]
(
	[RoleId] UniqueIdentifier NOT NULL CONSTRAINT [DF_SecurityRoleId] DEFAULT (newid()),
	[RoleName] SysName Not Null,
	-- Special Admin Security, Do I Need this?
	[IsSecurityAdmin] Bit Null,
	[IsHelpAdmin] Bit Null,
	[IsCatalogAdmin] Bit Null,
	[IsLibraryAdmin] Bit Null,
	[IsModelAdmin] Bit Null,
	[IsScriptAdmin] Bit Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_SecurityRole_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_SecurityRole_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_SecurityRole_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_SecurityRole] PRIMARY KEY CLUSTERED ([RoleId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_SecurityRole]
    ON [AppSecurity].[SecurityRole]([RoleName] ASC);
GO