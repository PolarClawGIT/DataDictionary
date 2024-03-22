CREATE TABLE [App_DataDictionary].[ModelScripting]
(
	-- The combination of Scripting objects used within the Model.
	[ModelId]              UniqueIdentifier NOT NULL,
	[ScriptingTitle]       [App_DataDictionary].[typeTitle] NOT NULL,
	[ScriptingDescription] [App_DataDictionary].[typeDescription] NULL,
    [SchemaId]             UniqueIdentifier NULL, -- How should the be defined (XSD)
	[SelectionId]          UniqueIdentifier NULL, -- What Items to be returned (XPath)
	[TransformId]          UniqueIdentifier NULL, -- How to transform the data (XSLT)
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName NOT NULL CONSTRAINT [DF_ModelScript_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelScript_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelScript_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ModelScripting] PRIMARY KEY CLUSTERED ([ModelId] ASC, [ScriptingTitle]),
	CONSTRAINT [FK_ModelScriptingSelection] FOREIGN KEY ([SelectionId]) REFERENCES [App_DataDictionary].[ScriptingSelection] ([SelectionId]),
	CONSTRAINT [FK_ModelScriptingSchema] FOREIGN KEY ([SchemaId]) REFERENCES [App_DataDictionary].[ScriptingSchema] ([SchemaId]),
	CONSTRAINT [FK_ModelScriptingTransform] FOREIGN KEY ([TransformId]) REFERENCES [App_DataDictionary].[ScriptingTransform] ([TransformId]),
)
