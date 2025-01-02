CREATE TABLE [HsModel].[SubjectArea] (
    [SubjectAreaId]          UniqueIdentifier NOT NULL,
    [SubjectAreaTitle]       [App_DataDictionary].[typeTitle]       NULL,
    [SubjectAreaDescription] [App_DataDictionary].[typeDescription] NULL,
    [SubjectName]            [AppModel].[typeQualifiedName]         Null,
    [ModelId]                UniqueIdentifier                       NOT NULL,
    -- Temporal History Support
    [SysStart]               DateTime2 (7) NOT NULL,
    [SysEnd]                 DateTime2 (7) NOT NULL,
);
GO
CREATE CLUSTERED INDEX [IX_SubjectArea]
    ON [HsModel].[SubjectArea]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_SubjectArea]
    ON [HsModel].[SubjectArea]([SubjectAreaId] ASC)
GO
