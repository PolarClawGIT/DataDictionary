CREATE TABLE [AppModel].[ProcessSubjectArea] (
    [ModelId]       UniqueIdentifier NOT NULL,
    [SubjectAreaId] UniqueIdentifier NOT NULL,
    [ProcessId]     UniqueIdentifier NOT NULL,
    [NameSpaceId]   UniqueIdentifier NOT NULL,
    -- Temporal History Support
    [SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_ProcessSubjectArea_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_ProcessSubjectArea_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
    -- Keys
    CONSTRAINT [PK_ProcessSubjectArea] PRIMARY KEY CLUSTERED ([ModelId] ASC, [SubjectAreaId] ASC, [ProcessId] ASC),
    CONSTRAINT [FK_ProcessSubjectArea_Process] FOREIGN KEY ([ModelId], [ProcessId]) REFERENCES [App_DataDictionary].[ModelProcess] ([ModelId], [ProcessId]),
    CONSTRAINT [FK_ProcessSubjectArea_Subject] FOREIGN KEY ([ModelId], [SubjectAreaId]) REFERENCES [AppModel].[SubjectArea] ([ModelId], [SubjectAreaId]),
    CONSTRAINT [FK_ProcessSubjectArea_NameSpace] FOREIGN KEY ([NameSpaceId]) REFERENCES [AppModel].[NameSpaceHierarchy] ([NameSpaceId]),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[ProcessSubjectArea]))

