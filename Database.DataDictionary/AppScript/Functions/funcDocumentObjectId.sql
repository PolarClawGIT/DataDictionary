CREATE FUNCTION [AppScript].[funcDocumentObjectId] (@ObjectPath [AppGeneral].[uddtPath])
-- Takes an ObjectPath and gets the Document ObjectId
-- Temporal Data NOT Supported
RETURNS UniqueIdentifier
AS
BEGIN
Declare	@Result UniqueIdentifier = null

;With [Data] As (
	Select	[MemberName] As [ObjectMember],
			[QualifiedName] As [ObjectPath],
			[Level],
			[IsBase]
	From	[AppGeneral].[funcParseName](@ObjectPath)),
[Search] As (
	Select	N.[ObjectId],
			N.[ParentObjectId],
			N.[ObjectMember],
			D.[Level],
			D.[IsBase]
	From	[Data] D
			Inner Join [AppScript].[DocumentObject] N
			On	D.[ObjectMember] = N.[ObjectMember] And
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
			Inner Join [AppScript].[DocumentObject] N
			On	S.[ObjectId] = N.[ParentObjectId] And
				D.[ObjectMember] = N.[ObjectMember])
Select	@Result = [ObjectId]
From	[Search]
Where	[IsBase] = 1

Return @Result
END
