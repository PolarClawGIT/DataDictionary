CREATE TABLE [HsModel].[Process]
(
	[ProcessId]          UniqueIdentifier Not Null,
	[ProcessTitle]       [AppGeneral].[uddtTitle] Not Null,
	[ProcessDescription] [AppGeneral].[uddtDescription] Null,
	[ProcessName]        [AppGeneral].[uddtQualifiedName] Null,
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