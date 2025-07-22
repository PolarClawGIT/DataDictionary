/* Obsolete
CREATE FUNCTION [AppScript].[funcNameSpace](@NameSpaceId UniqueIdentifier)
-- This takes the NameSpaceId and rebuilds them into a Scripting NameSpace.
-- NameSpace is qualified by square brackets and delimited by periods.
-- Temporal Data NOT Supported
RETURNS [AppGeneral].[uddtNameSpacePath] as 
BEGIN
	Declare @Result [AppGeneral].[uddtNameSpacePath] = null

	;With [Data] As (
	Select	[NameSpaceId],
			NullIf([ParentNameSpaceId], [NameSpaceId]) As [ParentNameSpaceId],
			Convert(NVarChar(Max),
				FormatMessage('[%s]',[MemberName])) As [NameSpace]
	From	[AppScript].[ScriptingNameSpace]
	Where	[NameSpaceId] = @NameSpaceId
	Union All
	Select	D.[NameSpaceId],
			NullIf(P.[ParentNameSpaceId], D.[NameSpaceId]) As [ParentNameSpaceId],
			Convert(NVarChar(Max),
				FormatMessage('[%s].%s',P.[MemberName],D.[NameSpace])) As [NameSpace]
	From	[Data] D
			Inner Join [AppScript].[ScriptingNameSpace] P
			On	D.[ParentNameSpaceId] = P.[NameSpaceId])
Select	@Result = [NameSpace]
From	[Data]
Where	[ParentNameSpaceId] is Null

Return	@Result
END
GO
*/