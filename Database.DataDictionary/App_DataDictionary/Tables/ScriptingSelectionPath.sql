CREATE TABLE [App_DataDictionary].[ScriptingSelectionPath]
(
	-- This describes the path to selected item.
	-- Functionally, it acts like the XPath for the scripting engine to find the item being referenced.
	[SelectionId]       UniqueIdentifier NOT NULL,
	[NameSpaceId]       UniqueIdentifier NOT NULL,
	[ScopeName]         [App_DataDictionary].[typeScopeName] NOT NULL,  -- The Scope for the Application to look for the Alias within
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ScriptingSelectionPath_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ScriptingSelectionPath_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ScriptingSelectionPathSysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_ScriptingSelectionPath] PRIMARY KEY CLUSTERED ([SelectionId] ASC, [NameSpaceId] ASC),
	CONSTRAINT [FK_ScriptingSelection] FOREIGN KEY ([SelectionId]) REFERENCES [App_DataDictionary].[ScriptingSelection] ([SelectionId]),
	CONSTRAINT [FK_ScriptingSelectionNameSpace] FOREIGN KEY ([NameSpaceId]) REFERENCES [App_DataDictionary].[ModelNameSpace] ([NameSpaceId]),
)
GO
