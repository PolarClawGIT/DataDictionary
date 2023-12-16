CREATE FUNCTION [App_DataDictionary].[funcGetAliasName](@AliasId UniqueIdentifier)
-- This takes the Alias Elements and rebuilds them into a fully Qualified Alias Name.
-- Alias Name is qualified by square brackets and delimited by periods.
-- This is based on the SQL Qualified Naming of objects.
RETURNS TABLE AS RETURN (
With [Data] As (
	Select	[AliasId],
			NullIf([ParentAliasId], [AliasId]) As [ParentAliasId],
			Convert(NVarChar(Max),
				FormatMessage('[%s]',[AliasElement])) As [AliasName],
			Convert(NVarChar(Max), Null) As [ParentAliasName],
			[AliasElement]
	From	[App_DataDictionary].[DomainAlias]
	Where	[AliasId] = @AliasId
	Union All
	Select	D.[AliasId],
			NullIf(P.[ParentAliasId], D.[AliasId]) As [ParentAliasId],
			Convert(NVarChar(Max),
				FormatMessage('[%s].%s',P.[AliasElement],D.[AliasName])) As [AliasName],
			Convert(NVarChar(Max),
				IIF(D.[ParentAliasName] is Null,
				FormatMessage('[%s]',P.[AliasElement]),
				FormatMessage('[%s].%s',P.[AliasElement], D.[ParentAliasName])))
				As [ParentAliasName],
			D.[AliasElement]
	From	[Data] D
			Inner Join [App_DataDictionary].[DomainAlias] P
			On	D.[ParentAliasId] = P.[AliasId])
Select	[AliasId],
		[AliasName],
		[ParentAliasName],
		[AliasElement]
From	[Data]
Where	[ParentAliasId] is Null)
GO
