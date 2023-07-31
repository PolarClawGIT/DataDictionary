﻿CREATE TABLE [App_DataDictionary].[ApplicationCatalog]
(
	-- It is possible to have a single catalog used in multiple models.
	[ModelId] UniqueIdentifier NOT NULL,
	[CatalogId] UniqueIdentifier NOT NULL,
	-- Keys
	CONSTRAINT [PK_ApplicationCatalog] PRIMARY KEY CLUSTERED ([ModelId] ASC, [CatalogId] ASC),
	CONSTRAINT [FK_ApplicationCatalog_Model] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[ApplicationModel] ([ModelId]),
	CONSTRAINT [FK_ApplicationDatabaseCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
)
