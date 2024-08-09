CREATE TABLE [App_DataDictionary].[DatabaseReference]
(
	-- [INFORMATION_SCHEMA] does not contain a list of Columns referenced by Procedures or Functions.
	-- The best source of this is sys.dm_sql_referenced_entities.
	-- This is function is not 100% accurate.
	-- Simple routines return good information but the more complex the routine the less reliable the values are.
	-- The system function will also return exceptions for routines not specifically called for.
	[ReferenceId]             UniqueIdentifier Not Null CONSTRAINT [DF_DatabaseReferenceId] DEFAULT (newid()),
	--[CatalogId]               UniqueIdentifier Not Null,
	[ObjectId]                UniqueIdentifier Not Null, -- Could be Table, View, Procedure, Function or something else
	--[ReferencingDatabaseName] SysName Not Null,
	--[ReferencingSchemaName]   SysName Not Null,
	--[ReferencingObjectName]   SysName Not Null,
	[ObjectType]		      [App_DataDictionary].[typeObjectSubType] Not Null, -- USER_TABLE, VIEW, FUNCTION, PROCEDURE, ...
	-- Source has Referenced objects as null-able and may not reflect the current state of the database.
	--[ReferencedServerName]    SysName Null,
	[ReferencedDatabaseName]  SysName Null,
	[ReferencedSchemaName]    SysName Null,
	[ReferencedObjectName]    SysName Null,
	[ReferencedColumnName]    SysName Null,
	[ReferencedType]          [App_DataDictionary].[typeObjectSubType] Null,
	[IsCallerDependent]       Bit Null,
	[IsAmbiguous]             Bit Null,
	[IsSelected]              Bit Null,
	[IsUpdated]               Bit Null,
	[IsSelectAll]             Bit Null,
	[IsAllColumnsFound]       Bit Null,
	[IsInsertAll]             Bit Null,
	[IsIncomplete]            Bit NULL,
	-- TODO: Add System Version later once the schema is locked down. Not needed for Db Schema?
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DatabaseReference_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DatabaseReference_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DatabaseReference_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DatabaseReference] PRIMARY KEY CLUSTERED ([ReferenceId] ASC),
)
GO
CREATE UNIQUE INDEX [AK_DatabaseReference]
    ON [App_DataDictionary].[DatabaseReference]([ObjectId] ASC, [ReferencedDatabaseName] ASC, [ReferencedSchemaName] ASC, [ReferencedObjectName] ASC, [ReferencedColumnName] ASC)
GO