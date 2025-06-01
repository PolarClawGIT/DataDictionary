CREATE TABLE [HsModel].[ProcessAlias]
(
	[ProcessId]         UniqueIdentifier Not Null,
	[AliasId]           UniqueIdentifier Not Null,
	[AliasScope]        [AppModel].[typeScopeName] NOT NULL,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_ProcessAlias]
    ON [HsModel].[ProcessAlias]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_ProcessAlias]
    ON [HsModel].[ProcessAlias]([ProcessId] ASC, [AliasId] ASC)
GO