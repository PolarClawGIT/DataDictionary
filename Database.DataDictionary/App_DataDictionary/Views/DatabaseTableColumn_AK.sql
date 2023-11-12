CREATE VIEW [App_DataDictionary].[DatabaseTableColumn_AK]
WITH SCHEMABINDING AS
-- Enforces Natural Key for the Database Table Column
-- Key components exist over multiple tables. Keys on the tables only partially enforce unique names.
Select	C.[CatalogId],
		S.[SchemaId],
		T.[TableId],
		O.[ColumnId],
		C.[SourceDatabaseName] As [DatabaseName],
		S.[SchemaName],
		T.[TableName],
		O.[ColumnName],
		FormatMessage('[%s].[%s].[%s].[%s]', C.[SourceDatabaseName], S.[SchemaName], T.[TableName], O.[ColumnName]) As [AliasName]
From	[App_DataDictionary].[DatabaseCatalog] C
		Inner Join [App_DataDictionary].[DatabaseSchema] S
		On	C.[CatalogId] = S.[CatalogId]
		Inner Join [App_DataDictionary].[DatabaseTable] T
		On	S.[SchemaId] = T.[SchemaId]
		Inner Join [App_DataDictionary].[DatabaseTableColumn] O
		On	T.[TableId] = O.[TableId]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_DatabaseTableColumn]
    ON [App_DataDictionary].[DatabaseTableColumn_AK]([ColumnId])
GO
CREATE UNIQUE INDEX [AK_DatabaseTableColumn]
    ON [App_DataDictionary].[DatabaseTableColumn_AK]([DatabaseName] ASC, [TableName] ASC, [SchemaName] ASC, [ColumnName] ASC, [CatalogId] ASC)
GO