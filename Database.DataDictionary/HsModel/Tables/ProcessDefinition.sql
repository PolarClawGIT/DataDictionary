CREATE TABLE [HsModel].[ProcessDefinition]
(
	[ProcessId]         UniqueIdentifier NOT Null,
	[DefinitionId]      UniqueIdentifier NOT NULL,
	[DefinitionSummary] [AppGeneral].[uddtDescription] Null,
	[DefinitionText]    [AppModel].[uddtRichText] Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_ProcessDefinition]
    ON [HsModel].[ProcessDefinition]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_ProcessDefinition]
    ON [HsModel].[ProcessDefinition]([ProcessId] ASC, [DefinitionId] ASC)
GO