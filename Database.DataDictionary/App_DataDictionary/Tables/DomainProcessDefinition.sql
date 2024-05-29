CREATE TABLE [App_DataDictionary].[DomainProcessDefinition]
(
	[ProcessId]         UniqueIdentifier NOT Null,
	[DefinitionId]      UniqueIdentifier NOT NULL,
	[DefinitionSummary] [App_DataDictionary].[typeDescription] Null, -- Plain Text summary, used where RTF cannot be used.
	[DefinitionText]    NVarChar(Max) Null, -- Contains Rich Text Definition. Rich Text must be handled differently.
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainProcessDefinitionModfiedBy] DEFAULT (ORIGINAL_LOGIN()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_DomainProcessDefinition_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_DomainProcessDefinition_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainProcessDefinition] PRIMARY KEY CLUSTERED ([ProcessId] ASC, [DefinitionId] ASC),
	CONSTRAINT [FK_DomainProcessDefinitionProcess] FOREIGN KEY ([ProcessId]) REFERENCES [App_DataDictionary].[DomainProcess] ([ProcessId]),
	CONSTRAINT [FK_DomainProcessDefinitionDefinition] FOREIGN KEY ([DefinitionId]) REFERENCES [App_DataDictionary].[DomainDefinition] ([DefinitionId]),
)
