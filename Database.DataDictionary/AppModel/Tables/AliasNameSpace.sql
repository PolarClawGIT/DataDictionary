CREATE TABLE [AppModel].[AliasNameSpace]
(
	-- Alias are a shared resource used as a data type to store NameSpace like values.
	-- NameSpaces can exceed the limits of indexes within SQL.
	-- To handle this, NameSpaces are broken into parts called Members.
	-- Members then can be re-combined to create a NameSpace.
	-- For this purpose, SQL Qualified Names are treated as NameSpaces.
	-- No Member can exceed 1700 - 36 bytes or NVarChar(800) for indexing purposes.
	--
	-- Because of the indirect relationships, it is possible for orphaned values to exist.
	-- An orphan is a Alias with no reference to it.
	-- This is difficult to track and cannot be historically guaranteed.
	[AliasId]           UniqueIdentifier Not Null CONSTRAINT [DF_AliasId] DEFAULT (newid()),
	[AliasMember]       [AppGeneral].[uddtMember] Not Null, -- Member Name of the alias. Combined to create a NameSpace.
	[ParentAliasId]     UniqueIdentifier NULL,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Alias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Alias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Alias] PRIMARY KEY CLUSTERED ([AliasId] ASC),
	CONSTRAINT [FK_Alias_Parent] FOREIGN KEY ([ParentAliasId]) REFERENCES [AppModel].[AliasNameSpace] ([AliasId]),
	CONSTRAINT [AK_Alias_MemberName] UNIQUE ([ParentAliasId] ASC, [AliasMember] ASC)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[AliasNameSpace]))
GO
