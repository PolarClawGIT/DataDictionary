CREATE TABLE [HsCatalog].[CatalogModel]
(
	[ModelId]   UniqueIdentifier NOT NULL,
	[CatalogId] UniqueIdentifier NOT NULL,
	[SysStart]  DateTime2 (7) NOT NULL,
	[SysEnd]    DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_ModelCatalog]
    ON [HsCatalog].[CatalogModel]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_ModelCatalog]
    ON [HsCatalog].[CatalogModel]([ModelId] ASC, [CatalogId] ASC)
GO