CREATE PROCEDURE [App_DataDictionary].[procGetScriptingSelection]
		@ModelId UniqueIdentifier = Null,
		@SelectionId UniqueIdentifier = Null,
		@SelectionTitle [App_DataDictionary].[typeTitle] = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ScriptingSelection.
*/
Select	D.[SelectionId],
		D.[SelectionTitle],
		D.[SelectionDescription],
		A.[SchemaId],
		A.[TransformId]
From	[App_DataDictionary].[ScriptingSelection] D
		Left Join [App_DataDictionary].[ModelScripting] A
		On	D.[SelectionId] = A.[SelectionId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@SelectionId is Null or @SelectionId = D.[SelectionId]) And
		(@SelectionTitle is Null or @SelectionTitle = D.[SelectionDescription])
GO