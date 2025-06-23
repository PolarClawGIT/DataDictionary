CREATE TYPE [AppModel].[udttProcess] AS TABLE
(
    [ProcessId]            UniqueIdentifier NULL,
	[ProcessTitle]         [AppGeneral].[uddtTitle] Null,
	[ProcessDescription]   [AppGeneral].[uddtDescription] Null,
	[ProcessName]          [AppGeneral].[uddtQualifiedName] Null,
	-- Temporal Data
	[CreatedOn]            DateTime2 (7) Null,
	[CreatedBy]            NVarChar(4000) Null,
	[RemovedOn]            DateTime2 (7) Null,
	[RemovedBy]            NVarChar(4000) Null,
	[IsInserted]           Bit Null,
	[IsUpdated]            Bit Null,
	[IsDeleted]            Bit Null,
	[IsCurrent]            Bit Null
);
