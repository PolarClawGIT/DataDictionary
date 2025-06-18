CREATE TABLE [AppCatalog].[Constraint]
(
	-- [INFORMATION_SCHEMA] does not contain Indexes.
	-- For the purpose of this applications, non-key indexes are not very interesting.
	-- The additional values can be gotten from Sys.Indexes.
	-- The INFORMATION_SCHEMA allows the constraint to be in a different database/schema then
	-- the table is is on. RDBS do not allow the table and constraint to be in different schema's.
	[ConstraintId]        UniqueIdentifier Not Null CONSTRAINT [DF_ConstraintId] DEFAULT (newid()),
	[SchemaId]            UniqueIdentifier Not Null,
	[ConstraintName]      SysName Not Null,
	[TableId]             UniqueIdentifier Not Null,
	[ConstraintType]      [AppGeneral].[typeObjectType] Null, -- Known types: FOREIGN KEY, UNIQUE, PRIMARY KEY
	-- TODO: Add System Version later once the schema is locked down. Not needed for Db Schema?
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Constraint_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Constraint_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Constraint] PRIMARY KEY CLUSTERED ([ConstraintId] ASC),
	CONSTRAINT [FK_ConstraintSchema] FOREIGN KEY ([SchemaId]) REFERENCES [AppCatalog].[Schema] ([SchemaId]),
	CONSTRAINT [FK_ConstraintTable] FOREIGN KEY ([TableId]) REFERENCES [AppCatalog].[Table] ([TableId]),
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsCatalog].[Constraint]))
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Constraint]
    ON [AppCatalog].[Constraint]([ConstraintName], [SchemaId]);
GO

