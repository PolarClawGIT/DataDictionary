CREATE TYPE [App_DataDictionary].[typeDomainEntity] AS TABLE (
    [EntityId]          UNIQUEIDENTIFIER                           NULL,
    [EntityTitle]       [App_DataDictionary].[typeTitle]           NULL,
    [EntityDescription] [App_DataDictionary].[typeDescription]     NULL,
    [MemberName]        [App_DataDictionary].[typeNameSpaceMember] NULL);

