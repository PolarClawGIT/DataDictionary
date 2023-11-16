CREATE PROCEDURE [App_DataDictionary].[procGetApplicationScope]
As
Select	S.[ScopeId],
		F.[ScopeName],
		S.[ScopeDescription]
From	[App_DataDictionary].[ApplicationScope] S
		Cross Apply [App_DataDictionary].[funcGetScopeName](S.[ScopeId]) F
GO