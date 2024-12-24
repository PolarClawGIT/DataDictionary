CREATE TABLE [AppModel].[EntityDefinition]
(
	[EntityId]			UniqueIdentifier NOT Null,
	[DefinitionId]      UniqueIdentifier NOT NULL,
	[DefinitionSummary] [App_DataDictionary].[typeDescription] Null, -- Plain Text summary, used where RTF cannot be used.
	[DefinitionText]    [App_DataDictionary].[typeRichText] Null, -- Contains Rich Text Definition. Rich Text must be handled differently.
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_EntityDefinitionModifiedBy] DEFAULT (ORIGINAL_LOGIN()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_EntityDefinition_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_EntityDefinition_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_EntityDefinition] PRIMARY KEY CLUSTERED ([EntityId] ASC, [DefinitionId] ASC),
	CONSTRAINT [FK_EntityDefinitionEntity] FOREIGN KEY ([EntityId]) REFERENCES [AppModel].[Entity] ([EntityId]),
	CONSTRAINT [FK_EntityDefinitionDefinition] FOREIGN KEY ([DefinitionId]) REFERENCES [AppModel].[DefinitionEnumeration] ([DefinitionId]),

)
