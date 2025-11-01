CREATE FUNCTION [AppScript].[funcObjectNameId](@NameSpace [AppGeneral].[uddtPath])
-- Takes an Data Object NameSpace and gets the ObjectNameId
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
	Select	N.[ObjectNameId],
			N.[ParentNameId],
			N.[ObjectMember],
			D.[Level],
			D.[IsBase]
	From	[Data] D
			Inner Join [AppScript].[DataObjectName] N
			On	D.[DataMember] = N.[ObjectMember] And
				N.[ParentNameId] is Null And
				D.[Level] = 1
	Union All
	Select	N.[ObjectNameId],
			N.[ParentNameId],
			N.[ObjectMember],
			D.[Level],
			D.[IsBase]
	From	[Search] S
			Inner Join [Data] D
			On	S.[Level] + 1 = D.[Level]
			Inner Join [AppScript].[DataObjectName] N
			On	S.[ObjectNameId] = N.[ParentNameId] And
				D.[DataMember] = N.[ObjectMember])
Select	@Result = [ObjectNameId]
From	[Search]
Where	[IsBase] = 1

Return @Result
END
