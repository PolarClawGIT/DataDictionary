CREATE TYPE [App_DataDictionary].[typeScriptingNode] AS TABLE
(
	[TemplateId]            UniqueIdentifier NULL,
	[NodeId]				UniqueIdentifier NULL,
	[PropertyScope]         [App_DataDictionary].[typeScopeName] Null,
	[PropertyName]          [App_DataDictionary].[typeNameSpaceMember] Null,
	[NodeName]				[App_DataDictionary].[typeNameSpaceMember] Null,
	[NodeValueAs]			NVarChar(50) Not Null
)
