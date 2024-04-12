CREATE TYPE [App_DataDictionary].[typeScriptingSelection] AS TABLE
(
	[SelectionId]          UniqueIdentifier NULL,
	[SelectionTitle]       [App_DataDictionary].[typeTitle] Null,
	[SelectionDescription] [App_DataDictionary].[typeDescription] Null,
	[SchemaId]             UniqueIdentifier NULL, -- How should the be defined (XSD)
	[TransformId]          UniqueIdentifier NULL  -- How to transform the data (XSLT)
)
