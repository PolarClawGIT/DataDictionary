CREATE TYPE [Obsolete].[udttDocument] AS TABLE
(
	[DocumentId]			UniqueIdentifier Null,
	[DocumentTitle]			[AppGeneral].[uddtTitle] Null,
	[TemplateId]            UniqueIdentifier NULL,
	[RootFolder]			[AppGeneral].[uddtFileRoot] Null,
	[InputPath]				[AppGeneral].[uddtFilePath] Null,
	[InputFile]             [AppGeneral].[uddtFileName] Null, 
	[ProcessPath]			[AppGeneral].[uddtFilePath] Null,
	[ProcessFile]           [AppGeneral].[uddtFileName] Null, 
	[OutputPath]			[AppGeneral].[uddtFilePath] Null,
	[OutputFile]            [AppGeneral].[uddtFileName] Null,
	-- Temporal Data
	[CreatedOn]             DateTime2 (7) Null,
	[CreatedBy]             NVarChar(4000) Null,
	[RemovedOn]             DateTime2 (7) Null,
	[RemovedBy]             NVarChar(4000) Null,
	[IsInserted]            Bit Null,
	[IsUpdated]             Bit Null,
	[IsDeleted]             Bit Null,
	[IsCurrent]             Bit Null);

