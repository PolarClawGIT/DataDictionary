CREATE TYPE [AppModel].[typeEntityAlias] AS TABLE 
(    -- TIP: This matches the C# DataTable structure and GET procedure
    [EntityId]             UNIQUEIDENTIFIER NULL,
    [AliasName]            [App_DataDictionary].[typeNameSpacePath] Null,
	[AliasScope]           [App_DataDictionary].[typeScopeName] Null
)