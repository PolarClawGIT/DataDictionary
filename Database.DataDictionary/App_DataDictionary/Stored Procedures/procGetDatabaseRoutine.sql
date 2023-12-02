CREATE PROCEDURE [App_DataDictionary].[procGetDatabaseRoutine]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@DatabaseName SysName = Null,
		@SchemaName SysName = Null,
		@RoutineName SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseRoutine.
*/
Select	S.[CatalogId],
		D.[RoutineId],
		S.[DatabaseName],
		S.[SchemaName],
		D.[RoutineName],
		C.[ScopeName],
		D.[RoutineType]
From	[App_DataDictionary].[DatabaseRoutine] D
		Inner Join [App_DataDictionary].[DatabaseSchema_AK] S
		On	D.[SchemaId] = S.[SchemaId]
		Left Join [App_DataDictionary].[ModelCatalog] A
		On	S.[CatalogId] = A.[CatalogId]
		Outer Apply [App_DataDictionary].[funcGetScopeName](D.[ScopeId]) C
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@CatalogId is Null or @CatalogId = S.[CatalogId]) And
		(@DatabaseName is Null or @DatabaseName = S.[DatabaseName]) And
		(@SchemaName is Null or @SchemaName = S.[SchemaName]) And
		(@RoutineName is Null or @RoutineName = D.[RoutineName])
GO