CREATE TYPE [AppLibrary].[typeLibrarySource] AS TABLE
(
	[LibraryId]            UniqueIdentifier Null,
	[LibraryTitle]         [AppGeneral].[typeTitle] Null,
	[LibraryDescription]   [AppGeneral].[typeDescription] Null,
	[AssemblyName]         NVarChar(128) Null,
	[SourceFile]           NVarChar(500) Null, 
	[SourceDate]           DateTime2 (7) Null
)
