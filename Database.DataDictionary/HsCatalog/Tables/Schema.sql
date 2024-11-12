CREATE TABLE [HsCatalog].[Schema]
(
	[SchemaId]           UniqueIdentifier Not Null,
	[CatalogId]          UniqueIdentifier Not Null,
	[SchemaName]         SysName Not Null,
	[CreatedBy]          SysName Not Null,
	[SysStart]           DateTime2 (7) Not Null,
	[SysEnd]             DateTime2 (7)  Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_Schema]
    ON [HsCatalog].[Schema]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Schema]
    ON [HsCatalog].[Schema]([CatalogId] ASC)
GO