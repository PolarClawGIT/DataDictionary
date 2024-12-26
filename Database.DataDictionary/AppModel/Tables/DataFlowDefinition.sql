CREATE TABLE [AppModel].[DataFlowDefinition]
(
	[DataFlowId]       UniqueIdentifier NOT Null,
	[DefinitionId]      UniqueIdentifier NOT NULL,
	[DefinitionSummary] [App_DataDictionary].[typeDescription] Null, -- Plain Text summary, used where RTF cannot be used.
	[DefinitionText]    [App_DataDictionary].[typeRichText] Null, -- Contains Rich Text Definition. Rich Text must be handled differently.
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_DataFlowDefinition_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_DataFlowDefinition_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DataFlowDefinition] PRIMARY KEY CLUSTERED ([DataFlowId] ASC, [DefinitionId] ASC),
	CONSTRAINT [FK_DataFlowDefinitionDataFlow] FOREIGN KEY ([DataFlowId]) REFERENCES [AppModel].[DataFlow] ([DataFlowId]),
	CONSTRAINT [FK_DataFlowDefinitionDefinition] FOREIGN KEY ([DefinitionId]) REFERENCES [AppModel].[DefinitionEnumeration] ([DefinitionId]),
)
