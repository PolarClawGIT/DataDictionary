CREATE TABLE [HsModel].[RelationshipProperty]
(
	[RelationshipId]	UniqueIdentifier Not Null,
	[PropertyId]		UniqueIdentifier NOT Null,
	[PropertyValue]		NVarChar(4000) Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_RelationshipProperty]
    ON [HsModel].[RelationshipProperty]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_RelationshipProperty]
    ON [HsModel].[RelationshipProperty]([RelationshipId] ASC, [PropertyId] ASC)
GO