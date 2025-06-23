CREATE TYPE [AppScript].[udttScriptingPath] AS TABLE
(
	[TemplateId]        UniqueIdentifier NULL,
	[NameSpace]			[AppGeneral].[uddtNameSpacePath] NULL,
	[PathScope]         [AppGeneral].[uddtScopeName] NULL
)
