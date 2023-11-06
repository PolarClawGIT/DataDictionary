CREATE VIEW [App_DataDictionary].[DatabaseConstraint_AK]
WITH SCHEMABINDING AS
-- Enforces Natural Key for the Database Constraint
-- Key components exist over multiple tables. Keys on the tables only partially enforce unique names.
Select	C.[CatalogId],
		S.[SchemaId],
		T.[ConstraintId],
		C.[SourceDatabaseName] As [DatabaseName],
		S.[SchemaName],
		T.[ConstraintName],
		N.[TableName]
From	[App_DataDictionary].[DatabaseCatalog] C
		Inner Join [App_DataDictionary].[DatabaseSchema] S
		On	C.[CatalogId] = S.[CatalogId]
		Inner Join [App_DataDictionary].[DatabaseConstraint] T
		On	S.[SchemaId] = T.[SchemaId]
		Inner Join [App_DataDictionary].[DatabaseTable] N
		On	T.[ParentTableId] = N.[TableId]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_DatabaseConstraint]
    ON [App_DataDictionary].[DatabaseConstraint_AK]([ConstraintId])
GO
CREATE UNIQUE INDEX [AK_DatabaseConstraint]
    ON [App_DataDictionary].[DatabaseConstraint_AK]([DatabaseName] ASC, [SchemaName] ASC, [ConstraintName] ASC, [CatalogId] ASC)
GO