CREATE TYPE [App_DataDictionary].[typeModelSubjectArea] AS TABLE (
    [SubjectAreaId]          UNIQUEIDENTIFIER                           NULL,
    [SubjectAreaTitle]       [App_DataDictionary].[typeTitle]           NULL,
    [SubjectAreaDescription] [App_DataDictionary].[typeDescription]     NULL,
    [SubjectName]            [App_DataDictionary].[typeNameSpacePath]   NULL);


