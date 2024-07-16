CREATE TABLE [App_DataDictionary].[ModelSubjectRelationship] (
    [ModelId]        UNIQUEIDENTIFIER NOT NULL,
    [SubjectAreaId]  UNIQUEIDENTIFIER NOT NULL,
    [RelationshipId] UNIQUEIDENTIFIER NOT NULL,
    [NameSpaceId]    UNIQUEIDENTIFIER NULL,
    [ModfiedBy]      [sysname]                                          CONSTRAINT [DF_ModelSubjectRelationship_ModfiedBy] DEFAULT (original_login()) NOT NULL,
    [SysStart]       DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_ModelSubjectRelationship_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]         DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_ModelSubjectRelationship_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    CONSTRAINT [PK_ModelSubjectRelationship] PRIMARY KEY CLUSTERED ([ModelId] ASC, [SubjectAreaId] ASC, [RelationshipId] ASC),
    CONSTRAINT [FK_ModelSubjectRelationship_Relationship] FOREIGN KEY ([ModelId], [RelationshipId]) REFERENCES [App_DataDictionary].[ModelRelationship] ([ModelId], [RelationshipId]),
    CONSTRAINT [FK_ModelSubjectRelationship_Subject] FOREIGN KEY ([ModelId], [SubjectAreaId]) REFERENCES [App_DataDictionary].[ModelSubjectArea] ([ModelId], [SubjectAreaId]),
    CONSTRAINT [FK_ModelSubjectRelationship_NameSpace] FOREIGN KEY ([NameSpaceId]) REFERENCES [App_DataDictionary].[ModelNameSpace] ([NameSpaceId]),
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd])
);

