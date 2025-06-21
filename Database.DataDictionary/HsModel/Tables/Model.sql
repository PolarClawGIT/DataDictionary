CREATE TABLE [HsModel].[Model]
(
	[ModelId]          UniqueIdentifier NOT NULL,
	[ModelTitle]       [AppGeneral].[dtTitle] Not Null,
	[ModelDescription] [AppGeneral].[dtDescription] Null,
	[SysStart]         DateTime2 (7) NOT NULL,
	[SysEnd]           DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_Model]
    ON [HsModel].[Model]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Model]
    ON [HsModel].[Model]([ModelId] ASC)
GO