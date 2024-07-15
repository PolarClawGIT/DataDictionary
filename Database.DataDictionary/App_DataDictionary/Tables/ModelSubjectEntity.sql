CREATE TABLE [App_DataDictionary].[ModelSubjectEntity] (
    [ModelId]       UNIQUEIDENTIFIER                                   NOT NULL,
    [SubjectAreaId] UNIQUEIDENTIFIER                                   NOT NULL,
    [EntityId]      UNIQUEIDENTIFIER                                   NOT NULL,
    [ModfiedBy]     [sysname]                                          CONSTRAINT [DF_ModelSubjectEntity_ModfiedBy] DEFAULT (original_login()) NOT NULL,
    [SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_ModelSubjectEntity_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_ModelSubjectEntity_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    CONSTRAINT [PK_ModelSubjectEntity] PRIMARY KEY CLUSTERED ([ModelId] ASC, [SubjectAreaId] ASC, [EntityId] ASC),
    CONSTRAINT [FK_ModelSubjectEntity_Entity] FOREIGN KEY ([ModelId], [EntityId]) REFERENCES [App_DataDictionary].[ModelEntity] ([ModelId], [EntityId]),
    CONSTRAINT [FK_ModelSubjectEntity_Subject] FOREIGN KEY ([ModelId], [SubjectAreaId]) REFERENCES [App_DataDictionary].[ModelSubjectArea] ([ModelId], [SubjectAreaId]),
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd])
);

