CREATE TYPE [ProofOfConcept].[udttDataObject] AS TABLE
(
	[TemplateId]            UniqueIdentifier NULL,
	[ObjectScope]			[AppGeneral].[uddtScopeName] Null,
	[ObjectPath]			[AppGeneral].[uddtPath] Null,
	-- Temporal Data
	[CreatedOn]             DateTime2 (7) Null,
	[CreatedBy]             NVarChar(4000) Null,
	[RemovedOn]             DateTime2 (7) Null,
	[RemovedBy]             NVarChar(4000) Null,
	[IsInserted]            Bit Null,
	[IsUpdated]             Bit Null,
	[IsDeleted]             Bit Null,
	[IsCurrent]             Bit Null);