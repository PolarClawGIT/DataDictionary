CREATE FUNCTION [AppModel].[funcAliasId] (@AliasNameSpace [App_DataDictionary].[typeNameSpacePath])
-- Takes an AliasNameSpace and gets the AliasID
-- Temporal Data NOT Supported
RETURNS UniqueIdentifier As 
BEGIN
Declare	@Result UniqueIdentifier = null

;With [Data] As (
	Select	[MemberName] As [AliasMember],
			[QualifiedName] As [NameSpace],
			[Level],
			[IsBase]
	From	[AppModel].[funcParseName](@AliasNameSpace)),
[Search] As (
	Select	N.[AliasId],
			N.[ParentAliasId],
			N.[AliasMember],
			D.[Level],
			D.[IsBase]
	From	[Data] D
			Inner Join [AppModel].[AliasHierarchy] N
			On	D.[AliasMember] = N.[AliasMember] And
				N.[ParentAliasId] is Null And
				D.[Level] = 1
	Union All
	Select	N.[AliasId],
			N.[ParentAliasId],
			N.[AliasMember],
			D.[Level],
			D.[IsBase]
	From	[Search] S
			Inner Join [Data] D
			On	S.[Level] + 1 = D.[Level]
			Inner Join [AppModel].[AliasHierarchy] N
			On	S.[AliasId] = N.[ParentAliasId] And
				D.[AliasMember] = N.[AliasMember])
Select	@Result = [AliasId]
From	[Search]
Where	[IsBase] = 1

Return @Result
END
