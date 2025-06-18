CREATE TABLE [AppCatalog].[Catalog]
(
	-- The Catalog or Database Schema side of the Model assumes the data is always coming from the database.
	-- The objects are never edited directly by the end-users. (This is not a DDL tool)
	-- Instead the objects are used to provide a Snapshot of the Database and objects of interest.
	-- The Object names are used as the key rather then creating a surrogate key that needs to be reconciled.
	[CatalogId] UniqueIdentifier Not Null CONSTRAINT [DF_CatalogId] DEFAULT (newid()),
	[CatalogTitle] [AppGeneral].[typeTitle] Not Null,
	[CatalogDescription] [AppGeneral].[typeDescription] Null,
	[ServerName] SysName Not Null,
	[DatabaseName] SysName Not Null,
	[SourceDate] DateTime Not Null,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Catalog_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Catalog_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Catalog] PRIMARY KEY CLUSTERED ([CatalogId] ASC),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsCatalog].[Catalog]))
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_CatalogTitle]
    ON [AppCatalog].[Catalog]([CatalogTitle]);
GO
