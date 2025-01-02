CREATE TABLE [HsModel].[ModelProperty]
(
	[ModelId]    UniqueIdentifier NOT NULL,
	[PropertyId] UniqueIdentifier NOT NULL,
	[SysStart]   DateTime2 (7) NOT NULL,
	[SysEnd]     DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_ModelProperty]
    ON [HsModel].[ModelProperty]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_ModelProperty]
    ON [HsModel].[ModelProperty]([ModelId] ASC, [PropertyId] ASC)
GO