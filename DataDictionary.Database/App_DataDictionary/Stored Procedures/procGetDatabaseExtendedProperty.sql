﻿CREATE PROCEDURE [App_DataDictionary].[procGetDatabaseExtendedProperty]
		@ModelId     UniqueIdentifier = Null,
		@PropertyId  UniqueIdentifier = Null,
		@CatalogName SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseExtendedProperty.
*/

Select	A.[ModelId],
		C.[CatalogId],
		D.[PropertyId],
		C.[CatalogName],
		D.[Level0Type],
		D.[Level0Name],
		D.[Level1Type],
		D.[Level1Name],
		D.[Level2Type],
		D.[Level2Name],
		D.[ObjType],
		D.[ObjName],
		D.[PropertyName],
		D.[PropertyValue]
From	[App_DataDictionary].[DatabaseExtendedProperty] D
		Inner Join [App_DataDictionary].[ApplicationCatalog] A
		On	D.[CatalogId] = A.[CatalogId]
		Inner Join [App_DataDictionary].[DatabaseCatalog] C
		On	D.[CatalogId] = C.[CatalogId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@PropertyId is Null or @PropertyId = D.[PropertyId]) And
		(@CatalogName is Null or @CatalogName = C.[CatalogName])
GO