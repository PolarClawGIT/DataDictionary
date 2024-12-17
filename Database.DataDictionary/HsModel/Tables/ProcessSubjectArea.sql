CREATE TABLE [HsModel].[ProcessSubjectArea] (
    [ModelId]       UniqueIdentifier NOT NULL,
    [SubjectAreaId] UniqueIdentifier NOT NULL,
    [ProcessId]     UniqueIdentifier NOT NULL,
    [NameSpaceId]   UniqueIdentifier NOT NULL,
    [SysStart]      DateTime2 NOT NULL,
    [SysEnd]        DateTime2 NOT NULL,
);
GO
CREATE CLUSTERED INDEX [IX_ProcessSubjectArea]
    ON [HsModel].[ProcessSubjectArea]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_ProcessSubjectArea]
    ON [HsModel].[ProcessSubjectArea]([ModelId] ASC, [SubjectAreaId] ASC, [ProcessId] ASC)
GO
