CREATE TABLE [HsModel].[AttributeDefinition]
(
	[AttributeId]       UniqueIdentifier NOT Null,
	[DefinitionId]      UniqueIdentifier NOT NULL,
	[DefinitionSummary] [AppGeneral].[dtDescription] Null,
	[DefinitionText]    [AppModel].[typeRichText] Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_AttributeDefinition]
    ON [HsModel].[AttributeDefinition]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_AttributeDefinition]
    ON [HsModel].[AttributeDefinition]([AttributeId] ASC, [DefinitionId] ASC)
GO