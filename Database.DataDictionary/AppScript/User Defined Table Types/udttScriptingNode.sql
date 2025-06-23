CREATE TYPE [AppScript].[udttScriptingNode] AS TABLE
(
	[TemplateId]            UniqueIdentifier NULL,
	[NodeId]				UniqueIdentifier NULL,
	[PropertyScope]         [AppGeneral].[uddtScopeName] Null,
	[PropertyName]          [AppGeneral].[uddtNameSpaceMember] Null,
	[NodeName]				[AppGeneral].[uddtNameSpaceMember] Null,
	[NodeValueAs]			NVarChar(50) Not Null
)
