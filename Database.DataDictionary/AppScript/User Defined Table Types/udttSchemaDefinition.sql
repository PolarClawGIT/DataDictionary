CREATE TYPE [AppScript].[udttSchemaDefinition] AS TABLE
(
	[SchemaId]				UniqueIdentifier Null,
	[TemplateId]            UniqueIdentifier Null,
	[SchemaTitle]			[AppGeneral].[uddtTitle] Null,
	[ForEachScope]			[AppGeneral].[uddtScopeName] Null,
	[RootNodeName]			[AppGeneral].[uddtMember] Null,
	[RootFolder]			[AppGeneral].[uddtFileRoot] Null,
	[RelativePath]			[AppGeneral].[uddtFilePath] Null,
	[FilePrefix]			[AppGeneral].[uddtFileAffix] Null,
	[FileSuffix]			[AppGeneral].[uddtFileAffix] Null,
	[FileExtension]			[AppGeneral].[uddtFileExtension] Null,
	-- Temporal Data
	[CreatedOn]             DateTime2 (7) Null,
	[CreatedBy]             NVarChar(4000) Null,
	[RemovedOn]             DateTime2 (7) Null,
	[RemovedBy]             NVarChar(4000) Null,
	[IsInserted]            Bit Null,
	[IsUpdated]             Bit Null,
	[IsDeleted]             Bit Null,
	[IsCurrent]             Bit Null);