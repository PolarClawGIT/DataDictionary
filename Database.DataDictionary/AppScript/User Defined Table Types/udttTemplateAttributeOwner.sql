CREATE TYPE [AppScript].[udttTemplateAttributeOwner] AS TABLE
(
	[TemplateId]            UniqueIdentifier NULL,
	[AttributeId]			UniqueIdentifier NULL,
	[NodeId]				UniqueIdentifier NULL,
	-- Temporal Data
	[CreatedOn]             DateTime2 (7) Null,
	[CreatedBy]             NVarChar(4000) Null,
	[RemovedOn]             DateTime2 (7) Null,
	[RemovedBy]             NVarChar(4000) Null,
	[IsInserted]            Bit Null,
	[IsUpdated]             Bit Null,
	[IsDeleted]             Bit Null,
	[IsCurrent]             Bit Null);