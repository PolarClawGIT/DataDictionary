CREATE TABLE [AppModel].[SubjectArea] (
    [SubjectAreaId]          UniqueIdentifier                       CONSTRAINT [DF_SubjectAreaId] DEFAULT (newsequentialid()) NOT NULL,
    [SubjectAreaTitle]       [App_DataDictionary].[typeTitle]       NULL,
    [SubjectAreaDescription] [App_DataDictionary].[typeDescription] NULL,
    [SubjectName]            [AppModel].[typeQualifiedName]         Null,
    [ModelId]                UniqueIdentifier                       NOT NULL,
   	-- Temporal History Support
    [SysStart]               DateTime2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_SubjectArea_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]                 DateTime2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_SubjectArea_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
    -- Keys
    CONSTRAINT [PK_SubjectArea] PRIMARY KEY CLUSTERED ([SubjectAreaId] ASC),
    CONSTRAINT [FK_SubjectAreaModel] FOREIGN KEY ([ModelId]) REFERENCES [AppModel].[Model] ([ModelId]),
    CONSTRAINT [UK_SubjectArea] UNIQUE NONCLUSTERED ([ModelId] ASC, [SubjectAreaId] ASC),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[SubjectArea]))
GO
CREATE UNIQUE INDEX [AK_SubjectArea]
    ON [AppModel].[SubjectArea]([ModelId] ASC, [SubjectAreaTitle] ASC)
GO