CREATE TYPE [AppScript].[udttTransformDocument] AS TABLE
(
	[DocumentId]			UniqueIdentifier Null,
	[TemplateId]			UniqueIdentifier Null,
	[TransformId]			UniqueIdentifier Null,
	[DataFileName]			[AppGeneral].[uddtFileName] Null, -- Input
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