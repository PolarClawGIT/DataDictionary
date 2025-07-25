CREATE FUNCTION [AppScript].[funcDataObjectId](@DataNameSpace [AppGeneral].[uddtNameSpacePath])
-- Takes an DataNameSpace and gets the DataObjectId
-- Temporal Data NOT Supported
RETURNS UniqueIdentifier As 
BEGIN
Declare	@Result UniqueIdentifier = null

;With [Data] As (
	Select	[MemberName] As [DataMember],
			[QualifiedName] As [NameSpace],
			[Level],
			[IsBase]
	From	[AppGeneral].[funcParseName](@DataNameSpace)),
[Search] As (
	Select	N.[DataObjectId],
			N.[ParentObjectId],
			N.[DataMember],
			D.[Level],
			D.[IsBase]
	From	[Data] D
			Inner Join [AppScript].[DataObject] N
			On	D.[DataMember] = N.[DataMember] And
				N.[ParentObjectId] is Null And
				D.[Level] = 1
	Union All
	Select	N.[DataObjectId],
			N.[ParentObjectId],
			N.[DataMember],
			D.[Level],
			D.[IsBase]
	From	[Search] S
			Inner Join [Data] D
			On	S.[Level] + 1 = D.[Level]
			Inner Join [AppScript].[DataObject] N
			On	S.[DataObjectId] = N.[ParentObjectId] And
				D.[DataMember] = N.[DataMember])
Select	@Result = [DataObjectId]
From	[Search]
Where	[IsBase] = 1

Return @Result
END
