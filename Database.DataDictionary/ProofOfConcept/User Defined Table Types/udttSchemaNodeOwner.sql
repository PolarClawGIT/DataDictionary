CREATE TYPE [ProofOfConcept].[udttSchemaNodeOwner] AS TABLE
(
	[TemplateId]            UniqueIdentifier NULL,
	[SchemaId]				UniqueIdentifier Null,
	[NodeId]				UniqueIdentifier Null, -- Child
	[NodeOwnerId]			UniqueIdentifier Null, -- Parent
	-- Temporal Data
	[CreatedOn]             DateTime2 (7) Null,
	[CreatedBy]             NVarChar(4000) Null,
	[RemovedOn]             DateTime2 (7) Null,
	[RemovedBy]             NVarChar(4000) Null,
	[IsInserted]            Bit Null,
	[IsUpdated]             Bit Null,
	[IsDeleted]             Bit Null,
	[IsCurrent]             Bit Null);