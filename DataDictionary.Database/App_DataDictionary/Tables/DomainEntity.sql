CREATE TABLE [App_DataDictionary].[DomainEntity]
(
	[DomainId] Int NOT NULL,
	[EntityId] Int Not Null,
	[ParentEntityId] Int Null,
	[EntityTitle] NVarChar(250) Not Null,
	[ObjectName] SysName Null,
	[ObjectTypeId] Int Null,
	-- TODO: Add System Versioning later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainEntity_ModfiedBy] DEFAULT (ORIGINAL_LOGIN()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_DomainEntity_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_DomainEntity_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),

	CONSTRAINT [PK_DomainEntity] PRIMARY KEY CLUSTERED ([DomainId] ASC, [EntityId] ASC),
	CONSTRAINT [FK_DomainEntity_Parent] FOREIGN KEY ([DomainId], [ParentEntityId]) REFERENCES [App_DataDictionary].[DomainEntity] ([DomainId], [EntityId]),
	CONSTRAINT [FK_DomainEntity_ObjectType] FOREIGN KEY ([ObjectTypeId]) REFERENCES [App_DataDictionary].[ExtendedPropertyType] ([ObjectTypeId])
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DomainEntity]
    ON [App_DataDictionary].[DomainEntity]([DomainId] ASC, [ParentEntityId], [EntityTitle] Asc);
GO