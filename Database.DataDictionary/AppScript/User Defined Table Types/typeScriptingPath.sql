CREATE TYPE [AppScript].[typeScriptingPath] AS TABLE
(
	[TemplateId]        UniqueIdentifier NULL,
	[PathName]			[AppGeneral].[typeNameSpacePath] NULL,
	[PathScope]         [AppModel].[typeScopeName] NULL
)
