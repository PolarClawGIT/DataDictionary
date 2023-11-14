CREATE TYPE [App_DataDictionary].[typeDomainAttributeAlias] AS TABLE 
(    -- TIP: This matches the C# DataTable structure and GET procedure
    [AttributeId]          UNIQUEIDENTIFIER NULL,
    [SourceName]           NVarChar(128) Null,
    [AliasName]            [App_DataDictionary].[typeAliasName] Null,
	[ScopeName]            [App_DataDictionary].[typeScopeName] Null
)