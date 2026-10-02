CREATE TYPE [AppScript].[udttTemplate] AS TABLE
(
	[TemplateId]            UniqueIdentifier NULL,
	[TemplateTitle]			[AppGeneral].[uddtTitle] Null,
	[TemplateDescription]	[AppGeneral].[uddtDescription] Null,
	[RootFolder]			[AppGeneral].[uddtFileRoot] Null, -- Name of the Special Folder used as the Root defined in the Application.
	[RelativePath]			[AppGeneral].[uddtFilePath] Null,
	-- Temporal Data
	[CreatedOn]             DateTime2 (7) Null,
	[CreatedBy]             NVarChar(4000) Null,
	[RemovedOn]             DateTime2 (7) Null,
	[RemovedBy]             NVarChar(4000) Null,
	[IsInserted]            Bit Null,
	[IsUpdated]             Bit Null,
	[IsDeleted]             Bit Null,
	[IsCurrent]             Bit Null);