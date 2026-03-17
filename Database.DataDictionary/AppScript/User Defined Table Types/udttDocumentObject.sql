CREATE TYPE [AppScript].[udttDocumentObject] AS TABLE
(
	[ObjectId]				UniqueIdentifier NULL,
	[TemplateId]            UniqueIdentifier NULL,
	[ObjectScope]			[AppGeneral].[uddtScopeName] Null,
	[ObjectName]			[AppGeneral].[uddtPath] Null, -- Full Name including Member Name
	[IsExcluded]			Bit Null,
	[KeepOrphaned]			Bit Null,
	-- Temporal Data
	[CreatedOn]             DateTime2 (7) Null,
	[CreatedBy]             NVarChar(4000) Null,
	[RemovedOn]             DateTime2 (7) Null,
	[RemovedBy]             NVarChar(4000) Null,
	[IsInserted]            Bit Null,
	[IsUpdated]             Bit Null,
	[IsDeleted]             Bit Null,
	[IsCurrent]             Bit Null);