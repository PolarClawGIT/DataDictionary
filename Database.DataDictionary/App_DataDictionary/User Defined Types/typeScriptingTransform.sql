CREATE TYPE [App_DataDictionary].[typeScriptingTransform] AS TABLE
(
	[TransformId]             UniqueIdentifier NULL,
	[TransformTitle]          [App_DataDictionary].[typeTitle] Null,
	[TransformDescription]    [App_DataDictionary].[typeDescription] Null,
	[AsText]                  Bit Null,
	[AsXml]                   Bit Null,
	[TransformScript]         XML Null -- C# code will use String and convert to XElement
)
