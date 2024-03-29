CREATE VIEW [App_DataDictionary].[DatabaseTable_AK]
WITH SCHEMABINDING AS
-- Enforces Natural Key for the Database Table (or View)
-- Key components exist over multiple tables. Keys on the tables only partially enforce unique names.
Select	C.[CatalogId],
		S.[SchemaId],
		T.[TableId],
		C.[SourceDatabaseName] As [DatabaseName],
		S.[SchemaName],
		T.[TableName],
		T.[TableType]
From	[App_DataDictionary].[DatabaseCatalog] C
		Inner Join [App_DataDictionary].[DatabaseSchema] S
		On	C.[CatalogId] = S.[CatalogId]
		Inner Join [App_DataDictionary].[DatabaseTable] T
		On	S.[SchemaId] = T.[SchemaId]

GO
CREATE UNIQUE CLUSTERED INDEX [PK_DatabaseTable]
    ON [App_DataDictionary].[DatabaseTable_AK]([TableId])
GO
CREATE UNIQUE INDEX [AK_DatabaseTable]
    ON [App_DataDictionary].[DatabaseTable_AK]([DatabaseName] ASC, [SchemaName] ASC, [TableName] ASC, [CatalogId] ASC)
GO