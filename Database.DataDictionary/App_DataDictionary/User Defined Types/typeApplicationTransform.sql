CREATE TYPE [App_DataDictionary].[typeApplicationTransform] AS TABLE
(
	[TransformId]             UniqueIdentifier NOT NULL,
	[TransformTitle]          [App_DataDictionary].[typeTitle] Not Null,
	[TransformDescription]    [App_DataDictionary].[typeDescription] Null,
	[ScopeName]               [App_DataDictionary].[typeScopeName] Null,
	[AsText]                  Bit Null,
	[AsXml]                   Bit Null,
	[TransformScript]         XML Null
)
