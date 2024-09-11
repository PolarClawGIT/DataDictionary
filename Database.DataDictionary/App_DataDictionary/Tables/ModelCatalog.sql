CREATE TABLE [App_DataDictionary].[ModelCatalog]
(
	-- It is possible to have a single catalog used in multiple models.
	[ModelId] UniqueIdentifier NOT NULL,
	[CatalogId] UniqueIdentifier NOT NULL,
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_ModelCatalog_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelCatalog_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelCatalog_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ModelCatalog] PRIMARY KEY CLUSTERED ([ModelId] ASC, [CatalogId] ASC),
	CONSTRAINT [FK_ModelCatalogModel] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelCatalogDatabaseCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
)
