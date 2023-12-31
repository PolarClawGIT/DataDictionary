﻿CREATE TABLE [App_DataDictionary].[DatabaseConstraint]
(
	-- [INFORMATION_SCHEMA] does not contain Indexes.
	-- For the purpose of this applications, non-key indexes are not very interesting.
	-- The additional values can be gotten from Sys.Indexes.
	[ConstraintId]        UniqueIdentifier Not Null CONSTRAINT [DF_DatabaseConstraintId] DEFAULT (newid()),
	[SchemaId]            UniqueIdentifier Not Null,
	[ConstraintName]      SysName Not Null,
	[ScopeId]             Int Not Null,
	[ParentTableId]       UniqueIdentifier Not Null,
	[ConstraintType]      NVarChar(60) Null, -- Known types: FOREIGN KEY, UNIQUE, PRIMARY KEY
	-- TODO: Add System Version later once the schema is locked down. Not needed for Db Schema?
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DatabaseConstraint_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DatabaseConstraint_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DatabaseConstraint_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DatabaseConstraint] PRIMARY KEY CLUSTERED ([ConstraintId] ASC),
	CONSTRAINT [FK_DatabaseConstraintSchema] FOREIGN KEY ([SchemaId]) REFERENCES [App_DataDictionary].[DatabaseSchema] ([SchemaId]),
	CONSTRAINT [FK_DatabaseConstraintTable] FOREIGN KEY ([ParentTableId]) REFERENCES [App_DataDictionary].[DatabaseTable] ([TableId]),
	CONSTRAINT [FK_DatabaseConstraintScope] FOREIGN KEY ([ScopeId]) REFERENCES [App_DataDictionary].[ApplicationScope] ([ScopeId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DatabaseConstraint]
    ON [App_DataDictionary].[DatabaseConstraint]([ConstraintName], [SchemaId]);
GO

