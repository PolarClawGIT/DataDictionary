CREATE TABLE [AppCatalog].[Schema]
(
	[SchemaId]   UniqueIdentifier Not Null CONSTRAINT [DF_SchemaId] DEFAULT (newid()),
	[CatalogId]  UniqueIdentifier Not Null,
	[SchemaName] SysName Not Null,
	-- Temporal History Support
	[CreatedBy] SysName Not Null CONSTRAINT [DF_Schema_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Schema_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Schema_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Schema] PRIMARY KEY CLUSTERED ([SchemaId] ASC),
	CONSTRAINT [FK_SchemaCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [AppCatalog].[Catalog] ([CatalogId]),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsCatalog].[Schema]))
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Schema]
    ON [AppCatalog].[Schema]([SchemaName], [CatalogId]);
GO