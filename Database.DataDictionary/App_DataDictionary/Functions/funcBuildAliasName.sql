CREATE FUNCTION [App_DataDictionary].[funcBuildAliasName](@AliasElementId UniqueIdentifier)
-- This takes the Alias Elements and rebuilds them into a fully Qualified Alias Name.
-- Alias Name is qualified by square brackets and delimited by periods.
-- This is based on the SQL Qualified Naming of objects.
RETURNS TABLE AS RETURN (
	With [Data] As (
		Select	[AliasElementId],
				[AliasElementParentId],
				Convert(NVarChar(Max),
					FormatMessage('[%s]',[AliasElementName])) As [AliasName],
				Convert(NVarChar(Max), Null) As [ParentAliasName],
				[AliasElementName],
				S.[ScopeName]
		From	[App_DataDictionary].[DomainAliasElement]
				Outer Apply [App_DataDictionary].[funcBuildScopeName]([ScopeId]) S
		Where	[AliasElementId] = @AliasElementId
		Union All
		Select	D.[AliasElementId],
				E.[AliasElementParentId],
				Convert(NVarChar(Max),
					FormatMessage('[%s].%s',E.[AliasElementName],D.[AliasName])) As [AliasName],
				Convert(NVarChar(Max),
					IIF(D.[ParentAliasName] is Null,
					FormatMessage('[%s]',E.[AliasElementName]),
					FormatMessage('[%s].%s',E.[AliasElementName], D.[ParentAliasName])))
					As [ParentAliasName],
				D.[AliasElementName],
				D.[ScopeName]
		From	[Data] D
				Inner Join [App_DataDictionary].[DomainAliasElement] E
				On	D.[AliasElementParentId] = E.[AliasElementId])
Select	[AliasElementId],
		[AliasName],
		[ParentAliasName],
		[AliasElementName]
		[ScopeName]
From	[Data]
Where	[AliasElementParentId] is Null)
GO