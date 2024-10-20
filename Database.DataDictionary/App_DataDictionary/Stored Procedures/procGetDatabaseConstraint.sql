﻿CREATE PROCEDURE [App_DataDictionary].[procGetDatabaseConstraint]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@DatabaseName SysName = Null,
		@SchemaName SysName = Null,
		@ConstraintName SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseConstraint.
*/
Select	T.[CatalogId],
		D.[ConstraintId],
		T.[DatabaseName],
		T.[SchemaName],
		T.[TableName],
		D.[ConstraintName],
		D.[ConstraintType]
From	[App_DataDictionary].[DatabaseConstraint] D
		Inner Join [App_DataDictionary].[DatabaseConstraint_AK] T
		On	D.[ConstraintId] = T.[ConstraintId]
		Left Join [App_DataDictionary].[ModelCatalog] A
		On	T.[CatalogId] = A.[CatalogId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@CatalogId is Null or @CatalogId = T.[CatalogId]) And
		(@DatabaseName is Null or @DatabaseName = T.[DatabaseName]) And
		(@SchemaName is Null or @SchemaName = T.[SchemaName]) And
		(@ConstraintName is Null or @ConstraintName = D.[ConstraintName])
GO