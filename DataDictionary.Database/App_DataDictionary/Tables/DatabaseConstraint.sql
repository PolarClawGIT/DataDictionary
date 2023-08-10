CREATE TABLE [App_DataDictionary].[DatabaseConstraint]
(
	-- [INFORMATION_SCHEMA] does not contain Indexes.
	-- For the purpose of this applications, non-key indexes are not very interesting.
	-- The additional values can be gotten from Sys.Indexes.
	[CatalogId]          UniqueIdentifier Not Null,
	[SchemaName]         SysName Not Null,
	[ConstraintName]     SysName Not Null,
	[ConstraintType]     NVarChar(60) Null, -- Known types: FOREIGN KEY, UNIQUE, PRIMARY KEY
	CONSTRAINT [PK_DatabaseConstraint] PRIMARY KEY CLUSTERED ([CatalogId] ASC, [SchemaName] ASC, [ConstraintName] ASC),
	--CONSTRAINT [FK_DatabaseConstraintCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
	CONSTRAINT [FK_DatabaseConstraintSchema] FOREIGN KEY ([CatalogId], [SchemaName]) REFERENCES [App_DataDictionary].[DatabaseSchema] ([CatalogId], [SchemaName]),
)
