CREATE TYPE [App_DataDictionary].[typeDomainAttribute] AS TABLE (
    -- TIP: This matches the C# DataTable structure
    [AttributeId]          UNIQUEIDENTIFIER NULL,
    [AttributeParentId]    UNIQUEIDENTIFIER NULL,
    [AttributeTitle]       NVARCHAR (100)   NULL,
    [AttributeDescription] NVARCHAR (MAX)   NULL,
    [Obsolete]             BIT              NULL,
    [SysStart]             DATETIME2 (7)    NULL);
