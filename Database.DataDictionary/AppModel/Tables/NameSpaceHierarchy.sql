CREATE TABLE [AppModel].[NameSpaceHierarchy]
(
	-- A Parent/Child Hierarchy is used instead of a Hierarchy Id.
	-- This provided better enforcement of parent/child relationships.
	-- Lookup by Name is also faster with this structure then the Hierarchy Id option.
	--
	-- Because of the possible full length of a NameSpace, the database cannot index or work with the value.
	-- The NameSpace is broken into parts called "MemberName".
	-- The length is long enough to address most scenario's.
	--
	-- In SQL Db examples are: Database Name, Schema Name, Object Name or Column Name.
	-- The maximum length is 128 characters (256 bytes)
	-- In .Net examples are: single part of a NameSpace, Class Name, Property Name, Field Name or Method Name.
	-- The maximum length is not defined but is restricted to 1023 (2046 bytes) based on VB.Net definitions.
	-- Fields of this type exceed the limits of a SQL Index (900 bytes for Clustered, 1700 bytes for non-clustered).
	[NameSpaceId]           UniqueIdentifier NOT NULL CONSTRAINT [DF_NameSpaceId] DEFAULT (newid()),
	[ModelId]               UniqueIdentifier NULL,
	[ParentNameSpaceId]     UniqueIdentifier NULL,
	[MemberName]            [App_DataDictionary].[typeNameSpaceMember] NOT NULL, -- 1600 bytes, NVarChar(800)
	-- Temporal History Support
	[SysStart]              DateTime2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_NameSpace_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]                DateTime2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_NameSpace_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_NameSpace] PRIMARY KEY CLUSTERED ([NameSpaceId] ASC),
	CONSTRAINT [FK_NameSpaceModel] FOREIGN KEY ([ModelId]) REFERENCES [AppModel].[Model] ([ModelId]),
	CONSTRAINT [FK_NameSpaceParent] FOREIGN KEY ([ParentNameSpaceId]) REFERENCES [AppModel].[NameSpaceHierarchy] ([NameSpaceId]),
	CONSTRAINT [UK_NameSpaceModel] UNIQUE ([ModelId] ASC, [NameSpaceId] ASC) -- Where ([ModelId] is not null)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[NameSpaceHierarchy]))
GO
CREATE UNIQUE INDEX [AK_NameSpace]
    ON [AppModel].[NameSpaceHierarchy]([ParentNameSpaceId] ASC, [ModelId] ASC, [MemberName] ASC)
GO
