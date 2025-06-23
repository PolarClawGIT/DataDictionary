CREATE TABLE [HsCatalog].[Catalog]
(
	[CatalogId]          UniqueIdentifier Not Null,
	[CatalogTitle]       [AppGeneral].[uddtTitle] Not Null,
	[CatalogDescription] [AppGeneral].[uddtDescription] Null,
	[ServerName]         SysName Not Null,
	[DatabaseName]       SysName Not Null,
	[SourceDate]         DateTime Not Null,
	[SysStart]           DateTime2 (7) Not Null,
	[SysEnd]             DateTime2 (7)  Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_Catalog]
    ON [HsCatalog].[Catalog]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Catalog]
    ON [HsCatalog].[Catalog]([CatalogId] ASC)
GO