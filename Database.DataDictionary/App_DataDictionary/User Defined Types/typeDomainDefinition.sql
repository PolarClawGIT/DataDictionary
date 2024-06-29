CREATE TYPE [App_DataDictionary].[typeDomainDefinition] AS TABLE
(
	[DefinitionId]             UniqueIdentifier NULL,
	[DefinitionTitle]          [App_DataDictionary].[typeTitle] Null,
	[DefinitionDescription]    [App_DataDictionary].[typeDescription] Null
)
