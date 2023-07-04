CREATE TABLE [App_DataDictionary].[DatabaseSchema]
(
	[CatalogId] UniqueIdentifier Not Null,
	[SchemaName] SysName Not Null,
	-- Domain Mappings
	[ScopeName] As ([SchemaName]), -- Name of Level0. Examples is Schema, Synonym, and User Name
	[ScopeType] As (Convert([sysname],'SCHEMA')) -- Type of Level0. Examples is Schema, Synonym, and User
	-- Keys
	CONSTRAINT [PK_DatabaseSchema] PRIMARY KEY CLUSTERED ([CatalogId] ASC, [SchemaName] ASC),
	CONSTRAINT [FK_DatabaseSchemaCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
    -- Select Db_Name() As [SCHEMA_CATALOG], [name] As [SCHEMA_NAME] From [Sys].[Schemas]
)
