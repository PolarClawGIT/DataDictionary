CREATE TABLE [HsModel].[ProcessProperty]
(
	[ProcessId]			UniqueIdentifier Not Null,
	[PropertyId]		UniqueIdentifier NOT Null,
	[PropertyValue]		[AppModel].[uddtPropertyValue] Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_ProcessProperty]
    ON [HsModel].[ProcessProperty]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_ProcessProperty]
    ON [HsModel].[ProcessProperty]([ProcessId] ASC, [PropertyId] ASC)
GO