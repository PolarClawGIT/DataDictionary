CREATE TYPE [AppModel].[typeDefinition] AS TABLE
(
	[DefinitionId]             UniqueIdentifier NULL,
	[DefinitionTitle]          [AppGeneral].[dtTitle] Null,
	[DefinitionDescription]    [AppGeneral].[dtDescription] Null,
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
