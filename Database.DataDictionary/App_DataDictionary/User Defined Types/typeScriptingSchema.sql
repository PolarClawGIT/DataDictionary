CREATE TYPE [App_DataDictionary].[typeScriptingSchema] AS TABLE
(
	[SchemaId]             UniqueIdentifier NULL,
	[SchemaTitle]          [App_DataDictionary].[typeTitle] Null,
	[SchemaDescription]    [App_DataDictionary].[typeDescription] Null
)
