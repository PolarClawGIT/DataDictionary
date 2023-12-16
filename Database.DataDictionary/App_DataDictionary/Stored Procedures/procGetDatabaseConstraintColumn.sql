CREATE PROCEDURE [App_DataDictionary].[procGetDatabaseConstraintColumn]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@DatabaseName SysName = Null,
		@SchemaName SysName = Null,
		@ConstraintName SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseConstraintColumn.
*/
Select	T.[CatalogId],
		D.[ConstraintColumnId],
		T.[DatabaseName],
		T.[SchemaName],
		T.[TableName],
		T.[ConstraintName],
		C.[ColumnName],
		D.[OrdinalPosition],
		D.[ReferenceSchemaName],
		D.[ReferenceTableName],
		D.[ReferenceColumnName]
From	[App_DataDictionary].[DatabaseConstraintColumn] D
		Inner Join [App_DataDictionary].[DatabaseTableColumn] C
		On	D.[ParentColumnId] = C.[ColumnId]
		Inner Join [App_DataDictionary].[DatabaseConstraintColumn_AK] T
		On	D.[ConstraintColumnId] = T.[ConstraintColumnId]
		Left Join [App_DataDictionary].[ModelCatalog] A
		On	T.[CatalogId] = A.[CatalogId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@CatalogId is Null or @CatalogId = T.[CatalogId]) And
		(@DatabaseName is Null or @DatabaseName = T.[DatabaseName]) And
		(@SchemaName is Null or @SchemaName = T.[SchemaName]) And
		(@ConstraintName is Null or @ConstraintName = T.[ConstraintName])
GO
