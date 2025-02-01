CREATE TABLE [HsModel].[AliasHierarchy]
(
	[AliasId]           UniqueIdentifier Not Null,
	[AliasMember]       NVarChar(800) Not Null,
--	[AliasScope]        [AppModel].[typeScopeName] NULL,
	[ParentAliasId]     UniqueIdentifier NULL,
    [SysStart]          DateTime2 (7) NOT NULL,
    [SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_AliasHierarchy]
    ON [HsModel].[AliasHierarchy]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_AliasHierarchy]
    ON [HsModel].[AliasHierarchy]([AliasId] ASC)
GO