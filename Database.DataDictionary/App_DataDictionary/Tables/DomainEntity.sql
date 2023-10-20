CREATE TABLE [App_DataDictionary].[DomainEntity]
(
	-- In ER Diagrams, an Entity is a Table, View, or other supported object type.
	-- For this tool Entities are a catch-all item that are not a Column or Parameter.
	-- This includes Tables, Views, Procedures Functions and User Defined Data Types.
	-- To be implemented later.
	[EntityId] UniqueIdentifier Not Null CONSTRAINT [DF_DomainEntityId] DEFAULT (newid()),
	[EntityTitle] [App_DataDictionary].[typeTitle] Not Null,
	[EntityDescription] [App_DataDictionary].[typeDescription] Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainEntity_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainEntity_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainEntity_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainEntity] PRIMARY KEY CLUSTERED ([EntityId] ASC),
)
GO

