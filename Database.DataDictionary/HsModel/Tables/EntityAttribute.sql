CREATE TABLE [HsModel].[EntityAttribute]
(
	[EntityId]          UniqueIdentifier Not Null,
	[AttributeAliasId]  UniqueIdentifier Not Null,
	[AttributeTitle]    [App_DataDictionary].[typeTitle] Not Null,
	[OrdinalPosition]   Int Not Null,
	[IsNullable]		Bit Not Null,
	[IsPrimaryKey]		Bit Not Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_EntityAttribute]
    ON [HsModel].[EntityAttribute]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_EntityAttribute]
    ON [HsModel].[EntityAttribute]([EntityId] ASC, [AttributeAliasId] ASC)
GO
