CREATE TABLE [App_DataDictionary].[DatabaseTable]
(
	-- Note: GetSchema combines Views and Tables.
	-- [INFORMATION_SCHEMA] both a [INFORMATION_SCHEMA].[TABLES] & [INFORMATION_SCHEMA].[VIEWS]
	-- but only a [INFORMATION_SCHEMA].[COLUMNS] representing columns from both.
	-- For Referential Integrity purposes, the GetSchema approach is used where both views and tables are returned in a single list.
	-- Temporal and History tables as different table types.
	[TableId]             UniqueIdentifier Not Null CONSTRAINT [DF_DatabaseTableId] DEFAULT (newid()),
	[SchemaId]            UniqueIdentifier Not Null,
	[TableName]           SysName Not Null,
	[TableType]           [App_DataDictionary].[typeObjectType] Null, -- BASE TABLE, VIEW, HISTORY TABLE, TEMPTORAL TABLE
	-- TODO: Add System Version later once the schema is locked down. Not needed for Db Schema?
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DatabaseTable_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DatabaseTable_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DatabaseTable_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DatabaseTable] PRIMARY KEY CLUSTERED ([TableId] ASC),
	CONSTRAINT [FK_DatabaseTableSchema] FOREIGN KEY ([SchemaId]) REFERENCES [App_DataDictionary].[DatabaseSchema] ([SchemaId]),
	CONSTRAINT [CK_DatabaseTableType] CHECK ([TableType]='View' OR [TableType]='History Table' OR [TableType]='Temporal Table' OR [TableType]='Table' OR [TableType] IS NULL),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DatabaseTable]
    ON [App_DataDictionary].[DatabaseTable]([TableName], [SchemaId]);
GO
