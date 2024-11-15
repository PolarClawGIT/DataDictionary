CREATE VIEW [App_DataDictionary].[DatabaseConstraintColumn_AK]
WITH SCHEMABINDING AS
-- Enforces Natural Key for the Database Constraint Column
-- Key components exist over multiple tables. Keys on the tables only partially enforce unique names.
Select	C.[CatalogId],
		S.[SchemaId],
		T.[ConstraintId],
		L.[ConstraintColumnId],
		T.[TableId],
		L.[ColumnId],
		C.[DatabaseName] As [DatabaseName],
		S.[SchemaName],
		T.[ConstraintName],
		N.[TableName],
		M.[ColumnName]
From	[AppCatalog].[Catalog] C
		Inner Join [AppCatalog].[Schema] S
		On	C.[CatalogId] = S.[CatalogId]
		Inner Join [App_DataDictionary].[DatabaseConstraint] T
		On	S.[SchemaId] = T.[SchemaId]
		Inner Join [App_DataDictionary].[DatabaseConstraintColumn] L
		On	T.[ConstraintId] = L.[ConstraintId]
		Inner Join [AppCatalog].[Table] N
		On	T.[TableId] = N.[TableId]
		Inner Join [AppCatalog].[TableColumn] M
		On	L.[ColumnId] = M.[ColumnId]
GO
/*
-- Policy and Indexed Views are not compatible.
CREATE UNIQUE CLUSTERED INDEX [PK_DatabaseConstraintColumn]
    ON [App_DataDictionary].[DatabaseConstraintColumn_AK]([ConstraintColumnId])
GO
CREATE UNIQUE INDEX [AK_DatabaseConstraintColumn]
    ON [App_DataDictionary].[DatabaseConstraintColumn_AK]([DatabaseName] ASC, [SchemaName] ASC, [ConstraintName] ASC, [ColumnName] ASC, [CatalogId] ASC)
GO
*/