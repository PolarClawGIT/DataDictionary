CREATE TABLE [AppSecurity].[ObjectPermission]
(
	-- Resolves many to many between Security Role and Security Object
	[RoleId] UniqueIdentifier Not Null, -- Role Granted Permission to the object
	[ObjectId]  UniqueIdentifier Not Null, -- Catalog, Library, Model, Attribute, Entity, Routine, Script
	--[IsContributor] Bit Not Null, -- Can Insert/Update/delete child items, but not this item.
	[IsGrant] Bit Not Null, -- Grants access to object 
	[IsDeny] Bit Not Null, -- Denies role access to object, regardless of any grants.
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_ObjectPermission_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ObjectPermission_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ObjectPermission_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ObjectPermission] PRIMARY KEY CLUSTERED ([RoleId] ASC, [ObjectId] ASC),
	CONSTRAINT [FK_ObjectPermission_SecurityRole] FOREIGN KEY ([RoleId]) REFERENCES [AppSecurity].[Role] ([RoleId]),
)
