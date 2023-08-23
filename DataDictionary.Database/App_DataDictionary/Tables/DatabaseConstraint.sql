CREATE TABLE [App_DataDictionary].[DatabaseConstraint]
(
	-- [INFORMATION_SCHEMA] does not contain Indexes.
	-- For the purpose of this applications, non-key indexes are not very interesting.
	-- The additional values can be gotten from Sys.Indexes.
	[CatalogId]          UniqueIdentifier Not Null,
	[SchemaName]         SysName Not Null,
	[ConstraintName]     SysName Not Null,
	[TableName]          SysName Null, -- Needed for Extended Property Call at Constraint level
	[ConstraintType]     NVarChar(60) Null, -- Known types: FOREIGN KEY, UNIQUE, PRIMARY KEY
	-- TODO: Add System Version later once the schema is locked down. Not needed for Db Schema?
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DatabaseConstraint_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DatabaseConstraint_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DatabaseConstraint_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DatabaseConstraint] PRIMARY KEY CLUSTERED ([CatalogId] ASC, [SchemaName] ASC, [ConstraintName] ASC),
	--CONSTRAINT [FK_DatabaseConstraintTable] FOREIGN KEY ([CatalogId], [SchemaName], [TableName]) REFERENCES [App_DataDictionary].[DatabaseTable] ([CatalogId], [SchemaName], [TableName])
)
