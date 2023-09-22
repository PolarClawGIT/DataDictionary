CREATE TABLE [App_DataDictionary].[DomainEntityAlias]
( 
	-- Alias are used to keep track of what Entities goes with what Database object.
	-- Specifically, tables, procedures, functions and domains appear here.
	-- A hard FK reference is not maintained as the associated Catalog may not be attached to the Model.
	-- To be implemented later.
	[EntityId] UniqueIdentifier Not Null,
	[EntityAliasId] Int Not Null, -- Surrogate Key element, maintained by Database.
	[ModelId] UniqueIdentifier NOT NULL, -- Needed to enforce Unique Key. Alias must be unique to a Model.
	-- This is based on https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16
	-- Not-normalized to better translate to SQL call.
	[CatalogName] SysName Null, -- Name of the Database
	[SchemaName] SysName Not Null, -- Name of Level0. Examples is Schema, Synonym, and User Name. For attributes, this is always a SchemaName.
	[ObjectName] SysName Not Null, -- Name of Level1. Examples are Table, View, Procedure and Function Name
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainEntityAlias_ModfiedBy] DEFAULT (ORIGINAL_LOGIN()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainEntityAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainEntityAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainEntityAlias] PRIMARY KEY CLUSTERED ([EntityId] ASC, [EntityAliasId] ASC),
	--CONSTRAINT [FK_DomainEntity] FOREIGN KEY ([EntityId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
	CONSTRAINT [FK_DomainEntityModel] FOREIGN KEY ([ModelId], [EntityId]) REFERENCES [App_DataDictionary].[ModelEntity] ([ModelId], [EntityId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DomainEntityAliasModel]
    ON [App_DataDictionary].[DomainEntityAlias]([ModelId] Asc, [CatalogName] Asc, [SchemaName] ASC, [ObjectName] ASC);
GO