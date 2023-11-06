CREATE PROCEDURE [App_DataDictionary].[procGetDatabaseCatalog]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@DatabaseName SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseCatalog.
*/
Select	D.[CatalogId],
		D.[CatalogTitle],
		D.[CatalogDescription],
		D.[SourceServerName],
		D.[SourceDatabaseName],
		D.[SourceDate]
From	[App_DataDictionary].[DatabaseCatalog] D
		Left Join [App_DataDictionary].[ModelCatalog] A
		On	D.[CatalogId] = A.[CatalogId]
		Inner Join [App_DataDictionary].[ModelScope] P
		On	D.[ScopeId] = P.[ScopeId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@CatalogId is Null or @CatalogId = D.[CatalogId]) And
		(@DatabaseName is Null or @DatabaseName = D.[SourceDatabaseName])
GO