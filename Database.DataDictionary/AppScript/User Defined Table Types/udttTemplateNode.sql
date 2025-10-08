CREATE TYPE [AppScript].[udttTemplateNode] AS TABLE
(
	[TemplateId]            UniqueIdentifier NULL,
	[NodeId]				UniqueIdentifier NULL,
	[NodeName]				[AppGeneral].[uddtNameSpaceMember] NULL,
	[NodeOrder]			Int Null,
	[RenderValueAs]			NVarChar(20) Null,
	[FixedValue]			NVarChar(250) NULL,
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
