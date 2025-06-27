CREATE TYPE [AppLibrary].[udttLibrarySource] AS TABLE
(
	[LibraryId]            UniqueIdentifier Null,
	[LibraryTitle]         [AppGeneral].[uddtTitle] Null,
	[LibraryDescription]   [AppGeneral].[uddtDescription] Null,
	[AssemblyName]         NVarChar(128) Null,
	[SourceFile]           NVarChar(500) Null, 
	[SourceDate]           DateTime2 (7) Null,
	-- Temporal Data
	[CreatedOn]             DateTime2 (7) Null,
	[CreatedBy]             NVarChar(4000) Null,
	[RemovedOn]             DateTime2 (7) Null,
	[RemovedBy]             NVarChar(4000) Null,
	[IsInserted]            Bit Null,
	[IsUpdated]             Bit Null,
	[IsDeleted]             Bit Null,
	[IsCurrent]             Bit Null
)
