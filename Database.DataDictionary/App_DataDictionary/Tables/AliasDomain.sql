CREATE TABLE [App_DataDictionary].[AliasDomain]
(
	[AliasId]           UniqueIdentifier Not Null,
	[DomainItemId]      As (Coalesce([AttributeId], [EntityId], [ProcessId])) PERSISTED,
	[ScopeId]           Int NOT Null,
	-- Sub-Type to a Domain Entity, Attribute or Process. This is a One to Zero or One relationship.
	[AttributeId]       UniqueIdentifier Null, 
	[EntityId]          UniqueIdentifier Null,
	[ProcessId]         UniqueIdentifier Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_AliasDomain_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_AliasDomain_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_AliasDomain_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	--CONSTRAINT [PK_AliasDomain] PRIMARY KEY CLUSTERED ([AliasId] ASC, [DomainItemId] Asc), -- DomainItemId can evaluate Null so primary Key constraint cannot be used.
	CONSTRAINT [FK_AliasDomainItem] FOREIGN KEY ([AliasId]) REFERENCES [App_DataDictionary].[AliasItem] ([AliasId]),
	CONSTRAINT [FK_AliasDomainAttribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
	CONSTRAINT [FK_AliasDomainEntity] FOREIGN KEY ([EntityId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
	CONSTRAINT [FK_AliasDomainProcess] FOREIGN KEY ([ProcessId]) REFERENCES [App_DataDictionary].[DomainProcess] ([ProcessId]),
	CONSTRAINT [FK_AliasDomainScope] FOREIGN KEY ([ScopeId]) REFERENCES [App_DataDictionary].[AliasScope] ([ScopeId]),
	CONSTRAINT [CK_AliasDomainId] CHECK (
		([AttributeId] is Not Null And [EntityId] is Null And [ProcessId] is Null) Or
		([AttributeId] is Null And [EntityId] is Not Null And [ProcessId] is Null) Or
		([AttributeId] is Null And [EntityId] is Null And [ProcessId] is Not Null))
)
GO
CREATE UNIQUE CLUSTERED INDEX [PK_AliasDomain]
    ON [App_DataDictionary].[AliasDomain]([AliasId] ASC, [DomainItemId] Asc);
GO