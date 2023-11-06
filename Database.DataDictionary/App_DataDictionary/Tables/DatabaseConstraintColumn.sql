CREATE TABLE [App_DataDictionary].[DatabaseConstraintColumn]
(
	[ConstraintColumnId]  UniqueIdentifier Not Null CONSTRAINT [DF_DatabaseConstraintColumnId] DEFAULT (newid()),
	[ConstraintId]        UniqueIdentifier Not Null,
	[ParentColumnId]      UniqueIdentifier Not Null,
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
	CONSTRAINT [PK_DatabaseConstraintColumn] PRIMARY KEY CLUSTERED ([ConstraintColumnId]),
	CONSTRAINT [FK_DatabaseConstraint] FOREIGN KEY ([ConstraintId]) REFERENCES [App_DataDictionary].[DatabaseConstraint] ([ConstraintId]),
	CONSTRAINT [FK_DatabaseConstraintTableColumn] FOREIGN KEY ([ParentColumnId]) REFERENCES [App_DataDictionary].[DatabaseTableColumn] ([ColumnId]),
)
GO
