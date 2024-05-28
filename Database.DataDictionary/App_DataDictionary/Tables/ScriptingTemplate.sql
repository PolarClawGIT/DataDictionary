CREATE TABLE [App_DataDictionary].[ScriptingTemplate]
(
	[TemplateId]            UniqueIdentifier NOT NULL CONSTRAINT [DF_ScriptingTemplateId] DEFAULT (newid()),
	[TemplateTitle]			[App_DataDictionary].[typeTitle] Not Null,
	[TemplateDescription]	[App_DataDictionary].[typeDescription] Null,
	-- Document Settings, refers to the XML document that is built.
	[DocumentScope]			[App_DataDictionary].[typeScopeName] NULL,  -- The Scope to have a document break on. Null = no break.
	[DocumentPrefix]		NVarChar(50) Null,
	[DocumentSufix]			NVarChar(50) Null,
	[DocumentExtension]		NVarChar(10) Null,
	-- Transform Settings, refers to the XSLT and the document produced.
	[TransformScript]		XML Null , -- XSLT Transform Script. Not sure how to specify this is xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	[TransformAsText]       Bit Null, -- The result is expected to be plain text. true = Text, false = XML.
	[TransformeExtension]	NVarChar(10) Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ScriptingTemplate_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ScriptingTemplate_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ScriptingTemplate_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_ScriptingTemplate] PRIMARY KEY CLUSTERED ([TemplateId] ASC),
	
)
