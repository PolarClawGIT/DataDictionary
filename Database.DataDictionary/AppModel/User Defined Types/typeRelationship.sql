CREATE TYPE [AppModel].[typeRelationship] AS TABLE
(
    [RelationshipId]            UniqueIdentifier NULL,
	[RelationshipTitle]         [App_DataDictionary].[typeTitle] Null,
	[RelationshipDescription]   [App_DataDictionary].[typeDescription] Null,
	[RelationshipName]          [AppModel].[typeQualifiedName] Null,
	[RelationshipType]          NVarChar(20) Null,
	--[OwnerAliasScope]           [AppModel].[typeScopeName] Null,
	[OwnerAliasPath]            [App_DataDictionary].[typeNameSpacePath] Null,
	--[RefrenceAliasScope]        [AppModel].[typeScopeName] Null,
	[RefrenceAliasPath]         [App_DataDictionary].[typeNameSpacePath] Null,
	-- Temporal Data
	[CreatedOn]            DateTime2 (7) Null,
	[CreatedBy]            NVarChar(4000) Null,
	[RemovedOn]            DateTime2 (7) Null,
	[RemovedBy]            NVarChar(4000) Null,
	[IsInserted]           Bit Null,
	[IsUpdated]            Bit Null,
	[IsDeleted]            Bit Null,
	[IsCurrent]            Bit Null
);
