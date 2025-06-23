CREATE TABLE [HsModel].[EntityAlias]
(
	[EntityId]          UniqueIdentifier NOT Null,
	[AliasId]           UniqueIdentifier Not Null,
	[AliasScope]        [AppGeneral].[uddtScopeName] NOT NULL,
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