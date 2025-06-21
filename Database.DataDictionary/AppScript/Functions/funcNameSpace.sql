CREATE FUNCTION [AppScript].[funcNameSpace](@NameSpaceId UniqueIdentifier)
-- This takes the NameSpaceId and rebuilds them into a Scripting NameSpace.
-- NameSpace is qualified by square brackets and delimited by periods.
-- Temporal Data NOT Supported
RETURNS [AppGeneral].[dtNameSpacePath] as 
BEGIN
	Declare @Result [AppGeneral].[dtNameSpacePath] = null

	;With [Data] As (
	Select	[NameSpaceId],
			NullIf([ParentNameSpaceId], [NameSpaceId]) As [ParentNameSpaceId],
			Convert(NVarChar(Max),
				FormatMessage('[%s]',[NameSpaceMember])) As [AliasNameSpace]
	From	[AppScript].[ScriptingNameSpace]
	Where	[NameSpaceId] = @NameSpaceId
	Union All
	Select	D.[NameSpaceId],
			NullIf(P.[ParentNameSpaceId], D.[NameSpaceId]) As [ParentNameSpaceId],
			Convert(NVarChar(Max),
				FormatMessage('[%s].%s',P.[NameSpaceMember],D.[AliasNameSpace])) As [AliasNameSpace]
	From	[Data] D
			Inner Join [AppScript].[ScriptingNameSpace] P
			On	D.[ParentNameSpaceId] = P.[NameSpaceId])
Select	@Result = [AliasNameSpace]
From	[Data]
Where	[ParentNameSpaceId] is Null

Return	@Result
END
GO