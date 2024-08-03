CREATE TABLE [App_DataDictionary].[ModelSubjectProcess] (
    [ModelId]       UNIQUEIDENTIFIER NOT NULL,
    [SubjectAreaId] UNIQUEIDENTIFIER NOT NULL,
    [ProcessId]     UNIQUEIDENTIFIER NOT NULL,
    [NameSpaceId]   UNIQUEIDENTIFIER NOT NULL,
    [ModfiedBy]     [sysname]                                          CONSTRAINT [DF_ModelSubjectProcess_ModfiedBy] DEFAULT (original_login()) NOT NULL,
    [SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_ModelSubjectProcess_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_ModelSubjectProcess_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    CONSTRAINT [PK_ModelSubjectProcess] PRIMARY KEY CLUSTERED ([ModelId] ASC, [SubjectAreaId] ASC, [ProcessId] ASC),
    CONSTRAINT [FK_ModelSubjectProcess_Process] FOREIGN KEY ([ModelId], [ProcessId]) REFERENCES [App_DataDictionary].[ModelProcess] ([ModelId], [ProcessId]),
    CONSTRAINT [FK_ModelSubjectProcess_Subject] FOREIGN KEY ([ModelId], [SubjectAreaId]) REFERENCES [App_DataDictionary].[ModelSubjectArea] ([ModelId], [SubjectAreaId]),
    CONSTRAINT [FK_ModelSubjectProcess_NameSpace] FOREIGN KEY ([NameSpaceId]) REFERENCES [App_DataDictionary].[ModelNameSpace] ([NameSpaceId]),
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd])
);

