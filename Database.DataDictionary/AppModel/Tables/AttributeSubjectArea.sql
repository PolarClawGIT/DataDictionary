CREATE TABLE [AppModel].[AttributeSubjectArea] (
    [ModelId]       UniqueIdentifier NOT NULL,
    [SubjectAreaId] UniqueIdentifier NOT NULL,
    [AttributeId]   UniqueIdentifier NOT NULL,
    [NameSpaceId]   UniqueIdentifier NOT NULL,
    -- Temporal History Support
    [SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_AttributeSubjectArea_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_AttributeSubjectArea_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
    -- Keys
    CONSTRAINT [PK_AttributeSubjectArea] PRIMARY KEY CLUSTERED ([ModelId] ASC, [SubjectAreaId] ASC, [AttributeId] ASC),
    CONSTRAINT [FK_AttributeSubjectArea_Attribute] FOREIGN KEY ([ModelId], [AttributeId]) REFERENCES [App_DataDictionary].[ModelAttribute] ([ModelId], [AttributeId]),
    CONSTRAINT [FK_AttributeSubjectArea_Subject] FOREIGN KEY ([ModelId], [SubjectAreaId]) REFERENCES [AppModel].[SubjectArea] ([ModelId], [SubjectAreaId]),
    CONSTRAINT [FK_AttributeSubjectArea_NameSpace] FOREIGN KEY ([NameSpaceId]) REFERENCES [AppModel].[NameSpaceHierarchy] ([NameSpaceId]),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[AttributeSubjectArea]))

