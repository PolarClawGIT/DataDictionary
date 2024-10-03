CREATE TABLE [AppSecurity].[SecurityPermission]
(
	[RoleId] UniqueIdentifier Not Null,
	[SystemId]  UniqueIdentifier Not Null, -- Catalog, Library, Model, Attribute, Entity, Routine, Script
	[IsWrite] Bit Not Null, -- 1 = Insert/Update/Delete is allowed, 0 = Deny all
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_SecurityPermission_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_SecurityPermission_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_SecurityPermission_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_SecurityPermission] PRIMARY KEY CLUSTERED ([RoleId] ASC, [SystemId] ASC),
	CONSTRAINT [FK_SecurityPermission_SecurityRole] FOREIGN KEY ([RoleId]) REFERENCES [AppSecurity].[SecurityRole] ([RoleId]),

)
