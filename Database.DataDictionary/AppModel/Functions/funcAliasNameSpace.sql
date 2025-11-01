CREATE FUNCTION [AppModel].[funcAliasNameSpace](@AliasId UniqueIdentifier)
-- This takes the Alias and rebuilds them into a Alias NameSpace.
-- NameSpace is qualified by square brackets and delimited by periods.
-- Temporal Data NOT Supported
RETURNS [AppGeneral].[uddtPath] as 
BEGIN
	Declare @Result [AppGeneral].[uddtPath] = null

	;With [Data] As (
	Select	[AliasId],
			NullIf([ParentAliasId], [AliasId]) As [ParentAliasId],
			[AppGeneral].[funcCreatePath]([AliasMember], Null) As [AliasNameSpace]
	From	[AppModel].[AliasNameSpace]
	Where	[AliasId] = @AliasId
	Union All
	Select	D.[AliasId],
			NullIf(P.[ParentAliasId], D.[AliasId]) As [ParentAliasId],
			[AppGeneral].[funcCreatePath](P.[AliasMember], D.[AliasNameSpace]) As [AliasNameSpace]
	From	[Data] D
			Inner Join [AppModel].[AliasNameSpace] P
			On	D.[ParentAliasId] = P.[AliasId])
Select	@Result = [AliasNameSpace]
From	[Data]
Where	[ParentAliasId] is Null

Return	@Result
END
GO