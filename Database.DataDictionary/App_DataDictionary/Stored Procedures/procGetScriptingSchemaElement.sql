CREATE PROCEDURE [App_DataDictionary].[procGetScriptingSchemaElement]
		@ElementId UniqueIdentifier = Null,
		@SchemaId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ScriptingSchemaElement.
*/
Select	D.[ElementId],
		D.[SchemaId],
		D.[ScopeId],
		D.[ColumnName],
		D.[DataName],
		D.[DataType],
		D.[DataNillable],
		D.[AsElement],
		Convert(Bit,IIF(D.[AsElement] is null,null,IIF(D.[AsElement]=1,0,1))) As [AsAttribute],
		D.[DataAsText],
		D.[DataAsCData],
		D.[DataAsXml]
From	[App_DataDictionary].[ScriptingSchemaElement] D
		Outer Apply [App_DataDictionary].[funcGetScopeName](D.[ScopeId]) C
Where	(@ElementId is Null or @ElementId = D.[ElementId]) And
		(@SchemaId is Null or @SchemaId = D.[SchemaId]) 
GO