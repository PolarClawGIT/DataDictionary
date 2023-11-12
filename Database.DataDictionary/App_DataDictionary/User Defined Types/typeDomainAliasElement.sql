CREATE TYPE [App_DataDictionary].[typeDomainAliasElement] AS TABLE 
(    -- TIP: This matches the C# DataTable structure and GET procedure
	[AliasElementId]          UniqueIdentifier Null,
	[AliasId]                 UniqueIdentifier Null,
	[ScopeName]               [App_DataDictionary].[typeScopeName] Null,
	[AliasParentName]         [App_DataDictionary].[typeNameSpace] Null,
	[AliasElementName]        [App_DataDictionary].[typeNameSpaceElement] Null
)
