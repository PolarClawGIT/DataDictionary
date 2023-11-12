CREATE TYPE [App_DataDictionary].[typeDomainAttributeAlias] AS TABLE 
(    -- TIP: This matches the C# DataTable structure and GET procedure
    [AttributeId]          UNIQUEIDENTIFIER NULL,
    [AliasParentName]      [App_DataDictionary].[typeNameSpace] Null,
	[AliasElementName]     [App_DataDictionary].[typeNameSpaceElement] Null
)