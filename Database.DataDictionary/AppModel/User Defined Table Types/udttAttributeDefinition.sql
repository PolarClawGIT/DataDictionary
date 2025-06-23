CREATE TYPE [AppModel].[udttAttributeDefinition] AS TABLE (
	[AttributeId]          UniqueIdentifier NOT Null,
	[DefinitionId]         UniqueIdentifier NOT NULL,
	[DefinitionSummary]    [AppGeneral].[uddtDescription] Null,
	[DefinitionText]       [AppModel].[uddtRichText] Null,
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