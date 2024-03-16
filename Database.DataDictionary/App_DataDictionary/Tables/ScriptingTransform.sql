CREATE TABLE [App_DataDictionary].[ScriptingTransform]
(
    -- This represents the data for the Scripting Engine. 
	-- The Engine uses XML Transform (XSLT) to do the work.
	-- The results is Text or XML.
	[TransformId]             UniqueIdentifier NOT NULL CONSTRAINT [DF_ApplicationTransformId] DEFAULT (newid()),
	[TransformTitle]          [App_DataDictionary].[typeTitle] Not Null,
	[TransformDescription]    [App_DataDictionary].[typeDescription] Null,
	[AsText]                  Bit Null, -- The result is expected to be plain text. true = Text, false = XML.
	--	[AsXml]         As (convert(bit, case when [AsText]=(0) then (1) when [AsText]=(1) then (0) end)),
	[TransformScript]         XML Null , -- XSLT Transform Script. Not sure how to specify this is xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ScriptingTransform_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ScriptingTransform_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ScriptingTransform_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_ScriptingTransform] PRIMARY KEY CLUSTERED ([TransformId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_ScriptingTransform]
    ON [App_DataDictionary].[ScriptingTransform]([TransformTitle]);
GO
