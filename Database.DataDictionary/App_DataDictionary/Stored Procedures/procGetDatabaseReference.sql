CREATE PROCEDURE [App_DataDictionary].[procGetDatabaseReference]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@DatabaseName SysName = Null,
		@SchemaName SysName = Null,
		@ObjectName SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseReference.
*/
Select	R.[CatalogId],
		D.[ReferenceId],
		R.[DatabaseName],
		R.[SchemaName],
		R.[ObjectName],
		D.[ObjectType],
		D.[ReferencedDatabaseName],
		D.[ReferencedSchemaName],
		D.[ReferencedObjectName],
		D.[ReferencedType],
		D.[IsCallerDependent],
		D.[IsAmbiguous],
		D.[IsSelected],
		D.[IsUpdated],
		D.[IsSelectAll],
		D.[IsAllColumnsFound],
		D.[IsInsertAll],
		D.[IsIncomplete]
From	[App_DataDictionary].[DatabaseReference] D
		Left Join [App_DataDictionary].[DatabaseObject] R
		On	D.[ObjectId] = R.[ObjectId]
		Left Join [App_DataDictionary].[ModelCatalog] A
		On	R.[CatalogId] = A.[CatalogId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@CatalogId is Null or @CatalogId = R.[CatalogId]) And
		(@DatabaseName is Null or @DatabaseName = R.[DatabaseName]) And
		(@SchemaName is Null or @SchemaName = R.[SchemaName]) And
		(@ObjectName is Null or @ObjectName = R.[ObjectName])
GO