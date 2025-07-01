CREATE TABLE [HsCatalog].[Constraint]
(
	[ConstraintId]        UniqueIdentifier Not Null,
	[SchemaId]            UniqueIdentifier Not Null,
	[ConstraintName]      SysName Not Null,
	[TableId]             UniqueIdentifier Not Null,
	[ConstraintType]      [AppGeneral].[uddtObjectType] Null, -- Known types: FOREIGN KEY, UNIQUE, PRIMARY KEY
	[SysStart]            DateTime2 (7) Not Null,
	[SysEnd]              DateTime2 (7)  Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_Constraint]
    ON [HsCatalog].[Constraint]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Constraint]
    ON [HsCatalog].[Constraint]([ConstraintId] ASC)
GO
