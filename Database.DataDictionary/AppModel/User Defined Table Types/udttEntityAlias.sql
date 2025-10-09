CREATE TYPE [AppModel].[udttEntityAlias] AS TABLE 
(    -- TIP: This matches the C# DataTable structure and GET procedure
    [EntityId]             UniqueIdentifier NULL,
	[AliasScope]           [AppGeneral].[uddtScopeName] Null,
	[AliasPath]            [AppGeneral].[uddtPath] Null,
	-- Temporal Data
	[CreatedOn]            DateTime2 (7) Null,
	[CreatedBy]            NVarChar(4000) Null,
	[RemovedOn]            DateTime2 (7) Null,
	[RemovedBy]            NVarChar(4000) Null,
	[IsInserted]           Bit Null,
	[IsUpdated]            Bit Null,
	[IsDeleted]            Bit Null,
	[IsCurrent]            Bit Null
)