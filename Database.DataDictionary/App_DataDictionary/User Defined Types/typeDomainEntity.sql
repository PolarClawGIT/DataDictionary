CREATE TYPE [App_DataDictionary].[typeDomainEntity] AS TABLE (
    -- TIP: This matches the C# DataTable structure
    [EntityId]             UNIQUEIDENTIFIER NULL,
    [SubjectAreaId]        UNIQUEIDENTIFIER NULL,
    [EntityTitle]          [App_DataDictionary].[typeTitle] Null,
    [EntityDescription]    [App_DataDictionary].[typeDescription] Null
    );