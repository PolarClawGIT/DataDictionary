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
) --WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[ModelRelationship]))
GO
/*
CREATE TABLE [HsModel].[ModelRelationship] (
    [ModelId]       UniqueIdentifier NOT NULL,
    [RelationshipId] UniqueIdentifier NOT NULL,
    [SysStart]      DateTime2 (7)    NOT NULL,
    [SysEnd]        DateTime2 (7)    NOT NULL
)
GO
CREATE CLUSTERED INDEX [IX_ModelRelationship]
    ON [HsModel].[ModelRelationship]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_ModelRelationship]
    ON [HsModel].[ModelRelationship]([ModelId] ASC, [RelationshipId] ASC)
GO
*/