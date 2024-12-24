CREATE TYPE [AppModel].[typeEntity] AS TABLE (
    [EntityId]             UniqueIdentifier NULL,
	[EntityTitle]          [App_DataDictionary].[typeTitle] Null,
	[EntityDescription]    [App_DataDictionary].[typeDescription] Null,
	[EntityName]           [AppModel].[typeQualifiedName] Null,
	-- Temporal Data
	[CreatedOn]            DateTime2 (7) Null,
	[CreatedBy]            NVarChar(4000) Null,
	[RemovedOn]            DateTime2 (7) Null,
	[RemovedBy]            NVarChar(4000) Null,
	[IsInserted]           Bit Null,
	[IsUpdated]            Bit Null,
	[IsDeleted]            Bit Null,
	[IsCurrent]            Bit Null);

