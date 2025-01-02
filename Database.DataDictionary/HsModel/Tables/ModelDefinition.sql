CREATE TABLE [HsModel].[ModelDefinition]
(
	[ModelId]      UniqueIdentifier NOT NULL,
	[DefinitionId] UniqueIdentifier NOT NULL,
	[SysStart]     DateTime2 (7) NOT NULL,
	[SysEnd]       DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_ModelDefinition]
    ON [HsModel].[ModelDefinition]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_ModelDefinition]
    ON [HsModel].[ModelDefinition]([ModelId] ASC, [DefinitionId] ASC)
GO