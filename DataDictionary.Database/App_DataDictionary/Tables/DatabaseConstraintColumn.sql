CREATE TABLE [App_DataDictionary].[DatabaseConstraintColumn]
(
	[CatalogId]           UniqueIdentifier Not Null,
	[SchemaName]          SysName Not Null,
	[ConstraintName]      SysName Not Null,
	[TableName]           SysName Not Null,
	[ColumnName]          SysName Not Null,
	[OrdinalPosition]     Int Null,
	[ReferenceSchemaName] SysName Null,
	[ReferenceTableName]  SysName Null,
	[ReferenceColumnName] SysName Null,
	CONSTRAINT [PK_DatabaseConstraintColumn] PRIMARY KEY CLUSTERED ([CatalogId] ASC, [SchemaName] ASC, [ConstraintName] ASC, [TableName] ASC, [ColumnName]),
	--CONSTRAINT [FK_DatabaseConstraintColumnCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
	CONSTRAINT [FK_DatabaseConstraint] FOREIGN KEY ([CatalogId], [SchemaName], [ConstraintName]) REFERENCES [App_DataDictionary].[DatabaseConstraint] ([CatalogId], [SchemaName], [ConstraintName]),
	CONSTRAINT [FK_DatabaseConstraintColumnKey] FOREIGN KEY ([CatalogId], [SchemaName], [TableName], [ColumnName]) REFERENCES [App_DataDictionary].[DatabaseTableColumn] ([CatalogId], [SchemaName], [TableName], [ColumnName]),
	CONSTRAINT [FK_DatabaseConstraintColumnRefrence] FOREIGN KEY ([CatalogId], [ReferenceSchemaName], [ReferenceTableName], [ReferenceColumnName]) REFERENCES [App_DataDictionary].[DatabaseTableColumn] ([CatalogId], [SchemaName], [TableName], [ColumnName])

)
