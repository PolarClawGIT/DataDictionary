CREATE TYPE [AppLibrary].[udttLibrarySource] AS TABLE
(
	[LibraryId]            UniqueIdentifier Null,
	[LibraryTitle]         [AppGeneral].[uddtTitle] Null,
	[LibraryDescription]   [AppGeneral].[uddtDescription] Null,
	[AssemblyName]         NVarChar(128) Null,
	[SourceFile]           NVarChar(500) Null, 
	[SourceDate]           DateTime2 (7) Null
)
