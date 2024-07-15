CREATE TABLE [App_DataDictionary].[ModelProcess] (
    [ModelId]       UNIQUEIDENTIFIER                                   NOT NULL,
    [ProcessId]     UNIQUEIDENTIFIER                                   NOT NULL,
    [SubjectAreaId] UNIQUEIDENTIFIER                                   NULL,
    [MemberName]    [App_DataDictionary].[typeNameSpaceMember]         NOT NULL,
    [ModfiedBy]     [sysname]                                          CONSTRAINT [DF_ModelProcess_ModfiedBy] DEFAULT (original_login()) NOT NULL,
    [SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_ModelProcess_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_ModelProcess_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    CONSTRAINT [PK_ModelProcess] PRIMARY KEY CLUSTERED ([ModelId] ASC, [ProcessId] ASC),
    CONSTRAINT [FK_ModelProcess_Model] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
    CONSTRAINT [FK_ModelProcess_Process] FOREIGN KEY ([ProcessId]) REFERENCES [App_DataDictionary].[DomainProcess] ([ProcessId]),
    CONSTRAINT [AK_ModelProcess] UNIQUE NONCLUSTERED ([ModelId] ASC, [MemberName] ASC),
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd])
);


GO
