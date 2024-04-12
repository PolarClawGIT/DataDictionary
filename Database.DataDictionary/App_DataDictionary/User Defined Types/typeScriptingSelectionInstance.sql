CREATE TYPE [App_DataDictionary].[typeScriptingSelectionInstance] AS TABLE
(
	[InstanceId]           UniqueIdentifier NULL,
	[SelectionId]          UniqueIdentifier NULL,
	[InstanceName]         [App_DataDictionary].[typeNameSpacePath] NULL,
	[ScopeName]            [App_DataDictionary].[typeScopeName] Null
)
