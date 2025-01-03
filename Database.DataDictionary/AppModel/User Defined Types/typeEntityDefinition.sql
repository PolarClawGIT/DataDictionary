CREATE TYPE [AppModel].[typeEntityDefinition] AS TABLE (
	[EntityId]			   UniqueIdentifier NOT Null,
	[DefinitionId]         UniqueIdentifier NOT NULL,
	[DefinitionSummary]    [App_DataDictionary].[typeDescription] Null,
	[DefinitionText]       [AppModel].[typeRichText] Null,
	-- Temporal Data
	[CreatedOn]            DateTime2 (7) Null,
	[CreatedBy]            NVarChar(4000) Null,
	[RemovedOn]            DateTime2 (7) Null,
	[RemovedBy]            NVarChar(4000) Null,
	[IsInserted]           Bit Null,
	[IsUpdated]            Bit Null,
	[IsDeleted]            Bit Null,
	[IsCurrent]            Bit Null
);