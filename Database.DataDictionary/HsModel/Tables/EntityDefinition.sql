CREATE TABLE [HsModel].[EntityDefinition]
(
	[EntityId]			UniqueIdentifier NOT Null,
	[DefinitionId]      UniqueIdentifier NOT NULL,
	[DefinitionSummary] [App_DataDictionary].[typeDescription] Null,
	[DefinitionText]    [AppModel].[typeRichText] Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_EntityDefinition]
    ON [HsModel].[EntityDefinition]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_EntityDefinition]
    ON [HsModel].[EntityDefinition]([EntityId] ASC, [DefinitionId] ASC)
GO