CREATE TYPE [App_DataDictionary].[typeDomainAttribute] AS TABLE (
    -- TIP: This matches the C# DataTable structure
    [AttributeId]          UNIQUEIDENTIFIER NULL,
    [SubjectAreaId]        UNIQUEIDENTIFIER NULL,
    [AttributeTitle]       [App_DataDictionary].[typeTitle] Null,
    [AttributeDescription] [App_DataDictionary].[typeDescription] Null
    )
