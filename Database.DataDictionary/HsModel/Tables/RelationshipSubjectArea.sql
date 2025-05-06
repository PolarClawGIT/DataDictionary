CREATE TABLE [HsModel].[RelationshipSubjectArea]
(
    [RelationshipId] UniqueIdentifier NOT NULL,
    [SubjectAreaId]  UniqueIdentifier NOT NULL,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_RelationshipSubjectArea]
    ON [HsModel].[RelationshipSubjectArea]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_RelationshipSubjectArea]
    ON [HsModel].[RelationshipSubjectArea]([RelationshipId] ASC, [SubjectAreaId] ASC)
GO