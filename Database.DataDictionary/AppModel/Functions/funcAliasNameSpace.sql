CREATE FUNCTION [AppModel].[funcAliasNameSpace](@AliasId UniqueIdentifier)
-- This takes the Alias and rebuilds them into a Alias NameSpace.
-- NameSpace is qualified by square brackets and delimited by periods.
-- Temporal Data NOT Supported
RETURNS [App_DataDictionary].[typeNameSpacePath] as 
BEGIN
	Declare @Result [App_DataDictionary].[typeNameSpacePath] = null

	;With [Data] As (
	Select	[AliasId],
			NullIf([ParentAliasId], [AliasId]) As [ParentAliasId],
			Convert(NVarChar(Max),
				FormatMessage('[%s]',[AliasMember])) As [AliasNameSpace]
	From	[AppModel].[AliasHierarchy]
	Where	[AliasId] = @AliasId
	Union All
	Select	D.[AliasId],
			NullIf(P.[ParentAliasId], D.[AliasId]) As [ParentAliasId],
			Convert(NVarChar(Max),
				FormatMessage('[%s].%s',P.[AliasMember],D.[AliasNameSpace])) As [AliasNameSpace]
	From	[Data] D
			Inner Join [AppModel].[AliasHierarchy] P
			On	D.[ParentAliasId] = P.[AliasId])
Select	@Result = [AliasNameSpace]
From	[Data]
Where	[ParentAliasId] is Null

Return	@Result
END
GO