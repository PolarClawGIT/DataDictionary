CREATE TABLE [App_DataDictionary].[DatabaseSchema]
(
	[SchemaId]   UniqueIdentifier Not Null CONSTRAINT [DF_DatabaseSchemaId] DEFAULT (newid()),
	[CatalogId]  UniqueIdentifier Not Null,
	[SchemaName] SysName Not Null,
	[ScopeId]    Int Not Null,
	-- TODO: Add System Version later once the schema is locked down. Not needed for Db Schema?
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DatabaseSchema_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DatabaseSchema_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DatabaseSchema_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DatabaseSchema] PRIMARY KEY CLUSTERED ([SchemaId] ASC),
	CONSTRAINT [FK_DatabaseSchemaCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
	CONSTRAINT [FK_DatabaseSchemaScope] FOREIGN KEY ([ScopeId]) REFERENCES [App_DataDictionary].[ApplicationScope] ([ScopeId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DatabaseSchema]
    ON [App_DataDictionary].[DatabaseSchema]([SchemaName], [CatalogId]);
GO