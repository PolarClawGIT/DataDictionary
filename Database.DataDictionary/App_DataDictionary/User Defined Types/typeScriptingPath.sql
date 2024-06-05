CREATE TYPE [App_DataDictionary].[typeScriptingPath] AS TABLE
(
	[TemplateId]        UniqueIdentifier NULL,
	[PathName]			[App_DataDictionary].[typeNameSpacePath] NULL,
	[PathScope]         [App_DataDictionary].[typeScopeName] NULL
)
