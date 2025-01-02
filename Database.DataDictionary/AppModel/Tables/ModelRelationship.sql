CREATE TABLE [AppModel].[ModelRelationship] (
    [ModelId]        UNIQUEIDENTIFIER                                   NOT NULL,
    [RelationshipId] UNIQUEIDENTIFIER                                   NOT NULL,
    -- TODO: Add System Version later once the schema is locked down
    [SysStart]       DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_ModelRelationship_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]         DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_ModelRelationship_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    CONSTRAINT [PK_ModelRelationship] PRIMARY KEY CLUSTERED ([ModelId] ASC, [RelationshipId] ASC),
    CONSTRAINT [FK_ModelRelationship_Model] FOREIGN KEY ([ModelId]) REFERENCES [AppModel].[Model] ([ModelId]),
    CONSTRAINT [FK_ModelRelationship_Relationship] FOREIGN KEY ([RelationshipId]) REFERENCES [AppModel].[Relationship] ([RelationshipId]),
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd])
);


GO
