CREATE TABLE [AppModel].[RelationshipSubjectArea] (
    [ModelId]        UniqueIdentifier NOT NULL,
    [SubjectAreaId]  UniqueIdentifier NOT NULL,
    [RelationshipId] UniqueIdentifier NOT NULL,
    [NameSpaceId]    UniqueIdentifier NULL,
    -- Temporal History Support
    [SysStart]       DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_RelationshipSubjectArea_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]         DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_RelationshipSubjectArea_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
    -- Keys
    CONSTRAINT [PK_RelationshipSubjectArea] PRIMARY KEY CLUSTERED ([ModelId] ASC, [SubjectAreaId] ASC, [RelationshipId] ASC),
    CONSTRAINT [FK_RelationshipSubjectArea_Relationship] FOREIGN KEY ([ModelId], [RelationshipId]) REFERENCES [App_DataDictionary].[ModelRelationship] ([ModelId], [RelationshipId]),
    CONSTRAINT [FK_RelationshipSubjectArea_Subject] FOREIGN KEY ([ModelId], [SubjectAreaId]) REFERENCES [AppModel].[SubjectArea] ([ModelId], [SubjectAreaId]),
    CONSTRAINT [FK_RelationshipSubjectArea_NameSpace] FOREIGN KEY ([NameSpaceId]) REFERENCES [AppModel].[NameSpaceHierarchy] ([NameSpaceId]),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[RelationshipSubjectArea]))

