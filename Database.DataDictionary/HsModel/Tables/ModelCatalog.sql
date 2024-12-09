CREATE TABLE [HsModel].[ModelCatalog]
(
	-- It is possible to have a single catalog used in multiple models.
	[ModelId]   UniqueIdentifier NOT NULL,
	[CatalogId] UniqueIdentifier NOT NULL,
	-- TODO: Add System Version later once the schema is locked down
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