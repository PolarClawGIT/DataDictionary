CREATE TABLE [App_DataDictionary].[ApplicationCatalog]
(
	[ModelId] UniqueIdentifier NOT NULL,
	[CatalogId] UniqueIdentifier NOT NULL,
	-- Keys
	CONSTRAINT [PK_ApplicationCatalog] PRIMARY KEY CLUSTERED ([ModelId] ASC, [CatalogId] ASC),
	CONSTRAINT [FK_ApplicationCatalog_Model] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[ApplicationModel] ([ModelId]),
	CONSTRAINT [FK_ApplicationDatabaseCatalog] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
)
