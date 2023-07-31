CREATE TABLE [App_DataDictionary].[DatabaseConstraintColumn]
(
	[CatalogId]          UniqueIdentifier Not Null,
	[SchemaName]         SysName Not Null,
	[ConstraintName]     SysName Not Null,
	[RefrenceSchemaName] SysName Not Null,
	[RefrenceTableName]  SysName Not Null,
	[RefrenceColumnName] SysName Not Null,
	[OrdinalPosition]    Int Null,
	CONSTRAINT [PK_DatabaseConstraintColumn] PRIMARY KEY CLUSTERED ([CatalogId] ASC, [SchemaName] ASC, [ConstraintName] ASC, [RefrenceSchemaName] ASC, [RefrenceTableName] ASC, [RefrenceColumnName] ASC),
	--CONSTRAINT [FK_DatabaseConstraintColumnCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
	CONSTRAINT [FK_DatabaseConstraint] FOREIGN KEY ([CatalogId], [SchemaName], [ConstraintName]) REFERENCES [App_DataDictionary].[DatabaseConstraint] ([CatalogId], [SchemaName], [ConstraintName]),
	CONSTRAINT [FK_DatabaseConstraintRefrenceColumn] FOREIGN KEY ([CatalogId], [RefrenceSchemaName], [RefrenceTableName], [RefrenceColumnName]) REFERENCES [App_DataDictionary].[DatabaseColumn] ([CatalogId], [SchemaName], [TableName], [ColumnName])

)
