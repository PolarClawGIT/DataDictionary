CREATE TABLE [App_DataDictionary].[DatabaseTable]
(
	[CatalogId] UniqueIdentifier Not Null,
	[SchemaName] SysName Not Null,
	[TableName] SysName Not Null,
	-- Domain Mappings
	[ScopeName] As ([SchemaName]), -- Name of Level0. Examples is Schema, Synonym, and User Name
	[ScopeType] As (Convert([sysname],'SCHEMA')), -- Type of Level0. Examples is Schema, Synonym, and User
	[ObjectName] As ([TableName]), -- Name of Level1. Examples are Table, View, Procedure and Function Name
	[ObjectType] As (Convert([sysname],'TABLE')), -- Type of Level1. Examples are Table, View, Procedure and Function
	-- Keys
	CONSTRAINT [PK_DatabaseTable] PRIMARY KEY CLUSTERED ([CatalogId] ASC, [SchemaName] ASC, [TableName] ASC),
	CONSTRAINT [FK_DatabaseTableCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
)
