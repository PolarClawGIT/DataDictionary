CREATE TYPE [AppScript].[typeScriptingNode] AS TABLE
(
	[TemplateId]            UniqueIdentifier NULL,
	[NodeId]				UniqueIdentifier NULL,
	[PropertyScope]         [AppModel].[typeScopeName] Null,
	[PropertyName]          [AppGeneral].[typeNameSpaceMember] Null,
	[NodeName]				[AppGeneral].[typeNameSpaceMember] Null,
	[NodeValueAs]			NVarChar(50) Not Null
)
