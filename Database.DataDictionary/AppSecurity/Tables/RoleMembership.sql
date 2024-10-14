CREATE TABLE [AppSecurity].[RoleMembership]
(
	-- Resolves many to many between Security Role and Security Principle.
	[RoleId] UniqueIdentifier Not Null,
	[PrincipleId] UniqueIdentifier Not Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_RoleMembership_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_RoleMembership_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_RoleMembership_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_SecurityMembership] PRIMARY KEY CLUSTERED ([RoleId] ASC, [PrincipleId] ASC),
	CONSTRAINT [FK_SecurityMembership_Role] FOREIGN KEY ([RoleId]) REFERENCES [AppSecurity].[Role] ([RoleId]),
	CONSTRAINT [FK_SecurityMembership_Principle] FOREIGN KEY ([PrincipleId]) REFERENCES [AppSecurity].[Principle] ([PrincipleId]),
)
