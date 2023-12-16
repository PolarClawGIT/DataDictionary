CREATE TYPE [App_DataDictionary].[typeModel] AS TABLE
(    -- TIP: This matches the C# DataTable structure
	[ModelId]              UniqueIdentifier Null,
	[ModelTitle]           [App_DataDictionary].[typeTitle] Null,
	[ModelDescription]     [App_DataDictionary].[typeDescription] Null
)
