CREATE TYPE [AppModel].[typeProcessArgument] AS TABLE
(    -- TIP: This matches the C# DataTable structure and GET procedure
    [ProcessId]				UniqueIdentifier NULL,
	[ArgumentTitle]			[App_DataDictionary].[typeTitle] Null,
	[ArgumentDescription]	[App_DataDictionary].[typeDescription] Null,
	[ArgumentName]			[AppModel].[typeQualifiedName] Null,
	[ArgumentType]			[AppModel].[typeQualifiedName] Null,
	[OrdinalPosition]       Int Null,
	[IsPassed]				Bit Not Null,
	[IsReturned]			Bit Not Null,
	[IsContributor]			Bit Not Null,
	[IsAltered]				Bit Not Null,
	[AsValue]				Bit Not Null,
	[AsReference]			Bit Not Null,
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