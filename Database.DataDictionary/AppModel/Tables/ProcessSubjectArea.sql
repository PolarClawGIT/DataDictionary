CREATE TABLE [AppModel].[ProcessSubjectArea]
(
    [ProcessId]     UniqueIdentifier NOT NULL,
    [SubjectAreaId] UniqueIdentifier NOT NULL,
    -- Temporal History Support
    [SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_ProcessSubjectArea_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_ProcessSubjectArea_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
    -- Keys
    CONSTRAINT [PK_ProcessSubjectArea] PRIMARY KEY CLUSTERED ([SubjectAreaId] ASC, [ProcessId] ASC),
    CONSTRAINT [FK_ProcessSubjectArea_Process] FOREIGN KEY ([ProcessId]) REFERENCES [AppModel].[Process] ([ProcessId]),
    CONSTRAINT [FK_ProcessSubjectArea_Subject] FOREIGN KEY ([SubjectAreaId]) REFERENCES [AppModel].[SubjectArea] ([SubjectAreaId]),
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[ProcessSubjectArea]))
GO

