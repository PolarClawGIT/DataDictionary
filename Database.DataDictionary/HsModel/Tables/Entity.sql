CREATE TABLE [HsModel].[Entity]
(
	[EntityId]          UniqueIdentifier Not Null,
	[EntityTitle]       [App_DataDictionary].[typeTitle] Not Null,
	[EntityDescription] [App_DataDictionary].[typeDescription] Null,
	[EntityName]        [AppModel].[typeQualifiedName] Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_Entity]
    ON [HsModel].[Entity]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Entity]
    ON [HsModel].[Entity]([EntityId] ASC)
GO

