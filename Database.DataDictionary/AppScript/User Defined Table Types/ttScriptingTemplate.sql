CREATE TYPE [AppScript].[ttScriptingTemplate] AS TABLE
(
	[TemplateId]            UniqueIdentifier NULL,
	[TemplateTitle]			[AppGeneral].[dtTitle] Not Null,
	[TemplateDescription]	[AppGeneral].[dtDescription] Null,
	[BreakOnScope]			[AppModel].[typeScopeName] NULL,
	[TransformScript]		NVarChar(Max) Null,
	[RootDirectory]         NVarChar(100) Null,
	[DocumentDirectory]		NVarChar(250) Null,
	[DocumentPrefix]		NVarChar(50) Null,
	[DocumentSuffix]		NVarChar(50) Null,
	[DocumentExtension]		NVarChar(10) Null,
	[ScriptAs]              NVarChar(10) Null,
	[ScriptDirectory]		NVarChar(250) Null,
	[ScriptPrefix]			NVarChar(50) Null,
	[ScriptSuffix]			NVarChar(50) Null,
	[ScriptExtension]		NVarChar(10) Null
)
