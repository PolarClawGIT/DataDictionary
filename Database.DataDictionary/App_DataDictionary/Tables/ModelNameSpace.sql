CREATE TABLE [App_DataDictionary].[ModelNameSpace]
(
	-- NameSpace does not represent a single entity. Rather an attribute of an entity.	
	-- Because of the possible length of the value, the database cannot index or work with the value as is.
	-- In SQL Db examples are: Database Name, Schema Name, Object Name or Column Name.
	-- The maximum length is 128 characters (256 bytes)
	-- In .Net examples are: single part of a NameSpace, Class Name, Property Name, Field Name or Method Name.
	-- The maximum length is not defined but is restricted to 1023 (2046 bytes) based on VB.Net definitions.
	-- Fields of this type exceed the limits of a SQL Index (900 bytes for Clustered, 1700 bytes for non-clustered).
	-- This structure allows for much longer NameSpaces then normally could be handled.
	[NameSpaceId]           UniqueIdentifier NOT NULL CONSTRAINT [DF_ModelNameSpaceId] DEFAULT (newid()),
	[ModelId]               UniqueIdentifier NOT NULL,
	[ParentNameSpaceId]     UniqueIdentifier NULL,
	[MemberName]            [App_DataDictionary].[typeNameSpaceMember] NOT NULL, -- 1600 bytes, NVarChar(800)
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_ModelNameSpace_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelNameSpace_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelNameSpace_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_ModelNameSpace] PRIMARY KEY CLUSTERED ([NameSpaceId] ASC),
--	CONSTRAINT [FK_ModelNameSpaceParent] FOREIGN KEY ([ParentNameSpaceId]) REFERENCES [App_DataDictionary].[ModelNameSpace] ([NameSpaceId]),
	CONSTRAINT [FK_ModelNameSpaceModel] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
	CONSTRAINT [UK_ModelNameSpaceModel] UNIQUE ([ModelId] ASC, [NameSpaceId] ASC),
	CONSTRAINT [FK_ModelNameSpaceParent] FOREIGN KEY ([ModelId], [ParentNameSpaceId]) REFERENCES [App_DataDictionary].[ModelNameSpace] ([ModelId], [NameSpaceId]),
)
GO
CREATE UNIQUE INDEX [AK_ModelNameSpace]
    ON [App_DataDictionary].[ModelNameSpace]([ModelId], [ParentNameSpaceId] ASC, [MemberName] ASC)
GO
