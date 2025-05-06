CREATE TABLE [HsModel].[ProcessSubjectArea]
(
    [ProcessId]     UniqueIdentifier NOT NULL,
    [SubjectAreaId] UniqueIdentifier NOT NULL,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_ProcessSubjectArea]
    ON [HsModel].[ProcessSubjectArea]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_ProcessSubjectArea]
    ON [HsModel].[ProcessSubjectArea]([ProcessId] ASC, [SubjectAreaId] ASC)
GO