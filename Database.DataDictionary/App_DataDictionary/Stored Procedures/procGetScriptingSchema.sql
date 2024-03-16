CREATE PROCEDURE [App_DataDictionary].[procGetScriptingSchema]
		@SchemaId UniqueIdentifier = Null,
		@SchemaTitle [App_DataDictionary].[typeTitle] = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ScriptingSchema.
*/
Select	D.[SchemaId],
		D.[SchemaTitle],
		D.[SchemaDescription]
From	[App_DataDictionary].[ScriptingSchema] D
Where	(@SchemaId is Null or @SchemaId = D.[SchemaId]) And
		(@SchemaTitle is Null or @SchemaTitle = D.[SchemaDescription])
GO