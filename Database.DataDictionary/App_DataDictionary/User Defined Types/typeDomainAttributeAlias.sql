CREATE TYPE [App_DataDictionary].[typeDomainAttributeAlias] AS TABLE (
	-- TIP: This matches the C# DataTable structure
    [AttributeId]          UNIQUEIDENTIFIER NULL,
    [AliasName]            [App_DataDictionary].[typeNameSpace] Null,
    [ScopeName]            [App_DataDictionary].[typeScopeName] Null);