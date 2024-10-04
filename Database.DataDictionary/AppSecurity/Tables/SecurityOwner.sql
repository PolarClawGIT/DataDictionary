CREATE TABLE [AppSecurity].[SecurityOwner]
(
	-- Resolves many to many between Security Principal and Security Object.
	[PrincipleId] UniqueIdentifier Not Null, -- The owner of the item
	[ObjectId]  UniqueIdentifier Not Null, -- Catalog, Library, Model, ...
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_SecurityOwner_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_SecurityOwner_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_SecurityOwner_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_SecurityOwner] PRIMARY KEY CLUSTERED ([PrincipleId] ASC, [ObjectId] ASC),
	CONSTRAINT [FK_SecurityOwner_SecurityPrinciple] FOREIGN KEY ([PrincipleId]) REFERENCES [AppSecurity].[SecurityPrinciple] ([PrincipleId]),
)
