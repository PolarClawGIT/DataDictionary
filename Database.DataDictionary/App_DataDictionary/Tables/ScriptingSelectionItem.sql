CREATE TABLE [App_DataDictionary].[ScriptingSelectionItem]
(
	-- This describes the selected item.
	-- Functionally, it acts like the XPath for the scripting engine to find the item being referenced.
	-- This is constructed in a similar manner as NameSpaces and Alias
	[ItemId]               UniqueIdentifier NOT NULL CONSTRAINT [DF_ScriptingSelectionItemId] DEFAULT (newsequentialid()),
	[SelectionId]          UniqueIdentifier NOT NULL,
	[ScopeId]              Int Not Null, -- Scope to match to. In effect what is the Table/Sub-Type look for.
	[ItemParentId]         UniqueIdentifier Null, -- Used to Build a Name Space of the Selected item.
	[ItemName]             [App_DataDictionary].[typeNameSpaceMember] Not Null, -- Used to Build a Name Space of the Selected item
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ScriptingSelectionItem_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ScriptingSelectionItem_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ScriptingSelectionItem_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_ScriptingSelectionItem] PRIMARY KEY CLUSTERED ([ItemId] ASC),
	CONSTRAINT [FK_ScriptingSelectionItemSelection] FOREIGN KEY ([SelectionId]) REFERENCES [App_DataDictionary].[ScriptingSelection] ([SelectionId]),
	CONSTRAINT [FK_ScriptingSelectionItemParent] FOREIGN KEY ([ItemParentId]) REFERENCES [App_DataDictionary].[ScriptingSelectionItem] ([ItemId]),
	CONSTRAINT [FK_ScriptingSelectionItemScope] FOREIGN KEY ([ScopeId]) REFERENCES [App_DataDictionary].[ApplicationScope] ([ScopeId]),
)
GO
CREATE UNIQUE INDEX [AK_ScriptingSelectionItem]
    ON [App_DataDictionary].[ScriptingSelectionItem]([SelectionId] ASC, [ItemParentId] ASC, [ItemName] ASC)
GO