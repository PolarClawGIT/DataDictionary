CREATE TYPE [AppModel].[udttEntity] AS TABLE (
    [EntityId]             UniqueIdentifier NULL,
	[EntityTitle]          [AppGeneral].[uddtTitle] Null,
	[EntityDescription]    [AppGeneral].[uddtDescription] Null,
	[EntityName]           [AppGeneral].[uddtQualifiedName] Null,
	-- Temporal Data
	[CreatedOn]            DateTime2 (7) Null,
	[CreatedBy]            NVarChar(4000) Null,
	[RemovedOn]            DateTime2 (7) Null,
	[RemovedBy]            NVarChar(4000) Null,
	[IsInserted]           Bit Null,
	[IsUpdated]            Bit Null,
	[IsDeleted]            Bit Null,
	[IsCurrent]            Bit Null);

