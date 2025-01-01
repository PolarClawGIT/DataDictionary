CREATE TABLE [HsModel].[EntitySubjectArea] (
    [EntityId]      UniqueIdentifier NOT NULL,
    [SubjectAreaId] UniqueIdentifier NOT NULL,
    [SysStart]      DateTime2 (7) NOT NULL,
    [SysEnd]        DateTime2 (7) NOT NULL,
);
GO
CREATE CLUSTERED INDEX [IX_EntitySubjectArea]
    ON [HsModel].[EntitySubjectArea]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_EntitySubjectArea]
    ON [HsModel].[EntitySubjectArea]([SubjectAreaId] ASC, [EntityId] ASC)
GO
