CREATE TABLE [HsModel].[Process]
(
	[ProcessId]          UniqueIdentifier Not Null,
	[ProcessTitle]       [App_DataDictionary].[typeTitle] Not Null,
	[ProcessDescription] [App_DataDictionary].[typeDescription] Null,
	[ProcessName]        [AppModel].[typeQualifiedName] Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_Process]
    ON [HsModel].[Process]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Process]
    ON [HsModel].[Process]([ProcessId] ASC)
GO