CREATE TYPE [AppScript].[ttScriptingPath] AS TABLE
(
	[TemplateId]        UniqueIdentifier NULL,
	[PathName]			[AppGeneral].[dtNameSpacePath] NULL,
	[PathScope]         [AppModel].[typeScopeName] NULL
)
