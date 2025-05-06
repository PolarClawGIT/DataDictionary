CREATE TABLE [HsModel].[RelationshipAlias]
(
	[RelationshipId]    UniqueIdentifier Not Null,
	[AliasId]           UniqueIdentifier Not Null,
	[AliasScope]        [AppModel].[typeScopeName] NOT NULL,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_RelationshipAlias]
    ON [HsModel].[RelationshipAlias]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_RelationshipAlias]
    ON [HsModel].[RelationshipAlias]([RelationshipId] ASC, [AliasId] ASC)
GO