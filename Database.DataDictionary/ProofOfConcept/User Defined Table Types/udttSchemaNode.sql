CREATE TYPE [ProofOfConcept].[udttSchemaNode] AS TABLE
(
	[TemplateId]            UniqueIdentifier Null,
	[SchemaId]				UniqueIdentifier Null,
	[NodeId]				UniqueIdentifier Null,
	[NodeName]				[AppGeneral].[uddtMember] Null,
	[NodeOrder]				Int Not Null,
	[RenderValueAs]			NVarChar(20) Not Null,
	[FixedValue]			NVarChar(250) Null,
	[ObjectScope]			[AppGeneral].[uddtScopeName] Null,
	[ObjectProperty]		[AppGeneral].[uddtQualifiedName] Null,
	[ModelPropertyId]		UniqueIdentifier NULL,
	-- Temporal Data
	[CreatedOn]             DateTime2 (7) Null,
	[CreatedBy]             NVarChar(4000) Null,
	[RemovedOn]             DateTime2 (7) Null,
	[RemovedBy]             NVarChar(4000) Null,
	[IsInserted]            Bit Null,
	[IsUpdated]             Bit Null,
	[IsDeleted]             Bit Null,
	[IsCurrent]             Bit Null);