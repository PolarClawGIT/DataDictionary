CREATE TABLE [HsModel].[AttributeAlias]
(
	[AttributeId]      UniqueIdentifier NOT Null,
	[AliasId]          UniqueIdentifier Not Null,
    [AliasScope]       [AppModel].[typeScopeName] NOT NULL,
    [SysStart]         DateTime2 (7) NOT NULL,
    [SysEnd]           DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_AttributeAlias]
    ON [HsModel].[AttributeAlias]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_AttributeAlias]
    ON [HsModel].[AttributeAlias]([AttributeId] ASC, [AliasId] ASC)
GO