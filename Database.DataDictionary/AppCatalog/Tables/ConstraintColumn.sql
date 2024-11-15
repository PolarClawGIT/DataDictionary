CREATE TABLE [AppCatalog].[ConstraintColumn]
(
	[ConstraintColumnId]   UniqueIdentifier Not Null CONSTRAINT [DF_ConstraintColumnId] DEFAULT (newid()),
	[ConstraintId]         UniqueIdentifier Not Null,
	[ColumnId]             UniqueIdentifier Not Null,
	[OrdinalPosition]      Int Null,
	[ReferencedSchemaName] SysName Null,
	[ReferencedTableName]  SysName Null,
	[ReferencedColumnName] SysName Null,
	-- TODO: Add System Version later once the schema is locked down. Not needed for Db Schema?
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ConstraintColumn_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ConstraintColumn_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ConstraintColumn] PRIMARY KEY CLUSTERED ([ConstraintColumnId]),
	CONSTRAINT [FK_Constraint] FOREIGN KEY ([ConstraintId]) REFERENCES [AppCatalog].[Constraint] ([ConstraintId]),
	CONSTRAINT [FK_ConstraintTableColumn] FOREIGN KEY ([ColumnId]) REFERENCES [AppCatalog].[TableColumn] ([ColumnId]),
	CONSTRAINT [CK_ConstraintReferenced] CHECK (([ReferencedSchemaName] IS NULL AND [ReferencedTableName] IS NULL AND [ReferencedColumnName] IS NULL) OR ([ReferencedSchemaName] IS NOT NULL AND [ReferencedTableName] IS NOT NULL AND [ReferencedColumnName] IS NOT NULL)),
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsCatalog].[ConstraintColumn]))
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_ConstraintColumn]
    ON [AppCatalog].[ConstraintColumn]([ConstraintId], [ReferencedSchemaName], [ReferencedTableName], [ReferencedColumnName])
	Where [ReferencedSchemaName] is not null And [ReferencedTableName] is not null And [ReferencedColumnName] is not null
GO