CREATE FUNCTION [AppScript].[funcDocumentObjectPath](@ObjectId UniqueIdentifier)
-- This takes the Object and rebuilds them into a Object Path.
-- Path is qualified by square brackets and delimited by periods.
-- Temporal Data NOT Supported
RETURNS [AppGeneral].[uddtPath] as 
BEGIN
	Declare @Result [AppGeneral].[uddtPath] = null

	;With [Data] As (
	Select	[ObjectId],
			NullIf([ParentObjectId], [ObjectId]) As [ParentObjectId],
			[AppGeneral].[funcCreatePath]([ObjectMember], Null) As [ObjectPath]
	From	[AppScript].[DocumentObject]
	Where	[ObjectId] = @ObjectId
	Union All
	Select	D.[ObjectId],
			NullIf(P.[ParentObjectId], D.[ObjectId]) As [ParentObjectId],
			[AppGeneral].[funcCreatePath](P.[ObjectMember], D.[ObjectPath]) As [ObjectPath]
	From	[Data] D
			Inner Join [AppScript].[DocumentObject] P
			On	D.[ParentObjectId] = P.[ObjectId])
Select	@Result = [ObjectPath]
From	[Data]
Where	[ParentObjectId] is Null

Return	@Result
END
GO