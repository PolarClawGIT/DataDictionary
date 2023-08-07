CREATE TABLE [App_DataDictionary].[DatabaseConstraintColumn]
(
	[CatalogId]          UniqueIdentifier Not Null,
	[SchemaName]         SysName Not Null,
	[ConstraintName]     SysName Not Null,
	[ReferenceSchemaName] SysName Not Null,
	[ReferenceTableName]  SysName Not Null,
	[ReferenceColumnName] SysName Not Null,
	[OrdinalPosition]    Int Null,
	CONSTRAINT [PK_DatabaseConstraintColumn] PRIMARY KEY CLUSTERED ([CatalogId] ASC, [SchemaName] ASC, [ConstraintName] ASC, [ReferenceSchemaName] ASC, [ReferenceTableName] ASC, [ReferenceColumnName] ASC),
	--CONSTRAINT [FK_DatabaseConstraintColumnCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
	CONSTRAINT [FK_DatabaseConstraint] FOREIGN KEY ([CatalogId], [SchemaName], [ConstraintName]) REFERENCES [App_DataDictionary].[DatabaseConstraint] ([CatalogId], [SchemaName], [ConstraintName]),
	CONSTRAINT [FK_DatabaseConstraintRefrenceColumn] FOREIGN KEY ([CatalogId], [ReferenceSchemaName], [ReferenceTableName], [ReferenceColumnName]) REFERENCES [App_DataDictionary].[DatabaseTableColumn] ([CatalogId], [SchemaName], [TableName], [ColumnName])

)
