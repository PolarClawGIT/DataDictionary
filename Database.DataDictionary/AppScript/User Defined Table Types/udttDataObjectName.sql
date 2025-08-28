CREATE TYPE [AppScript].[udttDataObjectName] AS TABLE
(
	[DataSourceId]			UniqueIdentifier Not Null,
	[ObjectPath]			[AppGeneral].[uddtNameSpacePath] Null
	-- Temporal Data (not needed, internal only)
	/*[CreatedOn]             DateTime2 (7) Null,
	[CreatedBy]             NVarChar(4000) Null,
	[RemovedOn]             DateTime2 (7) Null,
	[RemovedBy]             NVarChar(4000) Null,
	[IsInserted]            Bit Null,
	[IsUpdated]             Bit Null,
	[IsDeleted]             Bit Null,
	[IsCurrent]             Bit Null*/
)
