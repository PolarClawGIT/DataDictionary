CREATE TYPE [AppScript].[udttDocument] AS TABLE
(
	[DocumentId]			UniqueIdentifier Null,
	[DocumentTitle]			[AppGeneral].[uddtTitle] Null,
	[TemplateId]			UniqueIdentifier Null,
	[SchemaId]				UniqueIdentifier Null,
	[TransformId]			UniqueIdentifier Null,
	[StructuredFileName]	[AppGeneral].[uddtFileName] Null, -- Input
	[TransformFileName]		[AppGeneral].[uddtFileName] Null, -- Process
	[ScriptedFileName]		[AppGeneral].[uddtFileName] Null, -- Output
	-- Temporal Data
	[CreatedOn]             DateTime2 (7) Null,
	[CreatedBy]             NVarChar(4000) Null,
	[RemovedOn]             DateTime2 (7) Null,
	[RemovedBy]             NVarChar(4000) Null,
	[IsInserted]            Bit Null,
	[IsUpdated]             Bit Null,
	[IsDeleted]             Bit Null,
	[IsCurrent]             Bit Null);