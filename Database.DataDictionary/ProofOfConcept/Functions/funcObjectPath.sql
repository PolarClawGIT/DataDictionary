CREATE FUNCTION [ProofOfConcept].[funcObjectPath](@ObjectNameId UniqueIdentifier)
-- This takes the @ObjectNameId and rebuilds them into a Data Object NameSpace.
-- NameSpace is qualified by square brackets and delimited by periods.
-- Temporal Data NOT Supported
RETURNS [AppGeneral].[uddtPath] as 
BEGIN
	Declare @Result [AppGeneral].[uddtPath] = null

	;With [Data] As (
	Select	[ObjectId],
			[ParentObjectId],
			[AppGeneral].[funcCreatePath]([ObjectMember], Null) As [ObjectPath]
	From	[ProofOfConcept].[DataObject]
	Where	[ObjectId] = @ObjectNameId
	Union All
	Select	D.[ObjectId],
			NullIf(P.[ParentObjectId], D.[ObjectId]) As [ParentObjectId],
			[AppGeneral].[funcCreatePath](P.[ObjectMember],D.[ObjectPath]) As [ObjectPath]
	From	[Data] D
			Inner Join [ProofOfConcept].[DataObject] P
			On	D.[ParentObjectId] = P.[ObjectId])
Select	@Result = [ObjectPath]
From	[Data]
Where	[ParentObjectId] is Null

Return	@Result
END
