CREATE TABLE [HsModel].[RelationshipAttribute]
(	[RelationshipId]    UniqueIdentifier Not Null,
	[AttributeAliasId]  UniqueIdentifier Not Null,
	[AttributeKnownAs]	[App_DataDictionary].[typeTitle] Not Null,
	[OrdinalPosition]   Int Not Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_RelationshipAttribute]
    ON [HsModel].[RelationshipAttribute]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_RelationshipAttribute]
    ON [HsModel].[RelationshipAttribute]([RelationshipId] ASC, [AttributeAliasId] ASC)
GO