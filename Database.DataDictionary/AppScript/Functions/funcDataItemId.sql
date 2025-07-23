CREATE FUNCTION [AppScript].[funcDataItemId](@DataNameSpace [AppGeneral].[uddtNameSpacePath])
-- Takes an DataNameSpace and gets the DataItemId
-- Temporal Data NOT Supported
RETURNS UniqueIdentifier As 
BEGIN
Declare	@Result UniqueIdentifier = null

;With [Data] As (
	Select	[MemberName] As [DataItemMember],
			[QualifiedName] As [NameSpace],
			[Level],
			[IsBase]
	From	[AppGeneral].[funcParseName](@DataNameSpace)),
[Search] As (
	Select	N.[DataItemId],
			N.[ParentItemId],
			N.[DataItemMember],
			D.[Level],
			D.[IsBase]
	From	[Data] D
			Inner Join [AppScript].[DataItem] N
			On	D.[DataItemMember] = N.[DataItemMember] And
				N.[ParentItemId] is Null And
				D.[Level] = 1
	Union All
	Select	N.[DataItemId],
			N.[ParentItemId],
			N.[DataItemMember],
			D.[Level],
			D.[IsBase]
	From	[Search] S
			Inner Join [Data] D
			On	S.[Level] + 1 = D.[Level]
			Inner Join [AppScript].[DataItem] N
			On	S.[DataItemId] = N.[ParentItemId] And
				D.[DataItemMember] = N.[DataItemMember])
Select	@Result = [DataItemId]
From	[Search]
Where	[IsBase] = 1

Return @Result
END
