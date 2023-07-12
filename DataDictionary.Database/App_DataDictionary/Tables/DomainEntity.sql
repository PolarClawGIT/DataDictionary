CREATE TABLE [App_DataDictionary].[DomainEntity]
(
	-- Entities are a catch-all item that are not a Column or Parameter. They are to be implemented later.
	[EntityId] UniqueIdentifier Not Null CONSTRAINT [DF_DomainEntity_EntityId] DEFAULT (NEWSEQUENTIALID()),
	[EntityParentId] UniqueIdentifier Null,
	[EntityTitle] NVarChar(100) Not Null,
	[EntityDescription] NVarChar(Max) Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainEntity_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainEntity_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainEntity_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainEntity] PRIMARY KEY CLUSTERED ([EntityId] ASC),
	CONSTRAINT [FK_DomainEntity_Parent] FOREIGN KEY ([EntityParentId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DomainEntity]
    ON [App_DataDictionary].[DomainEntity]([EntityTitle] ASC, [EntityParentId] ASC);
GO
