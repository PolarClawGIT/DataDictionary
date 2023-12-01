CREATE PROCEDURE [App_DataDictionary].[procGetDatabaseRoutineDependency]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@DatabaseName SysName = Null,
		@SchemaName SysName = Null,
		@RoutineName SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseRoutineDependency.
*/
Select	R.[CatalogId],
		R.[DatabaseName],
		R.[SchemaName],
		R.[RoutineName],
		D.[ReferenceSchemaName],
		D.[ReferenceObjectName],
		D.[ReferenceObjectType],
		D.[ReferenceColumnName],
		D.[IsCallerDependent],
		D.[IsAmbiguous],
		D.[IsSelected],
		D.[IsUpdated],
		D.[IsSelectAll],
		D.[IsAllColumnsFound],
		D.[IsInsertAll],
		D.[IsIncomplete]
From	[App_DataDictionary].[DatabaseRoutineDependency] D
		Inner Join [App_DataDictionary].[DatabaseRoutine_AK] R
		On	D.[RoutineId] = R.[RoutineId]
		Left Join [App_DataDictionary].[ModelCatalog] A
		On	R.[CatalogId] = A.[CatalogId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@CatalogId is Null or @CatalogId = R.[CatalogId]) And
		(@DatabaseName is Null or @DatabaseName = R.[DatabaseName]) And
		(@SchemaName is Null or @SchemaName = R.[SchemaName]) And
		(@RoutineName is Null or @RoutineName = R.[RoutineName])
GO