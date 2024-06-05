CREATE PROCEDURE [App_DataDictionary].[procGetScriptingSchemaElement]
		@SchemaId UniqueIdentifier = Null,
		@ElementId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ScriptingSchemaElement.
*/
Select	D.[ElementId],
		D.[SchemaId],
		D.[ScopeName],
		D.[ColumnName],
		D.[ElementName],
		D.[ElementType],
		D.[ElementNillable],
		D.[AsElement],
		Convert(Bit,IIF(D.[AsElement] is null,null,IIF(D.[AsElement]=1,0,1))) As [AsAttribute],
		D.[DataAsText],
		D.[DataAsCData],
		D.[DataAsXml]
From	[App_DataDictionary].[ScriptingSchemaElement] D
Where	(@ElementId is Null or @ElementId = D.[ElementId]) And
		(@SchemaId is Null or @SchemaId = D.[SchemaId]) 
GO