CREATE TYPE [App_DataDictionary].[typeDomainAttributeAlias] AS TABLE (
	-- TIP: This matches the C# DataTable structure
    [AttributeId]          UNIQUEIDENTIFIER NULL,
    [AliasName]            NVarChar(Max) Null,
    [ScopeName]            NVarChar(Max) Null);