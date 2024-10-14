CREATE TABLE [AppSecurity].[ObjectOwner]
(
	-- Resolves many to many between Security Principal and Security Object.
	[PrincipleId] UniqueIdentifier Not Null, -- The owner of the item
	[ObjectId]  UniqueIdentifier Not Null, -- Catalog, Library, Model, ...
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_ObjectOwner_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ObjectOwner_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ObjectOwner_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ObjectOwner] PRIMARY KEY CLUSTERED ([PrincipleId] ASC, [ObjectId] ASC),
	CONSTRAINT [FK_ObjectOwner_SecurityPrinciple] FOREIGN KEY ([PrincipleId]) REFERENCES [AppSecurity].[Principle] ([PrincipleId]),
)
