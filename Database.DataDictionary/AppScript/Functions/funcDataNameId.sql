CREATE FUNCTION [AppScript].[funcDataNameId](@NameSpace [AppGeneral].[uddtNameSpacePath])
-- Takes an Data Object NameSpace and gets the DataNameId
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
	Select	N.[DataNameId],
			N.[ParentNameId],
			N.[DataMember],
			D.[Level],
			D.[IsBase]
	From	[Data] D
			Inner Join [AppScript].[DataObjectName] N
			On	D.[DataMember] = N.[DataMember] And
				N.[ParentNameId] is Null And
				D.[Level] = 1
	Union All
	Select	N.[DataNameId],
			N.[ParentNameId],
			N.[DataMember],
			D.[Level],
			D.[IsBase]
	From	[Search] S
			Inner Join [Data] D
			On	S.[Level] + 1 = D.[Level]
			Inner Join [AppScript].[DataObjectName] N
			On	S.[DataNameId] = N.[ParentNameId] And
				D.[DataMember] = N.[DataMember])
Select	@Result = [DataNameId]
From	[Search]
Where	[IsBase] = 1

Return @Result
END
