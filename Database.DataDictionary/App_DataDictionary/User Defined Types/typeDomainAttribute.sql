CREATE TYPE [App_DataDictionary].[typeDomainAttribute] AS TABLE (
    [AttributeId]          UNIQUEIDENTIFIER                           NULL,
    [AttributeTitle]       [App_DataDictionary].[typeTitle]           NULL,
    [AttributeDescription] [App_DataDictionary].[typeDescription]     NULL,
    [MemberName]           [App_DataDictionary].[typeNameSpaceMember] NULL,
    [TypeOfAttributeId]    UNIQUEIDENTIFIER                           NULL,
    [TypeOfAttributeTitle] [App_DataDictionary].[typeTitle]           NULL,
    [IsSingleValue]        BIT                                        NULL,
    [IsMultiValue]         BIT                                        NULL,
    [IsSimpleType]         BIT                                        NULL,
    [IsCompositeType]      BIT                                        NULL,
    [IsIntegral]           BIT                                        NULL,
    [IsDerived]            BIT                                        NULL,
    [IsValued]             BIT                                        NULL,
    [IsNullable]           BIT                                        NULL,
    [IsKey]                BIT                                        NULL,
    [IsNonKey]             BIT                                        NULL);


