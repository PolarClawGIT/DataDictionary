CREATE TABLE [AppScript].[ScriptingNameSpace]
(
	[NameSpaceId]			UniqueIdentifier Not Null CONSTRAINT [DF_NameSpaceId] DEFAULT (newid()),
	[NameSpaceMember]		[AppGeneral].[typeNameSpaceMember] Not Null, -- The Item Name to be scripted. Combined to create a NameSpace.
	[ParentNameSpaceId]		UniqueIdentifier Null,
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ItemHierarchy_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ItemHierarchy_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_ItemNameSpace] PRIMARY KEY CLUSTERED ([NameSpaceId] ASC),
	CONSTRAINT [FK_ItemNameSpace_Parent] FOREIGN KEY ([ParentNameSpaceId]) REFERENCES [AppScript].[ScriptingNameSpace] ([NameSpaceId]),

)
