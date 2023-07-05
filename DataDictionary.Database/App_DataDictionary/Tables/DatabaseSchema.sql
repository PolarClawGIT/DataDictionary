CREATE TABLE [App_DataDictionary].[DatabaseSchema]
(
	[CatalogId] UniqueIdentifier Not Null,
	[SchemaName] SysName Not Null,
	-- Keys
	CONSTRAINT [PK_DatabaseSchema] PRIMARY KEY CLUSTERED ([CatalogId] ASC, [SchemaName] ASC),
	CONSTRAINT [FK_DatabaseSchemaCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
)
/*
Select	Convert(UniqueIdentifier,Null) As [CatalogId],
		[CATALOG_NAME],
		[SCHEMA_NAME]
From	[INFORMATION_SCHEMA].[SCHEMATA]
*/