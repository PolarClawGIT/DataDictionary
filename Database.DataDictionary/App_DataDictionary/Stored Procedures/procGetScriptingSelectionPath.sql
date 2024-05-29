CREATE PROCEDURE [App_DataDictionary].[procGetScriptingSelectionPath]
		@ModelId UniqueIdentifier = Null,
		@SelectionId UniqueIdentifier = Null,
		@SelectionPath [App_DataDictionary].[typeNameSpacePath] = null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ScriptingSelectionPath.
*/
Select	D.[SelectionId],
		D.[ScopeName],
		N.[NameSpace] As [SelectionPath]
From	[App_DataDictionary].[ScriptingSelectionPath] D
		Cross Apply [App_DataDictionary].[funcGetNameSpace](D.[NameSpaceId]) N
		Left Join [App_DataDictionary].[ModelScripting] A
		On	D.[SelectionId] = A.[SelectionId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@SelectionId is Null or @SelectionId = D.[SelectionId]) And
		(@SelectionPath is Null or @SelectionPath = N.[NameSpace])
GO