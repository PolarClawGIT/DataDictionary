CREATE TYPE [App_DataDictionary].[typeModelAttribute] AS TABLE
(
	--[ModelId]              UniqueIdentifier NULL,
	[AttributeId]            UniqueIdentifier NULL,
	[AttributeTitle]         [App_DataDictionary].[typeTitle] Null,
	[SubjectAreaId]          UniqueIdentifier Null,
	[SubjectAreaTitle]       [App_DataDictionary].[typeTitle] NULL
)
