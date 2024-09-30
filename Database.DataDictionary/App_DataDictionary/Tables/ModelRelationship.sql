CREATE TABLE [App_DataDictionary].[ModelRelationship] (
    [ModelId]        UNIQUEIDENTIFIER                                   NOT NULL,
    [RelationshipId] UNIQUEIDENTIFIER                                   NOT NULL,
    [SubjectAreaId]  UNIQUEIDENTIFIER                                   NULL,
    [MemberName]     [App_DataDictionary].[typeNameSpaceMember]         NOT NULL,
    [ModifiedBy]     SysName                                            CONSTRAINT [DF_ModelRelationship_ModifiedBy] DEFAULT (original_login()) NOT NULL,
    [SysStart]       DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_ModelRelationship_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]         DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_ModelRelationship_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    CONSTRAINT [PK_ModelRelationship] PRIMARY KEY CLUSTERED ([ModelId] ASC, [RelationshipId] ASC),
    CONSTRAINT [FK_ModelRelationship_Model] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
    CONSTRAINT [FK_ModelRelationship_Relationship] FOREIGN KEY ([RelationshipId]) REFERENCES [App_DataDictionary].[DomainRelationship] ([RelationshipId]),
    CONSTRAINT [AK_ModelRelationship] UNIQUE NONCLUSTERED ([ModelId] ASC, [MemberName] ASC),
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd])
);


GO
