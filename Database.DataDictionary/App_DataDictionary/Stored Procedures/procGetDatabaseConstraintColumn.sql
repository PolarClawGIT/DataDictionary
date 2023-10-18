﻿CREATE PROCEDURE [App_DataDictionary].[procGetDatabaseConstraintColumn]
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
Select	D.[CatalogId],
		C.[SourceDatabaseName] As [DatabaseName],
		D.[SchemaName],
		D.[ConstraintName],
		D.[TableName],
		D.[ColumnName],
		D.[OrdinalPosition],
		D.[ReferenceSchemaName],
		D.[ReferenceTableName],
		D.[ReferenceColumnName]
From	[App_DataDictionary].[DatabaseConstraintColumn] D
		Left Join [App_DataDictionary].[ModelCatalog] A
		On	D.[CatalogId] = A.[CatalogId]
		Inner Join [App_DataDictionary].[DatabaseCatalog] C
		On	D.[CatalogId] = C.[CatalogId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@CatalogId is Null or @CatalogId = D.[CatalogId]) And
		(@DatabaseName is Null or @DatabaseName = C.[SourceDatabaseName]) And
		(@SchemaName is Null or @SchemaName = D.[SchemaName]) And
		(@ConstraintName is Null or @ConstraintName = D.[ConstraintName])
GO