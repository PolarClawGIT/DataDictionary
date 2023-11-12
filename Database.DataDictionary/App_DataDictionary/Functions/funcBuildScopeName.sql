CREATE FUNCTION [App_DataDictionary].[funcBuildScopeName](@ScopeId Int)
RETURNS TABLE AS RETURN (
With [Data] As (
	Select	[ScopeId],
			[ScopeParentId],
			Convert(NVarChar(450),[ScopedElementName]) As [ScopeName]
	From	[App_DataDictionary].[ApplicationScope]
	Where	[ScopeId] = @ScopeId
	Union All
	Select	D.[ScopeId],
			S.[ScopeParentId],
			Convert(NVarChar(450),FormatMessage('%s.%s',S.[ScopedElementName],D.[ScopeName])) As [ScopeName]
	From	[Data] D
			Inner Join [App_DataDictionary].[ApplicationScope] S
			On	D.[ScopeParentId] = S.[ScopeId])

Select	[ScopeId],
		[ScopeName]
From	[Data]
Where	[ScopeParentId] is Null)
GO