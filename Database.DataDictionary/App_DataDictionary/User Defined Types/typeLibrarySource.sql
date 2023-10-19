CREATE TYPE [App_DataDictionary].[typeLibrarySource] AS TABLE
(
	[LibraryId]            UniqueIdentifier Null,
	[LibraryTitle]         [App_DataDictionary].[typeTitle] Null,
	[LibraryDescription]   [App_DataDictionary].[typeDescription] Null,
	[AssemblyName]         NVarChar(128) Null,
	[SourceFile]           NVarChar(500) Null, 
	[SourceDate]           DateTime2 (7) Null,
    [SysStart]             DATETIME2 (7) NULL
)
