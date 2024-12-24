CREATE TABLE [HsModel].[ModelEntity] (
    [ModelId]       UniqueIdentifier                                   NOT NULL,
    [EntityId]      UniqueIdentifier                                   NOT NULL,
    [SysStart]      DateTime2 (7)    NOT NULL,
    [SysEnd]        DateTime2 (7)    NOT NULL
)
GO
CREATE CLUSTERED INDEX [IX_ModelEntity]
    ON [HsModel].[ModelEntity]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_ModelEntity]
    ON [HsModel].[ModelEntity]([ModelId] ASC, [EntityId] ASC)
GO
