CREATE PROCEDURE [App_DataDictionary].[procGetDatabaseTable]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@DatabaseName SysName = Null,
		@SchemaName SysName = Null,
		@TableName SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseTable.
*/
Select	D.[CatalogId],
		C.[SourceDatabaseName] As [DatabaseName],
		D.[SchemaName],
		D.[TableName],
		D.[TableType]
From	[App_DataDictionary].[DatabaseTable] D
		Left Join [App_DataDictionary].[ModelCatalog] A
		On	D.[CatalogId] = A.[CatalogId]
		Inner Join [App_DataDictionary].[DatabaseCatalog] C
		On	D.[CatalogId] = C.[CatalogId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@CatalogId is Null or @CatalogId = D.[CatalogId]) And
		(@DatabaseName is Null or @DatabaseName = C.[SourceDatabaseName]) And
		(@SchemaName is Null or @SchemaName = D.[SchemaName]) And
		(@TableName is Null or @TableName = D.[TableName])
GO