CREATE TYPE [AppScript].[ttScriptingPath] AS TABLE
(
	[TemplateId]        UniqueIdentifier NULL,
	[PathName]			[AppGeneral].[typeNameSpacePath] NULL,
	[PathScope]         [AppModel].[typeScopeName] NULL
)
