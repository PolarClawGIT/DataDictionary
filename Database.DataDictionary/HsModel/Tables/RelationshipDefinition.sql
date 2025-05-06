CREATE TABLE [HsModel].[RelationshipDefinition]
(
	[RelationshipId]    UniqueIdentifier NOT Null,
	[DefinitionId]      UniqueIdentifier NOT NULL,
	[DefinitionSummary] [App_DataDictionary].[typeDescription] Null,
	[DefinitionText]    [AppModel].[typeRichText] Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_RelationshipDefinition]
    ON [HsModel].[RelationshipDefinition]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_RelationshipDefinition]
    ON [HsModel].[RelationshipDefinition]([RelationshipId] ASC, [DefinitionId] ASC)
GO