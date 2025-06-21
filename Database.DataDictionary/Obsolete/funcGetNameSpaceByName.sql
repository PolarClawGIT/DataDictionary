CREATE FUNCTION [AppModel].[funcGetNameSpaceByName] (@NameSpace [AppGeneral].[dtNameSpacePath])
-- This takes a value and returns the ID's for all matching NameSpace & the parents of that NameSpace.
-- NameSpace is qualified by square brackets and delimited by periods.
--
-- This search the NameSpace Hierarchy based on MemberName(s) of the value passed.
-- This results in an Index search that is expected to be faster then searching for the whole value.
--
-- This function contains a CTE over a Temporal table but only supports the current state.
-- Use View [AppModel].[NameSpaceHs] for history.
RETURNS TABLE AS RETURN (
With [Data] As (
	Select	[MemberName],
			[QualifiedName] As [NameSpace],
			[Level],
			[IsBase]
	From	[AppGeneral].[funcParseName](@NameSpace)),
[Search] As (
	Select	N.[ModelId],
			N.[NameSpaceId],
			N.[ParentNameSpaceId],
			N.[MemberName],
			Convert(NVarChar(Max), FormatMessage('[%s]', N.[MemberName])) As [NameSpace],
			D.[Level],
			D.[IsBase]
	From	[Data] D
			Inner Join [AppModel].[NameSpaceHierarchy] N
			On	D.[MemberName] = N.[MemberName] And
				N.[ParentNameSpaceId] is Null And
				D.[Level] = 1
	Union All
	Select	N.[ModelId],
			N.[NameSpaceId],
			N.[ParentNameSpaceId],
			N.[MemberName],
			Convert(NVarChar(Max), FormatMessage('%s.[%s]',S.[NameSpace], N.[MemberName])) As [NameSpace],
			D.[Level],
			D.[IsBase]
	From	[Search] S
			Inner Join [Data] D
			On	S.[Level] + 1 = D.[Level]
			Inner Join [AppModel].[NameSpaceHierarchy] N
			On	S.[ModelId] = N.[ModelId] And
				S.[NameSpaceId] = N.[ParentNameSpaceId] And
				D.[MemberName] = N.[MemberName])
Select	[ModelId],
		[NameSpaceId],
		[ParentNameSpaceId],
		[NameSpace],
		[MemberName]
From	[Search]
Where	[IsBase] = 1)
