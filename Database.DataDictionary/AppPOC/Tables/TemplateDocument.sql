CREATE TABLE [AppPOC].[TemplateDocument]
(
	[TemplateId]		UniqueIdentifier Not Null,
	[ObjectId]			UniqueIdentifier Not Null,
	[DocumentId]		UniqueIdentifier Not Null,
	[DataId]			UniqueIdentifier Null, -- Input
	[TransformId]		UniqueIdentifier Null, -- Output

)
