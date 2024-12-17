CREATE TABLE [HsModel].[AttributeSubjectArea] (
    [ModelId]       UniqueIdentifier NOT NULL,
    [SubjectAreaId] UniqueIdentifier NOT NULL,
    [AttributeId]   UniqueIdentifier NOT NULL,
    [NameSpaceId]   UniqueIdentifier NOT NULL,
    [SysStart]      DateTime2  NOT NULL,
    [SysEnd]        DateTime2 (7)  NOT NULL,
);
GO
CREATE CLUSTERED INDEX [IX_AttributeSubjectArea]
    ON [HsModel].[AttributeSubjectArea]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_AttributeSubjectArea]
    ON [HsModel].[AttributeSubjectArea]([ModelId] ASC, [SubjectAreaId] ASC, [AttributeId] ASC)
GO
