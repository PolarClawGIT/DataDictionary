CREATE TYPE [AppScript].[typeScriptingNode] AS TABLE
(
	[TemplateId]            UniqueIdentifier NULL,
	[NodeId]				UniqueIdentifier NULL,
	[PropertyScope]         [AppModel].[typeScopeName] Null,
	[PropertyName]          [App_DataDictionary].[typeNameSpaceMember] Null,
	[NodeName]				[App_DataDictionary].[typeNameSpaceMember] Null,
	[NodeValueAs]			NVarChar(50) Not Null
)
