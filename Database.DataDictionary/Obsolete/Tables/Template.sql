CREATE TABLE [Obsolete].[Template]
(
	[TemplateId]            UniqueIdentifier Not Null CONSTRAINT [DF_TemplateId] DEFAULT (newid()),
	[TemplateTitle]			[AppGeneral].[uddtTitle] Not Null,
	[TemplateDescription]	[AppGeneral].[uddtDescription] Null,
	-- Transform Settings, refers to the XSLT and the document produced.
	[BreakOnScope]			[AppGeneral].[uddtScopeName] NULL,  -- The Scope to have a document break on. Null = no break.
	[TransformScript]		XML Null , -- XSLT Transform Script. Not sure how to specify this is xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	-- Document Settings, refers to the XML documents and scripting files that is built.
	[RootFolder]			[AppGeneral].[uddtFileRoot] Null, -- Name of the Special Directory used as the Root
	[ScriptAs]				NVarChar(10) Not Null, -- The type of Document the script produces. Text or XML. Obsolete, The Transform results determines type
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Template_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Template_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_Template] PRIMARY KEY CLUSTERED ([TemplateId] ASC),
	CONSTRAINT [CK_TemplateScriptAs] CHECK ([ScriptAs]='XML' OR [ScriptAs]='Text'),
)
