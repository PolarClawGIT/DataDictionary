CREATE TYPE [AppScript].[udttTemplateDocument] AS TABLE
(	-- POC Table. Replaces [SchemaDocument] & [TransformDocument]
	[DocumentId]			UniqueIdentifier Null,
	[DocumentTitle]			[AppGeneral].[uddtTitle] Null,
	[TemplateId]			UniqueIdentifier Null,
	[SchemaId]				UniqueIdentifier Null,
	[ObjectScope]			[AppGeneral].[uddtScopeName] Null,
	[ObjectPath]			[AppGeneral].[uddtPath] Null, -- Full Name including Member Name
	[IsExcluded]			Bit Null,
	[KeepOrphaned]			Bit Null,
	[DataFileName]			[AppGeneral].[uddtFileName] Null, -- Input
	[TransformId]			UniqueIdentifier Null,
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