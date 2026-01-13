CREATE TYPE [AppScript].[udttDocument] AS TABLE
(
	[DocumentId]			UniqueIdentifier Null,
	[DocumentTitle]			[AppGeneral].[uddtTitle] Null,
	[TemplateId]            UniqueIdentifier NULL,
	[TransformScript]		NVarChar(Max) Null,
	[RootFolder]			NVarChar(100) Null,
	[InputDirectory]		NVarChar(250) Null,
	[InputFile]             NVarChar(100) Null, 
	[OutputDirectory]		NVarChar(250) Null,
	[OutputFile]            NVarChar(100) Null,
	-- Temporal Data
	[CreatedOn]             DateTime2 (7) Null,
	[CreatedBy]             NVarChar(4000) Null,
	[RemovedOn]             DateTime2 (7) Null,
	[RemovedBy]             NVarChar(4000) Null,
	[IsInserted]            Bit Null,
	[IsUpdated]             Bit Null,
	[IsDeleted]             Bit Null,
	[IsCurrent]             Bit Null);

