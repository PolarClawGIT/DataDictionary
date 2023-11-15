CREATE TABLE [App_DataDictionary].[DomainAliasItem]
(
	[AliasId]           UniqueIdentifier Not Null,
	[DomainItemId]      As (Coalesce([AttributeId], [EntityId], [ProcessId])) PERSISTED,
	[ScopeId]           Int NOT Null,
	-- Sub-Type to a Domain Entity, Attribute or Process. This is a One to Zero or One relationship.
	[AttributeId]       UniqueIdentifier Null, 
	[EntityId]          UniqueIdentifier Null,
	[ProcessId]         UniqueIdentifier Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainAliasItem_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainAliasItem_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainAliasItem_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	--CONSTRAINT [PK_DomainAliasItem] PRIMARY KEY CLUSTERED ([AliasId] ASC, [DomainItemId] Asc), -- DomainItemId can evaluate Null so primary Key constraint cannot be used.
	CONSTRAINT [FK_DomainAliasItemItem] FOREIGN KEY ([AliasId]) REFERENCES [App_DataDictionary].[DomainAlias] ([AliasId]),
	CONSTRAINT [FK_DomainAliasItemAttribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
	CONSTRAINT [FK_DomainAliasItemEntity] FOREIGN KEY ([EntityId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
	CONSTRAINT [FK_DomainAliasItemProcess] FOREIGN KEY ([ProcessId]) REFERENCES [App_DataDictionary].[DomainProcess] ([ProcessId]),
	CONSTRAINT [FK_DomainAliasItemScope] FOREIGN KEY ([ScopeId]) REFERENCES [App_DataDictionary].[ApplicationScope] ([ScopeId]),
	CONSTRAINT [CK_DomainAliasItemId] CHECK (
		([AttributeId] is Not Null And [EntityId] is Null And [ProcessId] is Null) Or
		([AttributeId] is Null And [EntityId] is Not Null And [ProcessId] is Null) Or
		([AttributeId] is Null And [EntityId] is Null And [ProcessId] is Not Null))
)
GO
CREATE UNIQUE CLUSTERED INDEX [PK_DomainAliasItem]
    ON [App_DataDictionary].[DomainAliasItem]([AliasId] ASC, [DomainItemId] Asc);
GO