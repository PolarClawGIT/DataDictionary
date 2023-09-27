CREATE TYPE [App_DataDictionary].[typeLibrarySource] AS TABLE
(
	[LibraryId]            UniqueIdentifier Null,
	[LibraryTitle]         [App_DataDictionary].[typeTitle] Null,
	[LibraryDescription]   [App_DataDictionary].[typeDescription] Null,
	[AssemblyName]         NVarChar(1023) Null,
	[SourceFile]           NVarChar(500) Null, 
	[SourceDate]           DateTime Null,
    [SysStart]             DATETIME2 (7)    NULL
)
