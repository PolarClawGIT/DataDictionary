CREATE FUNCTION [AppModel].[funcGetNameSpaceById] (@NameSpaceId UniqueIdentifier)
-- This takes the NameSpace Member and rebuilds them into a fully Qualified NameSpace.
-- NameSpace is qualified by square brackets and delimited by periods.
--
-- This function contains a CTE over a Temporal table but only supports the current state.
-- Use View [AppModel].[NameSpaceHs] for history.
RETURNS TABLE AS RETURN (
With [Data] As (
	Select	[NameSpaceId],
			NullIf([ParentNameSpaceId], [NameSpaceId]) As [ParentNameSpaceId],
			Convert(NVarChar(Max),
				FormatMessage('[%s]',[MemberName])) As [NameSpace],
			Convert(NVarChar(Max), Null) As [ParentNameSpace],
			[MemberName]
	From	[AppModel].[NameSpaceHierarchy]
	Where	[NameSpaceId] = @NameSpaceId
	Union All
	Select	D.[NameSpaceId],
			NullIf(P.[ParentNameSpaceId], D.[NameSpaceId]) As [ParentNameSpaceId],
			Convert(NVarChar(Max),
				FormatMessage('[%s].%s',P.[MemberName],D.[NameSpace])) As [NameSpace],
			Convert(NVarChar(Max),
				IIF(D.[ParentNameSpace] is Null,
				FormatMessage('[%s]',P.[MemberName]),
				FormatMessage('[%s].%s',P.[MemberName], D.[ParentNameSpace])))
				As [ParentNameSpace],
			D.[MemberName]
	From	[Data] D
			Inner Join [AppModel].[NameSpaceHierarchy] P
			On	D.[ParentNameSpaceId] = P.[NameSpaceId])
Select	[NameSpaceId],
		[MemberName],
		[NameSpace]
From	[Data]
Where	[ParentNameSpaceId] is Null)
GO
/* Alternate implementation that allows Temporal values
;With [Base] As (
	Select	[NameSpaceId],
			NullIf([ParentNameSpaceId], [NameSpaceId]) As [ParentNameSpaceId],
			Convert(NVarChar(Max),
				FormatMessage('[%s]',[MemberName])) As [NameSpace],
			Convert(NVarChar(Max), Null) As [ParentNameSpace],
			[MemberName]
	From	[AppModel].[NameSpaceHierarchy] --For System_Time As of @AsOfUtcDate
	Where	[NameSpaceId] = @NameSpaceId And
			@AsOfUtcDate is Null
	Union All
	Select	D.[NameSpaceId],
			NullIf(P.[ParentNameSpaceId], D.[NameSpaceId]) As [ParentNameSpaceId],
			Convert(NVarChar(Max),
				FormatMessage('[%s].%s',P.[MemberName],D.[NameSpace])) As [NameSpace],
			Convert(NVarChar(Max),
				IIF(D.[ParentNameSpace] is Null,
				FormatMessage('[%s]',P.[MemberName]),
				FormatMessage('[%s].%s',P.[MemberName], D.[ParentNameSpace])))
				As [ParentNameSpace],
			D.[MemberName]
	From	[Base] D
			Inner Join [AppModel].[NameSpaceHierarchy] P
			On	D.[ParentNameSpaceId] = P.[NameSpaceId]
	Where	@AsOfUtcDate is Null),
[AsOf] As (
	Select	[NameSpaceId],
			NullIf([ParentNameSpaceId], [NameSpaceId]) As [ParentNameSpaceId],
			Convert(NVarChar(Max),
				FormatMessage('[%s]',[MemberName])) As [NameSpace],
			Convert(NVarChar(Max), Null) As [ParentNameSpace],
			[MemberName]
	From	[AppModel].[NameSpaceHierarchy] For System_Time As of @AsOfUtcDate
	Where	[NameSpaceId] = @NameSpaceId And
			@AsOfUtcDate is Not Null
	Union All
	Select	D.[NameSpaceId],
			NullIf(P.[ParentNameSpaceId], D.[NameSpaceId]) As [ParentNameSpaceId],
			Convert(NVarChar(Max),
				FormatMessage('[%s].%s',P.[MemberName],D.[NameSpace])) As [NameSpace],
			Convert(NVarChar(Max),
				IIF(D.[ParentNameSpace] is Null,
				FormatMessage('[%s]',P.[MemberName]),
				FormatMessage('[%s].%s',P.[MemberName], D.[ParentNameSpace])))
				As [ParentNameSpace],
			D.[MemberName]
	From	[AsOf] D
			Inner Join [AppModel].[NameSpaceHierarchy] For System_Time As of @AsOfUtcDate P
			On	D.[ParentNameSpaceId] = P.[NameSpaceId]
	Where	@AsOfUtcDate is Not Null)
Select	[NameSpaceId],
		[MemberName],
		[NameSpace]
From	[Base]
Where	[ParentNameSpaceId] is Null
Union
Select	[NameSpaceId],
		[MemberName],
		[NameSpace]
From	[AsOf]
Where	[ParentNameSpaceId] is Null
*/