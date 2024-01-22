CREATE TYPE [App_DataDictionary].[typeModelEntity] AS TABLE
(
	--[ModelId]              UniqueIdentifier NULL,
	[EntityId]               UniqueIdentifier NULL,
	[EntityTitle]            [App_DataDictionary].[typeTitle] Null,
	[SubjectAreaId]          UniqueIdentifier Null,
	[SubjectAreaTitle]       [App_DataDictionary].[typeTitle] NULL
)

