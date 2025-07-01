CREATE TABLE [AppCatalog].[Table]
(
	-- [INFORMATION_SCHEMA] both a [INFORMATION_SCHEMA].[TABLES] & [INFORMATION_SCHEMA].[VIEWS]
	-- There is [INFORMATION_SCHEMA].[COLUMNS] representing columns from both.
	-- This does not work for Referential Integrity purposes.
	-- A design decision was made to treat Tables and Views as a Sub-Type (de-normalized).
	-- The TableType delineates each of the sub-types as well as Temporal/Historic tables.
	[TableId]             UniqueIdentifier Not Null CONSTRAINT [DF_TableId] DEFAULT (newid()),
	[SchemaId]            UniqueIdentifier Not Null,
	[TableName]           SysName Not Null,
	[TableType]           [AppGeneral].[uddtObjectType] Null, -- BASE TABLE, VIEW, HISTORY TABLE, TEMPTORAL TABLE
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Table_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Table_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Table] PRIMARY KEY CLUSTERED ([TableId] ASC),
	CONSTRAINT [FK_TableSchema] FOREIGN KEY ([SchemaId]) REFERENCES [AppCatalog].[Schema] ([SchemaId]),
	CONSTRAINT [CK_TableType] CHECK ([TableType]='View' OR [TableType]='History Table' OR [TableType]='Temporal Table' OR [TableType]='Table' OR [TableType] IS NULL),
)  WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsCatalog].[Table]))
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Table]
    ON [AppCatalog].[Table]([TableName], [SchemaId]);
GO
