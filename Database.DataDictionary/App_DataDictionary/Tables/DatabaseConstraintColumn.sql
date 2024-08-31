CREATE TABLE [App_DataDictionary].[DatabaseConstraintColumn]
(
	[ConstraintColumnId]   UniqueIdentifier Not Null CONSTRAINT [DF_DatabaseConstraintColumnId] DEFAULT (newid()),
	[ConstraintId]         UniqueIdentifier Not Null,
	[ColumnId]             UniqueIdentifier Not Null,
	[OrdinalPosition]      Int Null,
	[ReferencedSchemaName] SysName Null,
	[ReferencedTableName]  SysName Null,
	[ReferencedColumnName] SysName Null,
	-- TODO: Add System Version later once the schema is locked down. Not needed for Db Schema?
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DatabaseConstraintColumn_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DatabaseConstraintColumn_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DatabaseConstraintColumn_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DatabaseConstraintColumn] PRIMARY KEY CLUSTERED ([ConstraintColumnId]),
	CONSTRAINT [FK_DatabaseConstraint] FOREIGN KEY ([ConstraintId]) REFERENCES [App_DataDictionary].[DatabaseConstraint] ([ConstraintId]),
	CONSTRAINT [FK_DatabaseConstraintTableColumn] FOREIGN KEY ([ColumnId]) REFERENCES [App_DataDictionary].[DatabaseTableColumn] ([ColumnId]),
	CONSTRAINT [CK_DatabaseConstraintReferenced] CHECK (([ReferencedSchemaName] IS NULL AND [ReferencedTableName] IS NULL AND [ReferencedColumnName] IS NULL) OR ([ReferencedSchemaName] IS NOT NULL AND [ReferencedTableName] IS NOT NULL AND [ReferencedColumnName] IS NOT NULL)),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DatabaseConstraintColumn]
    ON [App_DataDictionary].[DatabaseConstraintColumn]([ConstraintId], [ReferencedSchemaName], [ReferencedTableName], [ReferencedColumnName])
	Where [ReferencedSchemaName] is not null And [ReferencedTableName] is not null And [ReferencedColumnName] is not null
GO