CREATE VIEW [App_DataDictionary].[DatabaseDomain_AK]
WITH SCHEMABINDING AS
-- Enforces Natural Key for the Database Domain (Data Type)
-- Key components exist over multiple tables. Keys on the tables only partially enforce unique names.
Select	C.[CatalogId],
		S.[SchemaId],
		T.[DomainId],
		C.[SourceDatabaseName] As [DatabaseName],
		S.[SchemaName],
		T.[DomainName]
From	[App_DataDictionary].[DatabaseCatalog] C
		Inner Join [App_DataDictionary].[DatabaseSchema] S
		On	C.[CatalogId] = S.[CatalogId]
		Inner Join [App_DataDictionary].[DatabaseDomain] T
		On	S.[SchemaId] = T.[SchemaId]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_DatabaseDomain]
    ON [App_DataDictionary].[DatabaseDomain_AK]([DomainId])
GO
CREATE UNIQUE INDEX [AK_DatabaseDomain]
    ON [App_DataDictionary].[DatabaseDomain_AK]([DatabaseName] ASC, [SchemaName] ASC, [DomainName] ASC, [CatalogId] ASC)
GO