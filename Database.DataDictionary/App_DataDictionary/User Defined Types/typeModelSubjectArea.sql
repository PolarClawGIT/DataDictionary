CREATE TYPE [App_DataDictionary].[typeModelSubjectArea] AS TABLE
(
	[SubjectAreaId]          UniqueIdentifier NULL,
	[SubjectAreaParentId]    UniqueIdentifier NULL,
	[SubjectAreaTitle]       [App_DataDictionary].[typeTitle] NULL,
	[SubjectAreaDescription] [App_DataDictionary].[typeDescription] NULL
)
