CREATE TABLE [HsModel].[ModelProcess] (
    [ModelId]       UniqueIdentifier NOT NULL,
    [ProcessId]     UniqueIdentifier NOT NULL,
    [SysStart]      DateTime2 (7)    NOT NULL,
    [SysEnd]        DateTime2 (7)    NOT NULL
)
GO
CREATE CLUSTERED INDEX [IX_ModelProcess]
    ON [HsModel].[ModelProcess]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_ModelProcess]
    ON [HsModel].[ModelProcess]([ModelId] ASC, [ProcessId] ASC)
GO
