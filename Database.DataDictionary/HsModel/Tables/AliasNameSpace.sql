CREATE TABLE [HsModel].[AliasNameSpace]
(
	[AliasId]           UniqueIdentifier Not Null,
	[AliasMember]       [AppGeneral].[uddtMember] Not Null,
	[ParentAliasId]     UniqueIdentifier NULL,
    [SysStart]          DateTime2 (7) NOT NULL,
    [SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_AliasNameSpace]
    ON [HsModel].[AliasNameSpace]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_AliasNameSpace]
    ON [HsModel].[AliasNameSpace]([AliasId] ASC)
GO