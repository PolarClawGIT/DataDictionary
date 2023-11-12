CREATE TYPE [App_DataDictionary].[typeDomainAlias] AS TABLE
(    -- TIP: This matches the C# DataTable structure and GET procedure
    [AliasId]             UNIQUEIDENTIFIER NULL,
    [SubjectAreaId]       UNIQUEIDENTIFIER NULL,
    [AliasTitle]          [App_DataDictionary].[typeTitle] Null,
    [AliasDescription]    [App_DataDictionary].[typeDescription] Null
)
