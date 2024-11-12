CREATE TABLE [HsCatalog].[Catalog]
(
	[CatalogId]          UniqueIdentifier Not Null,
	[CatalogTitle]       [App_DataDictionary].[typeTitle] Not Null,
	[CatalogDescription] [App_DataDictionary].[typeDescription] Null,
	[ServerName]         SysName Not Null,
	[DatabaseName]       SysName Not Null,
	[SourceDate]         DateTime Not Null,
	[CreatedBy]          SysName Not Null,
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