CREATE TYPE [AppScript].[udttScriptingTemplate] AS TABLE
(
	[TemplateId]            UniqueIdentifier NULL,
	[TemplateTitle]			[AppGeneral].[uddtTitle] Not Null,
	[TemplateDescription]	[AppGeneral].[uddtDescription] Null,
	[BreakOnScope]			[AppGeneral].[uddtScopeName] NULL,
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
