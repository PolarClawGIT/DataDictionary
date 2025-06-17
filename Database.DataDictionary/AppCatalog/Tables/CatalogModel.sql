CREATE TABLE [AppCatalog].[CatalogModel]
(
	-- Catalog(s) associated with a Model
	[ModelId]   UniqueIdentifier NOT NULL,
	[CatalogId] UniqueIdentifier NOT NULL,
	-- Temporal History Support
	[SysStart]  DateTime2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelCatalog_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]    DateTime2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelCatalog_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ModelCatalog] PRIMARY KEY CLUSTERED ([ModelId] ASC, [CatalogId] ASC),
	CONSTRAINT [FK_ModelCatalogModel] FOREIGN KEY ([ModelId]) REFERENCES [AppModel].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelCatalogDatabaseCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [AppCatalog].[Catalog] ([CatalogId]),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsCatalog].[CatalogModel]))
