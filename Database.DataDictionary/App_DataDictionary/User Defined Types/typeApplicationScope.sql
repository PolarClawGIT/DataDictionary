CREATE TYPE [App_DataDictionary].[typeApplicationScope] AS TABLE
(
    -- TIP: This matches the C# DataTable structure
	[ScopeId]          Int Null,
	[ScopeName]        [App_DataDictionary].[typeScopeName] Null,
	[ScopeDescription] [App_DataDictionary].[typeDescription] Null
)
