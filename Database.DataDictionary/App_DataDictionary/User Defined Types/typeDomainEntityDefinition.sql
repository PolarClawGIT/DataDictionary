CREATE TYPE [App_DataDictionary].[typeDomainEntityDefinition] AS TABLE (
	[EntityId]			UniqueIdentifier NOT Null,
	[DefinitionId]      UniqueIdentifier NOT NULL,
	[DefinitionSummary] [App_DataDictionary].[typeDescription] Null, -- Plain Text summary, used where RTF cannot be used.
	[DefinitionText]    NVarChar(Max) Null -- Contains Rich Text Definition. Rich Text must be handled differently.
);