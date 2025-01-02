CREATE TABLE [HsModel].[AttributeProperty]
(
	[AttributeId]		UniqueIdentifier Not Null,
	[PropertyId]		UniqueIdentifier NOT Null,
	[PropertyValue]		[AppModel].[typePropertyValue] Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_AttributeProperty]
    ON [HsModel].[AttributeProperty]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_AttributeProperty]
    ON [HsModel].[Model]([ModelId] ASC)
GO