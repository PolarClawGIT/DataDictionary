CREATE TABLE [AppModel].[EntitySubjectArea] (
    [SubjectAreaId] UniqueIdentifier NOT NULL,
    [EntityId]      UniqueIdentifier NOT NULL,
    [NameSpaceId]   UNIQUEIDENTIFIER NOT NULL,
    -- Temporal History Support
    [SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_EntitySubjectArea_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_EntitySubjectArea_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
    -- Keys
    CONSTRAINT [PK_EntitySubjectArea] PRIMARY KEY CLUSTERED ([EntityId] ASC, [SubjectAreaId]),
    CONSTRAINT [FK_EntitySubjectArea_Entity] FOREIGN KEY ([EntityId]) REFERENCES [AppModel].[Entity] ([EntityId]),
    CONSTRAINT [FK_EntitySubjectArea_Subject] FOREIGN KEY ([SubjectAreaId]) REFERENCES [AppModel].[SubjectArea] ([SubjectAreaId]),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[EntitySubjectArea]))

