CREATE TABLE [App_DataDictionary].[ScriptingSelection]
(
	-- Selection represent a set of items that are to be scripted.
	[SelectionId]          UniqueIdentifier NOT NULL CONSTRAINT [DF_ScriptingSelectionId] DEFAULT (newid()),
	[SelectionTitle]       [App_DataDictionary].[typeTitle] Not Null,
	[SelectionDescription] [App_DataDictionary].[typeDescription] Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ScriptingSelection_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ScriptingSelection_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ScriptingSelection_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_ScriptingSelection] PRIMARY KEY CLUSTERED ([SelectionId] ASC),
)
GO
CREATE UNIQUE INDEX [AK_ScriptingSelection]
    ON [App_DataDictionary].[ScriptingSelection]([SelectionTitle] ASC)
GO
