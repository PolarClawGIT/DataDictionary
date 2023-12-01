CREATE PROCEDURE [App_DataDictionary].[procGetDatabaseSchema]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@DatabaseName SysName = Null,
		@SchemaName SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseSchema.
*/
Select	S.[CatalogId],
		S.[DatabaseName],
		D.[SchemaName],
		C.[ScopeName]
From	[App_DataDictionary].[DatabaseSchema] D
		Inner Join [App_DataDictionary].[DatabaseSchema_AK] S
		On	D.[SchemaId] = S.[SchemaId]
		Left Join [App_DataDictionary].[ModelCatalog] A
		On	D.[CatalogId] = A.[CatalogId]
		Outer Apply [App_DataDictionary].[funcGetScopeName](D.[ScopeId]) C
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@CatalogId is Null or @CatalogId = S.[CatalogId]) And
		(@DatabaseName is Null or @DatabaseName = S.[DatabaseName]) And
		(@SchemaName is Null or @SchemaName = D.[SchemaName])
GO