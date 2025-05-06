CREATE TABLE [HsModel].[Relationship]
(
	[RelationshipId]          UniqueIdentifier Not Null,
	[RelationshipTitle]       [App_DataDictionary].[typeTitle] Not Null,
	[RelationshipDescription] [App_DataDictionary].[typeDescription] Null,
	[RelationshipName]        [AppModel].[typeQualifiedName] Null,
	[RelationshipType]        NVarChar(20) Not Null,
	[OwnerAliasId]            UniqueIdentifier Not Null,
	[RefrenceAliasId]         UniqueIdentifier Null,
	[SysStart]                DateTime2 (7) NOT NULL,
	[SysEnd]                  DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_Relationship]
    ON [HsModel].[Relationship]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Relationship]
    ON [HsModel].[Relationship]([RelationshipId] ASC)
GO