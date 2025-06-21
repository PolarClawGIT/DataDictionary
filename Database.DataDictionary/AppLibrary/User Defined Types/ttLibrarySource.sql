CREATE TYPE [AppLibrary].[ttLibrarySource] AS TABLE
(
	[LibraryId]            UniqueIdentifier Null,
	[LibraryTitle]         [AppGeneral].[dtTitle] Null,
	[LibraryDescription]   [AppGeneral].[dtDescription] Null,
	[AssemblyName]         NVarChar(128) Null,
	[SourceFile]           NVarChar(500) Null, 
	[SourceDate]           DateTime2 (7) Null
)
