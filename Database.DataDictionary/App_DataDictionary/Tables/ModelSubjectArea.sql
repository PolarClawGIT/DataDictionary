CREATE TABLE [App_DataDictionary].[ModelSubjectArea] (
    [SubjectAreaId]          UNIQUEIDENTIFIER                       CONSTRAINT [DF_ModelSubjectAreaId] DEFAULT (newsequentialid()) NOT NULL,
    [SubjectAreaTitle]       [App_DataDictionary].[typeTitle]       NULL,
    [SubjectAreaDescription] [App_DataDictionary].[typeDescription] NULL,
    [ModelId]                UNIQUEIDENTIFIER                       NOT NULL,
    [NameSpaceId]            UNIQUEIDENTIFIER                       NOT NULL,
    [ModifiedBy]              SysName                                           CONSTRAINT [DF_ModelSubjectArea_ModifiedBy] DEFAULT (original_login()) NOT NULL,
    [SysStart]               DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_ModelSubjectArea_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]                 DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_ModelSubjectArea_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    CONSTRAINT [PK_ModelSubjectArea] PRIMARY KEY CLUSTERED ([SubjectAreaId] ASC),
    CONSTRAINT [FK_ModelSubjectModel] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
    CONSTRAINT [UK_ModelSubjectArea] UNIQUE NONCLUSTERED ([ModelId] ASC, [SubjectAreaId] ASC),
    CONSTRAINT [FK_ModelSubjectAreaNameSpace] FOREIGN KEY ([NameSpaceId]) REFERENCES [App_DataDictionary].[ModelNameSpace] ([NameSpaceId]),
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd])
);


GO
CREATE UNIQUE INDEX [AK_ModelSubjectArea]
    ON [App_DataDictionary].[ModelSubjectArea]([ModelId] ASC, [SubjectAreaTitle] ASC)
GO

GO
