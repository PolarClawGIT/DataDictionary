CREATE TABLE [HsModel].[AttributeAlias]
(
	[AliasId]          UniqueIdentifier Not Null,
	[AttributeId]      UniqueIdentifier NOT Null,
	[AliasScope]       [AppModel].[typeScopeName] NOT NULL,
	[AliasNameSpace]   [App_DataDictionary].[typeNameSpacePath] Null,
    [SysStart]         DateTime2 (7) NOT NULL,
    [SysEnd]           DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_AttributeAlias]
    ON [HsModel].[AttributeAlias]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_AttributeAlias]
    ON [HsModel].[AttributeAlias]([AliasId] ASC)
GO