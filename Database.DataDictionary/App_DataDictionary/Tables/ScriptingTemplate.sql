﻿CREATE TABLE [App_DataDictionary].[ScriptingTemplate]
(
	[TemplateId]            UniqueIdentifier NOT NULL CONSTRAINT [DF_ScriptingTemplateId] DEFAULT (newid()),
	[TemplateTitle]			[App_DataDictionary].[typeTitle] Not Null,
	[TemplateDescription]	[App_DataDictionary].[typeDescription] Null,
	-- Transform Settings, refers to the XSLT and the document produced.
	[BreakOnScope]			[App_DataDictionary].[typeScopeName] NULL,  -- The Scope to have a document break on. Null = no break.
	[TransformScript]		XML Null , -- XSLT Transform Script. Not sure how to specify this is xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	[TransformAsText]       Bit Null, -- The result is expected to be plain text. true = Text, false = XML.
	-- Document Settings, refers to the XML documents and scripting files that is built.
	-- Concept: The database would not store the files.
	--          Instead, they are generated and written to the Visual Studio solution directory.
	--          The Solution directory is typically in the Source control folder. example: C:\Users\username\source\repos
	--          The other place it can commonly fall is in My Documents directory.
	--          External tools then can better handle the display of the files.
	--          For this reason, the Directory is split into components.
	--          Root: is what special directory the solution is within. \\Source\repo, \\Source\Workspaces or \\Documents
	--          Solution: relative path from the root to the Visual Studio Solution directory
	--          Document: relative path from the solution to the directory to store the XML source documents
	--          Script: relative path from the solution to the directory to store the Scripting documents
	--          Prefix/Suffix: Added to the Element Name when BreakOnScope is used or concatenated.  Possible duplicate file names?
	--          Extension: The extension to be used. The Default is either .XML or .TXT depending on TransformOnText.
	--          A simple viewer would exist within the application to preview the data with "Save As" feature.
	--          The application can search for files based on the Template settings and display them in the navigation tree.
	[RootDirectory]         NVarChar(100) Null,
	[SolutionDirectory]		NVarChar(250) Null,
	[DocumentDirectory]		NVarChar(250) Null,
	[DocumentPrefix]		NVarChar(50) Null,
	[DocumentSuffix]		NVarChar(50) Null,
	[DocumentExtension]		NVarChar(10) Null,
	[ScriptDirectory]		NVarChar(250) Null,
	[ScriptPrefix]			NVarChar(50) Null,
	[ScriptSuffix]			NVarChar(50) Null,
	[ScriptExtension]		NVarChar(10) Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ScriptingTemplate_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ScriptingTemplate_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ScriptingTemplate_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_ScriptingTemplate] PRIMARY KEY CLUSTERED ([TemplateId] ASC),
	
)
