CREATE TABLE [HsCatalog].[Catalog]
(
	[CatalogId]          UniqueIdentifier Not Null,
	[CatalogTitle]       [App_DataDictionary].[typeTitle] Not Null,
	[CatalogDescription] [App_DataDictionary].[typeDescription] Null,
	[SourceServerName]   SysName Not Null,
	[SourceDatabaseName] SysName Not Null,
	[SourceDate]         DateTime Not Null,
	[ModifiedBy]         SysName Not Null,
	[SysStart]           DATETIME2 (7) Not Null,
	[SysEnd]             DATETIME2 (7)  Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_Catalog]
    ON [HsCatalog].[Catalog]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Catalog]
    ON [HsCatalog].[Catalog]([CatalogId] ASC)
GO