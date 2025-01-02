CREATE TABLE [HsModel].[EntityProperty]
(
	[EntityId]			UniqueIdentifier Not Null,
	[PropertyId]		UniqueIdentifier NOT Null,
	[PropertyValue]		[AppModel].[typePropertyValue] Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_EntityProperty]
    ON [HsModel].[EntityProperty]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_EntityProperty]
    ON [HsModel].[EntityProperty]([EntityId] ASC, [PropertyId] ASC)
GO