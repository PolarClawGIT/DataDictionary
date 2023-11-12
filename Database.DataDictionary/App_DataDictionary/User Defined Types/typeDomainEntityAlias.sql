CREATE TYPE [App_DataDictionary].[typeDomainEntityAlias] AS TABLE 
(    -- TIP: This matches the C# DataTable structure and GET procedure
    [EntityId]             UNIQUEIDENTIFIER NULL,
    [AliasParentName]      [App_DataDictionary].[typeNameSpace] Null,
	[AliasElementName]     [App_DataDictionary].[typeNameSpaceElement] Null
	);