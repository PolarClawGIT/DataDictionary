CREATE TYPE [App_DataDictionary].[typeScriptingSelectionPath] AS TABLE
(
	[SelectionId]          UniqueIdentifier NULL,
	[ScopeName]            [App_DataDictionary].[typeScopeName] Null,
	[SelectionPath]        [App_DataDictionary].[typeNameSpacePath] NULL -- The XPath equivalent
)
