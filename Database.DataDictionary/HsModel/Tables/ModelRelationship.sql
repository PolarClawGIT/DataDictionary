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
