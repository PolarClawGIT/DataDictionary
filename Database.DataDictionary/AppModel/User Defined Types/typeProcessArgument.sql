CREATE TYPE [AppModel].[typeProcessArgument] AS TABLE
(    -- TIP: This matches the C# DataTable structure and GET procedure
    [ProcessId]				UniqueIdentifier NULL,
	[ArgumentTitle]			[App_DataDictionary].[typeTitle] Null,
	[ArgumentDescription]	[App_DataDictionary].[typeDescription] Null,
	[ArgumentName]			[AppModel].[typeQualifiedName] Null,
	[ArgumentType]			[AppModel].[typeQualifiedName] Null,
	[OrdinalPosition]       Int Null,
	[IsInput]               Bit Null,
	[IsOutput]              Bit Null,
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