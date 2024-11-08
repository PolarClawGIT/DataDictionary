CREATE VIEW [App_DataDictionary].[DatabaseTableColumn_AK]
WITH SCHEMABINDING AS
-- Enforces Natural Key for the Database Table Column
-- Key components exist over multiple tables. Keys on the tables only partially enforce unique names.
Select	C.[CatalogId],
		S.[SchemaId],
		T.[TableId],
		O.[ColumnId],
		C.[DatabaseName] As [DatabaseName],
		S.[SchemaName],
		T.[TableName],
		T.[TableType],
		O.[ColumnName]
From	[AppCatalog].[Catalog] C
		Inner Join [AppCatalog].[Schema] S
		On	C.[CatalogId] = S.[CatalogId]
		Inner Join [App_DataDictionary].[DatabaseTable] T
		On	S.[SchemaId] = T.[SchemaId]
		Inner Join [App_DataDictionary].[DatabaseTableColumn] O
		On	T.[TableId] = O.[TableId]
GO
/*
-- Policy and Indexed Views are not compatible.
CREATE UNIQUE CLUSTERED INDEX [PK_DatabaseTableColumn]
    ON [App_DataDictionary].[DatabaseTableColumn_AK]([ColumnId])
GO
CREATE UNIQUE INDEX [AK_DatabaseTableColumn]
    ON [App_DataDictionary].[DatabaseTableColumn_AK]([DatabaseName] ASC, [TableName] ASC, [SchemaName] ASC, [ColumnName] ASC, [CatalogId] ASC)
GO
*/