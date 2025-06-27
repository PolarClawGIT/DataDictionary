CREATE TYPE [AppScript].[udttScriptingNode] AS TABLE
(
	[TemplateId]            UniqueIdentifier NULL,
	[NodeId]				UniqueIdentifier NULL,
	[PropertyScope]         [AppGeneral].[uddtScopeName] Null,
	[PropertyName]          [AppGeneral].[uddtNameSpaceMember] Null,
	[NodeName]				[AppGeneral].[uddtNameSpaceMember] Null,
	[NodeValueAs]			NVarChar(50) Not Null,
	-- Temporal Data
	[CreatedOn]             DateTime2 (7) Null,
	[CreatedBy]             NVarChar(4000) Null,
	[RemovedOn]             DateTime2 (7) Null,
	[RemovedBy]             NVarChar(4000) Null,
	[IsInserted]            Bit Null,
	[IsUpdated]             Bit Null,
	[IsDeleted]             Bit Null,
	[IsCurrent]             Bit Null);