CREATE TABLE [App_DataDictionary].[DomainEntityDefinition]
(
	[EntityId]       UniqueIdentifier NOT NULL,
	[DefinitionId]   UniqueIdentifier NOT NULL,
	[DefinitionText] NVarChar(Max) NULL,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy]     SysName Not Null CONSTRAINT [DF_DomainEntityDefinition_ModfiedBy] DEFAULT (original_login()),
	[SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainEntityDefinition_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainEntityDefinition_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainEntityDefinition] PRIMARY KEY CLUSTERED ([EntityId] ASC, [DefinitionId] ASC),
	CONSTRAINT [FK_DomainEntityDefinitionDomainEntity] FOREIGN KEY ([EntityId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
	CONSTRAINT [FK_DomainEntityDefinitionApplicationDefinition] FOREIGN KEY ([DefinitionId]) REFERENCES [App_DataDictionary].[ApplicationDefinition] ([DefinitionId]),

)
