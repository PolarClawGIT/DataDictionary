CREATE TABLE [App_DataDictionary].[ScriptingPath]
(
	[TemplateId]        UniqueIdentifier NOT NULL,
	[NameSpaceId]       UniqueIdentifier NOT NULL,
	[PathScope]         [App_DataDictionary].[typeScopeName] NOT NULL,  -- The Scope for the Application to look for the Alias within
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ScriptingPath_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_SScriptingPath_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ScriptingPath_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_ScriptingPath] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [NameSpaceId] ASC),
	CONSTRAINT [FK_ScriptingPathTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [App_DataDictionary].[ScriptingTemplate] ([TemplateId]),
	CONSTRAINT [FK_ScriptingPathNameSpace] FOREIGN KEY ([NameSpaceId]) REFERENCES [App_DataDictionary].[ModelNameSpace] ([NameSpaceId]),

)
