CREATE TABLE [App_DataDictionary].[ScriptingSelectionInstance]
(
	-- This describes the selected item.
	-- Functionally, it acts like the XPath for the scripting engine to find the item being referenced.
	-- TODO: "Instance" is not a good name but it is the best I could come up with.
	--       Instance (over "Item", "Element") is avoid ambiguity in the Data layer.
	[InstanceId]           UniqueIdentifier NOT NULL CONSTRAINT [DF_SelectionInstanceId] DEFAULT (newid()),
	[ParentInstanceId]     UniqueIdentifier NULL,
	[SelectionId]          UniqueIdentifier NOT NULL,
	[ScopeName]            [App_DataDictionary].[typeScopeName] NOT Null,  -- The Scope for the Application to look for the item within
	[InstanceMember]       [App_DataDictionary].[typeNameSpaceMember] NOT NULL,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ScriptingSelectionItem_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ScriptingSelectionItem_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ScriptingSelectionItem_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_ScriptingSelectionInstance] PRIMARY KEY CLUSTERED ([InstanceId] ASC),
	CONSTRAINT [FK_ScriptingSelection] FOREIGN KEY ([SelectionId]) REFERENCES [App_DataDictionary].[ScriptingSelection] ([SelectionId]),
	CONSTRAINT [FK_ScriptingSelectionInstanceParent] FOREIGN KEY ([ParentInstanceId]) REFERENCES [App_DataDictionary].[ScriptingSelectionInstance] ([InstanceId]),
)
GO
