CREATE TYPE [App_DataDictionary].[typeScriptingTemplate] AS TABLE
(
	[TemplateId]            UniqueIdentifier NULL,
	[TemplateTitle]			[App_DataDictionary].[typeTitle] Not Null,
	[TemplateDescription]	[App_DataDictionary].[typeDescription] Null,
	[BreakOnScope]			[App_DataDictionary].[typeScopeName] NULL,
	[TransformScript]		NVarChar(Max) Null,
	[TransformAsText]       Bit Null,
	[RootDirectory]         NVarChar(100) Null,
	[SolutionDirectory]		NVarChar(250) Null,
	[DocumentDirectory]		NVarChar(250) Null,
	[DocumentPrefix]		NVarChar(50) Null,
	[DocumentSuffix]		NVarChar(50) Null,
	[DocumentExtension]		NVarChar(10) Null,
	[ScriptDirectory]		NVarChar(250) Null,
	[ScriptPrefix]			NVarChar(50) Null,
	[ScriptSuffix]			NVarChar(50) Null,
	[ScriptExtension]		NVarChar(10) Null
)
