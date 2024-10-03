CREATE TABLE [AppSecurity].[SecurityMembership]
(
	[RoleId] UniqueIdentifier Not Null,
	[PrincipleId] UniqueIdentifier Not Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_SecurityMembership_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_SecurityMembership_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_SecurityMembership_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_SecurityMembership] PRIMARY KEY CLUSTERED ([RoleId] ASC, [PrincipleId] ASC),
	CONSTRAINT [FK_SecurityMembership_SecurityRole] FOREIGN KEY ([RoleId]) REFERENCES [AppSecurity].[SecurityRole] ([RoleId]),
	CONSTRAINT [FK_SecurityMembership_SecurityPrinciple] FOREIGN KEY ([PrincipleId]) REFERENCES [AppSecurity].[SecurityPrinciple] ([PrincipleId]),
)
