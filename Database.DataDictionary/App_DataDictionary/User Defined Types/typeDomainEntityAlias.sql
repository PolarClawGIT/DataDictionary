CREATE TYPE [App_DataDictionary].[typeDomainEntityAlias] AS TABLE 
(    -- TIP: This matches the C# DataTable structure and GET procedure
    [EntityId]             UNIQUEIDENTIFIER NULL,
    [AliasName]            [App_DataDictionary].[typeNameSpaceFullName] Null,
	[ScopeName]            [App_DataDictionary].[typeScopeName] Null
)