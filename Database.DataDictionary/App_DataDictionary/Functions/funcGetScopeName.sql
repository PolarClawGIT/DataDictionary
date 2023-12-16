CREATE FUNCTION [App_DataDictionary].[funcGetScopeName](@ScopeId Int)
RETURNS TABLE AS RETURN (
With [Data] As (
	Select	[ScopeId],
			NullIf([ScopeParentId],[ScopeId]) As [ScopeParentId],
			Convert(NVarChar(450),[ScopeElement]) As [ScopeName],
			Convert(Int,1) As [Level]
	From	[App_DataDictionary].[ApplicationScope]
	Where	[ScopeId] = @ScopeId
	Union All
	Select	D.[ScopeId],
			NullIf(S.[ScopeParentId],D.[ScopeId]) as [ScopeParentId],
			Convert(NVarChar(450),FormatMessage('%s.%s',S.[ScopeElement],D.[ScopeName])) As [ScopeName],
			D.[Level] + 1 As [Level]
	From	[Data] D
			Inner Join [App_DataDictionary].[ApplicationScope] S
			On	D.[ScopeParentId] = S.[ScopeId])
Select	[ScopeName],
		[Level]
From	[Data]
Where	[ScopeParentId] is Null)
GO
