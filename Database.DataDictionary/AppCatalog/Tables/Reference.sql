CREATE TABLE [AppCatalog].[Reference]
(
	-- [INFORMATION_SCHEMA] does not contain a list of Columns referenced by Procedures or Functions.
	-- The best source of this is sys.dm_sql_referenced_entities.
	-- This is function is not 100% accurate.
	-- Simple routines return good information but the more complex the routine the less reliable the values are.
	-- The system function will also return exceptions for routines not specifically called for.
	-- Referencing objects are not guaranteed to exist in the Catalog.
	[ReferenceId]             UniqueIdentifier Not Null CONSTRAINT [DF_ReferenceId] DEFAULT (newid()),
	[CatalogId]               UniqueIdentifier Not Null,
	--[ObjectId]                UniqueIdentifier Not Null, -- Could be Table, View, Procedure, Function or something else. No guarantee this exists.
	--[DatabaseName] SysName Not Null,
	[SchemaName]              SysName Not Null,
	[ObjectName]              SysName Not Null, -- Name of Table, View, Procedure, Function or other database object
	[ObjectType]		      [App_DataDictionary].[typeObjectType] Not Null, -- USER_TABLE, VIEW, FUNCTION, PROCEDURE, ...
	-- Source has Referenced objects as null-able and may not reflect the current state of the database.
	--[ReferencedServerName]    SysName Null,
	[ReferencedDatabaseName]  SysName Null,
	[ReferencedSchemaName]    SysName Null,
	[ReferencedObjectName]    SysName Null,
	[ReferencedColumnName]    SysName Null,
	[ReferencedType]          [App_DataDictionary].[typeObjectType] Null,
	[IsCallerDependent]       Bit Null,
	[IsAmbiguous]             Bit Null,
	[IsSelected]              Bit Null,
	[IsModified]              Bit Null,
	[IsSelectAll]             Bit Null,
	[IsAllColumnsFound]       Bit Null,
	[IsInsertAll]             Bit Null,
	[IsIncomplete]            Bit NULL,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Reference_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Reference_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Reference] PRIMARY KEY CLUSTERED ([ReferenceId] ASC),
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsCatalog].[Reference]))
GO
