CREATE TABLE [App_DataDictionary].[ModelScripting]
(
	[ModelId]              UniqueIdentifier NOT NULL,
	[TemplateId]           UniqueIdentifier NOT NULL,
	--[SelectionId]          UniqueIdentifier NOT NULL, -- How to select the objects (XPath)
	--[SchemaId]             UniqueIdentifier     NULL, -- How should the be defined (XSD)
	--[TransformId]          UniqueIdentifier     NULL, -- How to transform the data (XSLT)
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_ModelScripting_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelScripting_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelScripting_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ModelScripting] PRIMARY KEY CLUSTERED ([ModelId] ASC, [TemplateId] ASC),
	CONSTRAINT [FK_ModelScriptingModel] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
	--CONSTRAINT [FK_ModelScriptingSchema] FOREIGN KEY ([SchemaId]) REFERENCES [App_DataDictionary].[ScriptingSchema] ([SchemaId]),
	--CONSTRAINT [FK_ModelScriptingTransform] FOREIGN KEY ([TransformId]) REFERENCES [App_DataDictionary].[ScriptingTransform] ([TransformId]),
	--CONSTRAINT [FK_ModelScriptingSelection] FOREIGN KEY ([SelectionId]) REFERENCES [App_DataDictionary].[ScriptingSelection] ([SelectionId]),
	CONSTRAINT [FK_ModelScriptingTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [App_DataDictionary].[ScriptingTemplate] ([TemplateId]),
)
