CREATE TYPE [App_DataDictionary].[typeDomainEntityAlias] AS TABLE (
	-- TIP: This matches the C# DataTable structure
    [EntityId]             UNIQUEIDENTIFIER NULL,
    [AliasName]            NVarChar(Max) Null,
    [ScopeName]            NVarChar(Max) Null);