CREATE TYPE [AppModel].[udttProcessArgument] AS TABLE
(    -- TIP: This matches the C# DataTable structure and GET procedure
    [ProcessId]				UniqueIdentifier NULL,
	[ArgumentKnownAs]		[AppGeneral].[uddtTitle] Null,
	[ArgumentName]			[AppGeneral].[uddtNameSpacePath] Null,
	[OrdinalPosition]       Int Null,
	[IsPassed]				Bit Null,
	[IsReturned]			Bit Null,
	[IsContributor]			Bit Null,
	[IsAltered]				Bit Null,
	[AsValue]				Bit Null,
	[AsReference]			Bit Null,
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