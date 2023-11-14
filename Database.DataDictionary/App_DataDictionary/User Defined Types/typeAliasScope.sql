CREATE TYPE [App_DataDictionary].[typeAliasScope] AS TABLE
(
    -- TIP: This matches the C# DataTable structure
	[ScopeId]          Int Null,
	[ScopedName]       [App_DataDictionary].[typeScopeName] Null,
	[ScopeDescription] [App_DataDictionary].[typeDescription] Null
)
