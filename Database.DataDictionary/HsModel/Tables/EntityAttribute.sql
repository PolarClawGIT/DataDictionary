CREATE TABLE [HsModel].[EntityAttribute]
(
	[EntityAttributeId] UniqueIdentifier Not Null,
	[EntityId]          UniqueIdentifier Not Null,
	[AttributeAlias]    [App_DataDictionary].[typeTitle] Not Null,
	[AttributeName]     [AppModel].[typeQualifiedName] Null,
	[OrdinalPosition]   Int Not Null,
	[IsNullable]		Bit Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_EntityAttribute]
    ON [HsModel].[EntityAttribute]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_EntityAttribute]
    ON [HsModel].[EntityAttribute]([EntityAttributeId] ASC)
GO
