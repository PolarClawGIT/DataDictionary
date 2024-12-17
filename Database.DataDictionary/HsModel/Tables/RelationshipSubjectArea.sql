CREATE TABLE [HsModel].[RelationshipSubjectArea] (
    [ModelId]        UniqueIdentifier NOT NULL,
    [SubjectAreaId]  UniqueIdentifier NOT NULL,
    [RelationshipId] UniqueIdentifier NOT NULL,
    [NameSpaceId]    UniqueIdentifier NULL,
    [SysStart]       DateTime2 (7) NOT NULL,
    [SysEnd]         DateTime2 (7) NOT NULL,
);
GO
CREATE CLUSTERED INDEX [IX_RelationshipSubjectArea]
    ON [HsModel].[RelationshipSubjectArea]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_RelationshipSubjectArea]
    ON [HsModel].[RelationshipSubjectArea]([ModelId] ASC, [SubjectAreaId] ASC, [RelationshipId] ASC)
GO
