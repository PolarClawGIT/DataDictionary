CREATE TABLE [HsCatalog].[ConstraintColumn]
(
	[ConstraintColumnId]   UniqueIdentifier Not Null,
	[ConstraintId]         UniqueIdentifier Not Null,
	[TableColumnId]        UniqueIdentifier Not Null,
	[OrdinalPosition]      Int Null,
	[ReferencedSchemaName] SysName Null,
	[ReferencedTableName]  SysName Null,
	[ReferencedColumnName] SysName Null,
	[SysStart]             DateTime2 (7) Not Null,
	[SysEnd]               DateTime2 (7)  Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_ConstraintColumn]
    ON [HsCatalog].[ConstraintColumn]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_ConstraintColumn]
    ON [HsCatalog].[ConstraintColumn]([ConstraintColumnId] ASC)
GO
