CREATE PROCEDURE [App_DataDictionary].[procGetApplicationScope]
		@ScopeId Int = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ApplicationScope.
*/
Select	S.[ScopeId],
		M.[ScopeName],
		S.[ScopeDescription]
From	[App_DataDictionary].[ApplicationScope] S
		Inner Join [App_DataDictionary].[ModelScope] M
		On	S.[ScopeId] = M.[ScopeId]
Where	(@ScopeId is Null Or @ScopeId = S.[ScopeId]) 
GO