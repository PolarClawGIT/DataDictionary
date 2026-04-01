CREATE FUNCTION [ProofOfConcept].[funcObjectNameId](@NameSpace [AppGeneral].[uddtPath])
-- Takes an Data Object NameSpace and gets the ObjectId
-- Temporal Data NOT Supported
RETURNS UniqueIdentifier As 
BEGIN
Declare	@Result UniqueIdentifier = null

;With [Data] As (
	Select	[MemberName] As [DataMember],
			[QualifiedName] As [NameSpace],
			[Level],
			[IsBase]
	From	[AppGeneral].[funcParseName](@NameSpace)),
[Search] As (
	Select	N.[ObjectId],
			N.[ParentObjectId],
			N.[ObjectMember],
			D.[Level],
			D.[IsBase]
	From	[Data] D
			Inner Join [ProofOfConcept].[DataObject] N
			On	D.[DataMember] = N.[ObjectMember] And
				N.[ParentObjectId] is Null And
				D.[Level] = 1
	Union All
	Select	N.[ObjectId],
			N.[ParentObjectId],
			N.[ObjectMember],
			D.[Level],
			D.[IsBase]
	From	[Search] S
			Inner Join [Data] D
			On	S.[Level] + 1 = D.[Level]
			Inner Join [ProofOfConcept].[DataObject] N
			On	S.[ObjectId] = N.[ParentObjectId] And
				D.[DataMember] = N.[ObjectMember])
Select	@Result = [ObjectId]
From	[Search]
Where	[IsBase] = 1

Return @Result
END
