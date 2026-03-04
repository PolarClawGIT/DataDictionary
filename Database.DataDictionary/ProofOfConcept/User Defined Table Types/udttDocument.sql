CREATE TYPE [ProofOfConcept].[udttDocument] AS TABLE
(
	[TemplateId]            UniqueIdentifier NULL,
	--[DocumentId]            UniqueIdentifier Null,
	[SchemaId]				UniqueIdentifier Null,
	[TransformId]			UniqueIdentifier Null,
	--[ObjectId]				UniqueIdentifier Null, -- Determined by [TemplateId] & [ObjectPath]
	[ObjectScope]			[AppGeneral].[uddtScopeName] Null,
	[ObjectPath]			[AppGeneral].[uddtPath] Null,
	[RootFolder]			[AppGeneral].[uddtFileRoot] Null,
	[RelativePath]			[AppGeneral].[uddtFilePath] Null, 
	[FileName]				[AppGeneral].[uddtFileName] Null,
	-- Temporal Data
	[CreatedOn]             DateTime2 (7) Null,
	[CreatedBy]             NVarChar(4000) Null,
	[RemovedOn]             DateTime2 (7) Null,
	[RemovedBy]             NVarChar(4000) Null,
	[IsInserted]            Bit Null,
	[IsUpdated]             Bit Null,
	[IsDeleted]             Bit Null,
	[IsCurrent]             Bit Null);
