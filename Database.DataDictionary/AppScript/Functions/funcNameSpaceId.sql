CREATE FUNCTION [AppScript].[funcNameSpaceId] (@NameSpace [AppGeneral].[dtNameSpacePath])
-- Takes an Scripting NameSpace and gets the NameSpaceID
-- Temporal Data NOT Supported
RETURNS UniqueIdentifier As 
BEGIN
Declare	@Result UniqueIdentifier = null

;With [Data] As (
	Select	[MemberName] As [NameSpaceMember],
			[QualifiedName] As [NameSpace],
			[Level],
			[IsBase]
	From	[AppGeneral].[funcParseName](@NameSpace)),
[Search] As (
	Select	N.[NameSpaceId],
			N.[ParentNameSpaceId],
			N.[NameSpaceMember],
			D.[Level],
			D.[IsBase]
	From	[Data] D
			Inner Join [AppScript].[ScriptingNameSpace] N
			On	D.[NameSpaceMember] = N.[NameSpaceMember] And
				N.[ParentNameSpaceId] is Null And
				D.[Level] = 1
	Union All
	Select	N.[NameSpaceId],
			N.[ParentNameSpaceId],
			N.[NameSpaceMember],
			D.[Level],
			D.[IsBase]
	From	[Search] S
			Inner Join [Data] D
			On	S.[Level] + 1 = D.[Level]
			Inner Join [AppScript].[ScriptingNameSpace] N
			On	S.[NameSpaceId] = N.[ParentNameSpaceId] And
				D.[NameSpaceMember] = N.[NameSpaceMember])
Select	@Result = [NameSpaceId]
From	[Search]
Where	[IsBase] = 1

Return @Result
END
