CREATE TABLE [HsModel].[ModelCatalog]
(
	[ModelId]   UniqueIdentifier NOT NULL,
	[CatalogId] UniqueIdentifier NOT NULL,
	[SysStart]  DateTime2 (7) NOT NULL,
	[SysEnd]    DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_ModelCatalog]
    ON [HsModel].[ModelCatalog]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_ModelCatalog]
    ON [HsModel].[ModelCatalog]([ModelId] ASC, [CatalogId] ASC)
GO