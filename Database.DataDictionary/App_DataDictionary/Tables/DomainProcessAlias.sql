CREATE TABLE [App_DataDictionary].[DomainProcessAlias]
(
	-- Alias are used to keep track of what Entities goes with what Database object.
	-- Specifically, tables, procedures, functions and domains appear here.
	-- A hard FK reference is not maintained as the associated Catalog may not be attached to the Model.
	-- To be implemented later.
	[ProcessId] UniqueIdentifier Not Null,
	[ProcessAliasId] Int Not Null, -- Surrogate Key element, maintained by Database.
	-- This is based on https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16
	-- Not-normalized to better translate to SQL call.
	[DatabaseName] SysName Null, -- Name of the Catalog/Database
	[SchemaName] SysName Not Null, -- Name of Level0. Examples is Schema, Synonym, and User Name. For attributes, this is always a SchemaName.
	[ObjectName] SysName Not Null, -- Name of Level1. Examples are Table, View, Procedure and Function Name
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainProcessAlias_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainProcessAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainProcessAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainProcessAlias] PRIMARY KEY CLUSTERED ([ProcessId] ASC, [ProcessAliasId] ASC),
)
GO
