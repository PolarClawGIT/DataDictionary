CREATE TABLE [HsModel].[EntityAlias]
(
	[AliasId]           UniqueIdentifier Not Null,
	[EntityId]          UniqueIdentifier NOT Null,
	[AliasScope]        [App_DataDictionary].[typeScopeName] NOT NULL,
	[AliasNameSpace]    [App_DataDictionary].[typeNameSpacePath] Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_EntityAlias]
    ON [HsModel].[EntityAlias]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_EntityAlias]
    ON [HsModel].[EntityAlias]([AliasId] ASC)
GO