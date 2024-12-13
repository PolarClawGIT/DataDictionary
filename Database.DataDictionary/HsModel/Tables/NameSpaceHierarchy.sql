CREATE TABLE [HsModel].[NameSpaceHierarchy]
(
	[NameSpaceId]           UniqueIdentifier NOT NULL,
	[ModelId]               UniqueIdentifier NOT NULL,
	[ParentNameSpaceId]     UniqueIdentifier NULL,
	[MemberName]            [App_DataDictionary].[typeNameSpaceMember] NOT NULL,
	[SysStart]              DateTime2 (7) NOT NULL,
	[SysEnd]                DateTime2 (7) NOT NULL ,
)
GO
CREATE CLUSTERED INDEX [IX_NameSpaceHierarchy]
    ON [HsModel].[NameSpaceHierarchy]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_NameSpaceHierarchy]
    ON [HsModel].[NameSpaceHierarchy]([NameSpaceId] ASC)
GO