CREATE TYPE [AppScript].[ttScriptingNode] AS TABLE
(
	[TemplateId]            UniqueIdentifier NULL,
	[NodeId]				UniqueIdentifier NULL,
	[PropertyScope]         [AppModel].[typeScopeName] Null,
	[PropertyName]          [AppGeneral].[dtNameSpaceMember] Null,
	[NodeName]				[AppGeneral].[dtNameSpaceMember] Null,
	[NodeValueAs]			NVarChar(50) Not Null
)
