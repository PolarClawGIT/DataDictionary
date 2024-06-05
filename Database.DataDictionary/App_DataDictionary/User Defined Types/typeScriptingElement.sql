CREATE TYPE [App_DataDictionary].[typeScriptingElement] AS TABLE
(
	[ElementId]             UniqueIdentifier NULL,
	[TemplateId]            UniqueIdentifier NULL,
	[PropertyScope]         [App_DataDictionary].[typeScopeName] Null,
	[PropertyName]          [App_DataDictionary].[typeNameSpaceMember] Null,
	[AsElement]             Bit Null,
	[ElementName]           [App_DataDictionary].[typeNameSpaceMember] Null,
	[ElementType]           NVarChar(50) Null,
	[DataAs]                NVarChar(10) Null
)
