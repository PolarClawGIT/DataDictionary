CREATE TYPE [AppModel].[udttDefinition] AS TABLE
(
	[DefinitionId]             UniqueIdentifier NULL,
	[DefinitionTitle]          [AppGeneral].[uddtTitle] Null,
	[DefinitionDescription]    [AppGeneral].[uddtDescription] Null,
	[IsCommon]                 Bit Null,
	-- Temporal Data
	[CreatedOn]	               DateTime2 (7) Null,
	[CreatedBy]	               NVarChar(4000) Null,
	[RemovedOn]	               DateTime2 (7) Null,
	[RemovedBy]	               NVarChar(4000) Null,
	[IsInserted]               Bit Null,
	[IsUpdated]	               Bit Null,
	[IsDeleted]	               Bit Null,
	[IsCurrent]	               Bit Null
)
