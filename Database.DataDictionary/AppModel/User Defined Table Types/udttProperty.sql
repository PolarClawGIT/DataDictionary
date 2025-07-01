CREATE TYPE [AppModel].[udttProperty] AS TABLE
(
	[PropertyId]          UniqueIdentifier Null,
	[PropertyTitle]       [AppGeneral].[uddtTitle] Null,
	[PropertyDescription] [AppGeneral].[uddtDescription] Null,
	[IsCommon]            Bit Null,
	[DataType]            NVarChar(20) Null,
	[PropertyData]        NVarChar(2000) Null,
	-- Temporal Data
	[CreatedOn]           DateTime2 (7) Null,
	[CreatedBy]           NVarChar(4000) Null,
	[RemovedOn]           DateTime2 (7) Null,
	[RemovedBy]           NVarChar(4000) Null,
	[IsInserted]          Bit Null,
	[IsUpdated]           Bit Null,
	[IsDeleted]           Bit Null,
	[IsCurrent]           Bit Null
)
