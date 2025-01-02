CREATE TABLE [HsModel].[ModelAttribute] (
    [ModelId]       UniqueIdentifier NOT NULL,
    [AttributeId]   UniqueIdentifier NOT NULL,
    [SysStart]      DateTime2 (7)    NOT NULL,
    [SysEnd]        DateTime2 (7)    NOT NULL,
);
GO
CREATE CLUSTERED INDEX [IX_ModelAttribute]
    ON [HsModel].[ModelAttribute]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_ModelAttribute]
    ON [HsModel].[ModelAttribute]([ModelId] ASC, [AttributeId] ASC)
GO
