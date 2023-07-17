CREATE TYPE [App_DataDictionary].[typeDomainAttribute] AS TABLE (
    [AttributeId]          UNIQUEIDENTIFIER NULL,
    [AttributeParentId]    UNIQUEIDENTIFIER NULL,
    [AttributeTitle]       NVARCHAR (100)   NULL,
    [AttributeDescription] NVARCHAR (MAX)   NULL,
    [SysStart]             DATETIME2 (7)    NULL
    );
