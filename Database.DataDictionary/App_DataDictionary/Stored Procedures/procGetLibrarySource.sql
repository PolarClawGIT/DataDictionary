CREATE PROCEDURE [App_DataDictionary].[procGetLibrarySource]
		@ModelId UniqueIdentifier = Null,
		@LibraryId UniqueIdentifier = Null,
		@AssemblyName SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on LibrarySource.
*/
Select	D.[LibraryId],
		D.[LibraryTitle],
		D.[LibraryDescription],
		D.[AssemblyName],
		D.[SourceFile],
		D.[SourceDate]
From	[App_DataDictionary].[LibrarySource] D
		Left Join [App_DataDictionary].[ModelLibrary] A
		On	D.[LibraryId] = A.[LibraryId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@LibraryId is Null or @LibraryId = D.[LibraryId]) And
		(@AssemblyName is Null or @AssemblyName = D.[AssemblyName])
GO