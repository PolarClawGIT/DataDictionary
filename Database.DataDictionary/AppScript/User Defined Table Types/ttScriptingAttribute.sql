CREATE TYPE [AppScript].[ttScriptingAttribute] AS TABLE
(
	[TemplateId]            UniqueIdentifier NULL,
	[NodeId]	            UniqueIdentifier NULL,
	[AttributeId]			UniqueIdentifier NULL,
	[AttributeName]			NVarChar(50) NULL,
	[AttributeValue]		NVarChar(250) NULL,
	[PropertyId]			UniqueIdentifier NULL
)
