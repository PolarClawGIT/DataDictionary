CREATE PROCEDURE [App_DataDictionary].[procGetApplicationTransform]
		@TransformId UniqueIdentifier = Null,
		@TransformTitle [App_DataDictionary].[typeTitle] = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ApplicationTransform.
*/
Select	D.[TransformId],
		D.[TransformTitle],
		D.[TransformDescription],
		C.[ScopeName],
		D.[AsText],
		Convert(Bit,IIF(D.[AsText] is null,null,IIF(D.[AsText]=1,0,1))) As [AsXml],
		D.[TransformScript]
From	[App_DataDictionary].[ApplicationTransform] D
		Outer Apply [App_DataDictionary].[funcGetScopeName](D.[ScopeId]) C
Where	(@TransformId is Null or @TransformId = D.[TransformId]) And
		(@TransformTitle is Null or @TransformTitle = D.[TransformDescription])
GO