CREATE TYPE [App_DataDictionary].[typeDomainAttribute] AS TABLE (
    -- TIP: This matches the C# DataTable structure
    [AttributeId]          UNIQUEIDENTIFIER NULL,
    [AttributeTitle]       [App_DataDictionary].[typeTitle] Null,
    [AttributeDescription] [App_DataDictionary].[typeDescription] Null,
    [TypeOfAttributeId]    UniqueIdentifier Null,
    [TypeOfAttributeTitle] [App_DataDictionary].[typeTitle] Null,
    [IsSingleValue]        Bit Null,
    --[IsMultiValue]         Bit Null,
    [IsSimple]             Bit Null,
    --[IsComposite]          Bit Null,
    [IsDerived]            Bit Null,
    [IsNullable]           Bit Null,
    [IsKey]                Bit Null)
