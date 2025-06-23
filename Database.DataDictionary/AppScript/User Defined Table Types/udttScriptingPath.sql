CREATE TYPE [AppScript].[udttScriptingPath] AS TABLE
(
	[TemplateId]        UniqueIdentifier NULL,
	[PathName]			[AppGeneral].[uddtNameSpacePath] NULL,
	[PathScope]         [AppGeneral].[uddtScopeName] NULL
)
