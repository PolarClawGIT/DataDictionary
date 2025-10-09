CREATE TYPE [AppModel].[udttEntityAttribute] AS TABLE
(
    [EntityId]             UniqueIdentifier NULL,
	[AttributeKnownAs]     [AppGeneral].[uddtTitle] Null,
	[AttributeName]        [AppGeneral].[uddtPath] Null,
	[OrdinalPosition]      Int Not Null,
	[IsNullable]		   Bit Null,
	[IsPrimaryKey]		   Bit Null,
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
