CREATE TYPE [App_DataDictionary].[typeDomainEntityAttribute] AS TABLE (
    [EntityId]          UNIQUEIDENTIFIER NULL,
    [AttributeId]       UNIQUEIDENTIFIER NULL,
    [AttributeName]     [App_DataDictionary].[typeTitle] Null,
    [IsNullable]        Bit Null,
    [OrdinalPosition]   Int Null
)
