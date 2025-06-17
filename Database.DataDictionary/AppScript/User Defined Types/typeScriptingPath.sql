CREATE TYPE [AppScript].[typeScriptingPath] AS TABLE
(
	[TemplateId]        UniqueIdentifier NULL,
	[PathName]			[App_DataDictionary].[typeNameSpacePath] NULL,
	[PathScope]         [AppModel].[typeScopeName] NULL
)
