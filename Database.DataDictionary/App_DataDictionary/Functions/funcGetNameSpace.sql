CREATE FUNCTION [App_DataDictionary].[funcGetNameSpace] (@NameSpaceId UniqueIdentifier)
-- This takes the NameSpace Member and rebuilds them into a fully Qualified NameSpace.
-- NameSpace is qualified by square brackets and delimited by periods.
-- This is based on the SQL Qualified Naming of objects.
RETURNS TABLE AS RETURN (
With [Data] As (
	Select	[NameSpaceId],
			NullIf([ParentNameSpaceId], [NameSpaceId]) As [ParentNameSpaceId],
			Convert(NVarChar(Max),
				FormatMessage('[%s]',[NameSpaceMember])) As [NameSpace],
			Convert(NVarChar(Max), Null) As [ParentNameSpace],
			[NameSpaceMember]
	From	[App_DataDictionary].[ModelNameSpace]
	Where	[NameSpaceId] = @NameSpaceId
	Union All
	Select	D.[NameSpaceId],
			NullIf(P.[ParentNameSpaceId], D.[NameSpaceId]) As [ParentNameSpaceId],
			Convert(NVarChar(Max),
				FormatMessage('[%s].%s',P.[NameSpaceMember],D.[NameSpace])) As [NameSpace],
			Convert(NVarChar(Max),
				IIF(D.[ParentNameSpace] is Null,
				FormatMessage('[%s]',P.[NameSpaceMember]),
				FormatMessage('[%s].%s',P.[NameSpaceMember], D.[ParentNameSpace])))
				As [ParentNameSpace],
			D.[NameSpaceMember]
	From	[Data] D
			Inner Join [App_DataDictionary].[ModelNameSpace] P
			On	D.[ParentNameSpaceId] = P.[NameSpaceId])
Select	[NameSpaceId],
		[NameSpaceMember],
		[NameSpace],
		[ParentNameSpace]
From	[Data]
Where	[ParentNameSpaceId] is Null)
GO
