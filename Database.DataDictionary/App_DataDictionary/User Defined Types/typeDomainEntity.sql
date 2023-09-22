CREATE TYPE [App_DataDictionary].[typeDomainEntity] AS TABLE (
    -- TIP: This matches the C# DataTable structure
    [EntityId]             UNIQUEIDENTIFIER NULL,
    [SubjectAreaId]        UNIQUEIDENTIFIER NULL,
    [EntityTitle]          NVARCHAR (100)   NULL,
    [EntityDescription]    NVARCHAR (1000)  NULL,
    [Obsolete]             BIT              NULL,
    [SysStart]             DATETIME2 (7)    NULL);