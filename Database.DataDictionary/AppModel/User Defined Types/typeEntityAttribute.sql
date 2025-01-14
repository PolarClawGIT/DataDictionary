CREATE TYPE [AppModel].[typeEntityAttribute] AS TABLE (
    [EntityId]             UniqueIdentifier NULL,
	[AttributeTitle]       [App_DataDictionary].[typeTitle] Null,
	[AttributePath]        [App_DataDictionary].[typeNameSpacePath] Null,
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
