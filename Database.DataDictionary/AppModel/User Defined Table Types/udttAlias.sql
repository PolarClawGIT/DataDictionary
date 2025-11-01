CREATE TYPE [AppModel].[udttAlias] AS TABLE 
(    -- TIP: This matches the C# DataTable structure and GET procedure
    [AliasId]              UniqueIdentifier NULL,
	--[AliasScope]           [AppModel].[typeScopeName] Null,
	[AliasNameSpace]       [AppGeneral].[uddtPath] Null--,
	-- Temporal Data (Not supported)
	--[CreatedOn]            DateTime2 (7) Null,
	--[CreatedBy]            NVarChar(4000) Null,
	--[RemovedOn]            DateTime2 (7) Null,
	--[RemovedBy]            NVarChar(4000) Null,
	--[IsInserted]           Bit Null,
	--[IsUpdated]            Bit Null,
	--[IsDeleted]            Bit Null,
	--[IsCurrent]            Bit Null
)