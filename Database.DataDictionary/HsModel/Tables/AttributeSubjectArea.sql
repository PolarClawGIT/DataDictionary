CREATE TABLE [HsModel].[AttributeSubjectArea] (
    [AttributeId]   UniqueIdentifier NOT NULL,
    [SubjectAreaId] UniqueIdentifier NOT NULL,
    [SysStart]      DateTime2  NOT NULL,
    [SysEnd]        DateTime2 (7)  NOT NULL,
);
GO
CREATE CLUSTERED INDEX [IX_AttributeSubjectArea]
    ON [HsModel].[AttributeSubjectArea]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_AttributeSubjectArea]
    ON [HsModel].[AttributeSubjectArea]([SubjectAreaId] ASC, [AttributeId] ASC)
GO
