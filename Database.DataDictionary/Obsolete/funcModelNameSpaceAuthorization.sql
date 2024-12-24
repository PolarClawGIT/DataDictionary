CREATE FUNCTION [AppSecurity].[funcModelNameSpaceAuthorization](@NameSpaceId UniqueIdentifier, @OwnerOnly Bit)
Returns Table With SchemaBinding
-- TODO: Suspect this will not work. 
-- The function is dependent on the Alias being added before the NameSpace is added.
-- But RI requires the Alias to be added first.
-- This could require a re-design.
--
-- VERFITED. NameSpace needs to be redesigned.
As Return 
With [NameSpace] As (

	Select	[NameSpaceId],
			[ModelId]
	From	[AppModel].[EntityAlias] A
			Inner Join [AppModel].[ModelEntity] M
			On	A.[EntityId] = M.[EntityId]
	Where	[NameSpaceId] = @NameSpaceId
	
	Union
	Select	[NameSpaceId],
			[ModelId]
	From	[App_DataDictionary].[DomainProcessAlias] A
			Inner Join [App_DataDictionary].[ModelProcess] M
			On	A.[ProcessId] = M.[ProcessId]
	Where	[NameSpaceId] = @NameSpaceId
	Union
	Select	[NameSpaceId],
			[ModelId]
	From	[App_DataDictionary].[DomainRelationshipAlias] A
			Inner Join [App_DataDictionary].[ModelRelationship] M
			On	A.[RelationshipId] = M.[RelationshipId]
	Where	[NameSpaceId] = @NameSpaceId),
[Model] As (
	Select	H.[NameSpaceId],
			IsNull(N.[ModelId],H.[ModelId]) As [ModelId],
			H.[ParentNameSpaceId]
	From	[AppModel].[NameSpaceHierarchy] H
			Left Join [NameSpace] N
			On	H.[NameSpaceId] = N.[NameSpaceId]
	Union All
	Select	H.[NameSpaceId],
			M.[ModelId],
			H.[ParentNameSpaceId]
	From	[Model] M
			Inner Join [AppModel].[NameSpaceHierarchy] H
			On	M.[ParentNameSpaceId] = H.[NameSpaceId])
Select	Convert(Bit,Max(1)) As [IsAllowed]
From	[Model] M
		Cross Apply [AppSecurity].[funcAuthorization](M.[ModelId]) F
Where	([IsDbWriter] = 1 Or
		 [IsModelAdmin] = 1 Or
		 ([IsModelOwner] = 1 And [IsOwner] = 1) Or
		 ([IsGrant] = 1 And [IsDeny] = 0 And IsNull(@OwnerOnly,0) = 0))
GO