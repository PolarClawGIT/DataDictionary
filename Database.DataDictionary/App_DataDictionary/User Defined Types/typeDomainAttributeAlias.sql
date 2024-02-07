CREATE TYPE [App_DataDictionary].[typeDomainAttributeAlias] AS TABLE 
(    -- TIP: This matches the C# DataTable structure and GET procedure
    [AttributeId]          UNIQUEIDENTIFIER NULL,
    [AliasName]            [App_DataDictionary].[typeNameSpaceFullName] Null,
	[ScopeName]            [App_DataDictionary].[typeScopeName] Null
)