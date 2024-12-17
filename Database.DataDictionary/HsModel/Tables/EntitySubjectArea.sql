CREATE TABLE [HsModel].[EntitySubjectArea] (
    [ModelId]       UniqueIdentifier NOT NULL,
    [SubjectAreaId] UniqueIdentifier NOT NULL,
    [EntityId]      UniqueIdentifier NOT NULL,
    [NameSpaceId]   UniqueIdentifier NOT NULL,
    [SysStart]      DateTime2 (7) NOT NULL,
    [SysEnd]        DateTime2 (7) NOT NULL,
);
GO
CREATE CLUSTERED INDEX [IX_EntitySubjectArea]
    ON [HsModel].[EntitySubjectArea]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_EntitySubjectArea]
    ON [HsModel].[EntitySubjectArea]([ModelId] ASC, [SubjectAreaId] ASC, [EntityId] ASC)
GO
