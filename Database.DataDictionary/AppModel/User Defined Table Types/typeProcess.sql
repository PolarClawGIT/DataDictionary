CREATE TYPE [AppModel].[typeProcess] AS TABLE
(
    [ProcessId]            UniqueIdentifier NULL,
	[ProcessTitle]         [AppGeneral].[typeTitle] Null,
	[ProcessDescription]   [AppGeneral].[typeDescription] Null,
	[ProcessName]          [AppModel].[typeQualifiedName] Null,
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
