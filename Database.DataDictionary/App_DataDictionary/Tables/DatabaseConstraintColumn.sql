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
	-- TODO: Add System Version later once the schema is locked down. Not needed for Db Schema?
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DatabaseConstraintColumn_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DatabaseConstraintColumn_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DatabaseConstraintColumn_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DatabaseConstraintColumn] PRIMARY KEY CLUSTERED ([CatalogId] ASC, [SchemaName] ASC, [ConstraintName] ASC, [TableName] ASC, [ColumnName]),
	--CONSTRAINT [FK_DatabaseConstraintColumnCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
	CONSTRAINT [FK_DatabaseConstraint] FOREIGN KEY ([CatalogId], [SchemaName], [ConstraintName]) REFERENCES [App_DataDictionary].[DatabaseConstraint] ([CatalogId], [SchemaName], [ConstraintName]),
	CONSTRAINT [FK_DatabaseConstraintColumnKey] FOREIGN KEY ([CatalogId], [SchemaName], [TableName], [ColumnName]) REFERENCES [App_DataDictionary].[DatabaseTableColumn] ([CatalogId], [SchemaName], [TableName], [ColumnName]),
	CONSTRAINT [FK_DatabaseConstraintColumnRefrence] FOREIGN KEY ([CatalogId], [ReferenceSchemaName], [ReferenceTableName], [ReferenceColumnName]) REFERENCES [App_DataDictionary].[DatabaseTableColumn] ([CatalogId], [SchemaName], [TableName], [ColumnName])

)
