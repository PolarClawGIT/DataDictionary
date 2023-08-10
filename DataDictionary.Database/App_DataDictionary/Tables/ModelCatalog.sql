CREATE TABLE [App_DataDictionary].[ModelCatalog]
(
	-- It is possible to have a single catalog used in multiple models.
	[ModelId] UniqueIdentifier NOT NULL,
	[CatalogId] UniqueIdentifier NOT NULL,
	-- Keys
	CONSTRAINT [PK_ModelCatalog] PRIMARY KEY CLUSTERED ([ModelId] ASC, [CatalogId] ASC),
	CONSTRAINT [FK_ModelCatalogModel] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelCatalogDatabaseCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
)
