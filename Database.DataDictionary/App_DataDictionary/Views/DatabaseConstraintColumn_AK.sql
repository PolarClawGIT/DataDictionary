CREATE VIEW [App_DataDictionary].[DatabaseConstraintColumn_AK]
WITH SCHEMABINDING AS
-- Enforces Natural Key for the Database Constraint Column
-- Key components exist over multiple tables. Keys on the tables only partially enforce unique names.
Select	C.[CatalogId],
		S.[SchemaId],
		T.[ConstraintId],
		L.[ConstraintColumnId],
		T.[ParentTableId],
		L.[ParentColumnId],
		C.[SourceDatabaseName] As [DatabaseName],
		S.[SchemaName],
		T.[ConstraintName],
		N.[TableName],
		M.[ColumnName]
From	[App_DataDictionary].[DatabaseCatalog] C
		Inner Join [App_DataDictionary].[DatabaseSchema] S
		On	C.[CatalogId] = S.[CatalogId]
		Inner Join [App_DataDictionary].[DatabaseConstraint] T
		On	S.[SchemaId] = T.[SchemaId]
		Inner Join [App_DataDictionary].[DatabaseConstraintColumn] L
		On	T.[ConstraintId] = L.[ConstraintId]
		Inner Join [App_DataDictionary].[DatabaseTable] N
		On	T.[ParentTableId] = N.[TableId]
		Inner Join [App_DataDictionary].[DatabaseTableColumn] M
		On	L.[ParentColumnId] = M.[ColumnId]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_DatabaseConstraintColumn]
    ON [App_DataDictionary].[DatabaseConstraintColumn_AK]([ConstraintColumnId])
GO
CREATE UNIQUE INDEX [AK_DatabaseConstraintColumn]
    ON [App_DataDictionary].[DatabaseConstraintColumn_AK]([DatabaseName] ASC, [SchemaName] ASC, [ConstraintName] ASC, [ColumnName] ASC, [CatalogId] ASC)
GO