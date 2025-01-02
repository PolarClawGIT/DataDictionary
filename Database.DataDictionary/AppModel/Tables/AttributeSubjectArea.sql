CREATE TABLE [AppModel].[AttributeSubjectArea] (
    [AttributeId]   UniqueIdentifier NOT NULL,
    [SubjectAreaId] UniqueIdentifier NOT NULL,
    -- Temporal History Support
    [SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_AttributeSubjectArea_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_AttributeSubjectArea_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
    -- Keys
    CONSTRAINT [PK_AttributeSubjectArea] PRIMARY KEY CLUSTERED ([AttributeId] ASC, [SubjectAreaId]),
    CONSTRAINT [FK_AttributeSubjectArea_Attribute] FOREIGN KEY ([AttributeId]) REFERENCES [AppModel].[Attribute] ([AttributeId]),
    CONSTRAINT [FK_AttributeSubjectArea_Subject] FOREIGN KEY ([SubjectAreaId]) REFERENCES [AppModel].[SubjectArea] ([SubjectAreaId]),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[AttributeSubjectArea]))

