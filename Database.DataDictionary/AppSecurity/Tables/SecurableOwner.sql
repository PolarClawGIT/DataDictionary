CREATE TABLE [AppSecurity].[SecurableOwner]
(
	-- Resolves many to many between Security Principal and Security Securable.
	[PrincipalId] UniqueIdentifier Not Null, -- The owner of the item
	[SecurableId]  UniqueIdentifier Not Null, -- Catalog, Library, Model, ...
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_SecurableOwner_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_SecurableOwner_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_SecurableOwner_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_SecurableOwner] PRIMARY KEY CLUSTERED ([PrincipalId] ASC, [SecurableId] ASC),
	CONSTRAINT [FK_SecurableOwner_SecurityPrincipal] FOREIGN KEY ([PrincipalId]) REFERENCES [AppSecurity].[Principal] ([PrincipalId]),
)
