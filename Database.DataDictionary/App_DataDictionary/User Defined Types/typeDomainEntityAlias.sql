CREATE TYPE [App_DataDictionary].[typeDomainEntityAlias] AS TABLE (
	-- TIP: This matches the C# DataTable structure
    [EntityId]             UNIQUEIDENTIFIER NULL,
    [AliasName]            [App_DataDictionary].[typeNameSpace] Null,
    [ScopeName]            [App_DataDictionary].[typeScopeName] Null);