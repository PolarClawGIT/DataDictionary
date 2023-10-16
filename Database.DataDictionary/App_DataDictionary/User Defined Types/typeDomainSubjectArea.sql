CREATE TYPE [App_DataDictionary].[typeDomainSubjectArea] AS TABLE
(
	[SubjectAreaId]				UniqueIdentifier Null,
	[SubjectAreaTitle]			[App_DataDictionary].[typeTitle] Null,
	[SubjectAreaDescription]	[App_DataDictionary].[typeDescription] Null,
    [SysStart]                  DATETIME2 (7)    NULL
)
