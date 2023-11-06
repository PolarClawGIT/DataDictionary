﻿CREATE VIEW [App_DataDictionary].[ModelScope] As
With [Data] As (
	Select	[ScopeId],
			Convert(NVarChar(450),[ScopedElementName]) As [ScopeName],
			Convert(Int,0) As [Level]
	From	[App_DataDictionary].[ApplicationScope]
	Where	[ScopeParentId] is Null
	Union All
	Select	S.[ScopeId],
			Convert(NVarChar(450),FormatMessage('%s.%s',D.[ScopeName],S.[ScopedElementName])) As [ScopeName],
			D.[Level] +1 As [Level]
	From	[Data] D
			Inner Join [App_DataDictionary].[ApplicationScope] S
			On	D.[ScopeId] = S.[ScopeParentId])
Select	[ScopeId],
		[ScopeName],
		[Level]
From	[Data]
GO