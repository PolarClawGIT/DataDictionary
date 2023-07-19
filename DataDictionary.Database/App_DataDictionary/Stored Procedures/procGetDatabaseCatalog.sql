CREATE PROCEDURE [App_DataDictionary].[procGetDatabaseCatalog]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@CatalogName SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseCatalog.
*/
Select	A.[ModelId],
		D.[CatalogId],
		D.[CatalogName],
		D.[SourceServerName],
		D.[ModfiedBy],
		D.[SysStart]
From	[App_DataDictionary].[DatabaseCatalog] D
		Inner Join [App_DataDictionary].[ApplicationCatalog] A
		On	D.[CatalogId] = A.[CatalogId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@CatalogId is Null or @CatalogId = D.[CatalogId]) And
		(@CatalogName is Null or @CatalogName = D.[CatalogName])
GO