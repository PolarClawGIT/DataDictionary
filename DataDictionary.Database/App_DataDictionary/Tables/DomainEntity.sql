﻿CREATE TABLE [App_DataDictionary].[DomainEntity]
(
	-- In ER Diagrams, an Entity is a Table, View, or other supported object type.
	-- For this tool Entities are a catch-all item that are not a Column or Parameter.
	-- This includes Tables, Views, Procedures Functions and User Defined Data Types.
	-- To be implemented later.
	[EntityId] UniqueIdentifier Not Null CONSTRAINT [DF_DomainEntityEntityId] DEFAULT (newid()),
	[EntityParentId] UniqueIdentifier Null,
	[EntityTitle] NVarChar(100) Not Null,
	[EntityDescription] NVarChar(Max) Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainEntity_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainEntity_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainEntity_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainEntity] PRIMARY KEY CLUSTERED ([EntityId] ASC),
	CONSTRAINT [FK_DomainEntity_Parent] FOREIGN KEY ([EntityParentId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DomainEntity]
    ON [App_DataDictionary].[DomainEntity]([EntityTitle] ASC, [EntityParentId] ASC);
GO
