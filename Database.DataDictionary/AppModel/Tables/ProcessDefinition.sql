CREATE TABLE [AppModel].[ProcessDefinition]
(
	[ProcessId]         UniqueIdentifier NOT Null,
	[DefinitionId]      UniqueIdentifier NOT NULL,
	[DefinitionSummary] [AppGeneral].[typeDescription] Null, -- Plain Text summary, used where RTF cannot be used.
	[DefinitionText]    [AppModel].[typeRichText] Null, -- Contains Rich Text Definition. Rich Text must be handled differently.
    -- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_ProcessDefinition_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_ProcessDefinition_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ProcessDefinition] PRIMARY KEY CLUSTERED ([ProcessId] ASC, [DefinitionId] ASC),
	CONSTRAINT [FK_ProcessDefinitionProcess] FOREIGN KEY ([ProcessId]) REFERENCES [AppModel].[Process] ([ProcessId]),
	CONSTRAINT [FK_ProcessDefinitionDefinition] FOREIGN KEY ([DefinitionId]) REFERENCES [AppModel].[DefinitionEnumeration] ([DefinitionId]),
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[ProcessDefinition]))
GO
