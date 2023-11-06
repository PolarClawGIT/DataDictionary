CREATE PROCEDURE [App_DataDictionary].[procGetDatabaseRoutineParameter]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@DatabaseName SysName = Null,
		@SchemaName SysName = Null,
		@RoutineName SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseRoutineParameter.
*/
Select	R.[CatalogId],
		R.[DatabaseName],
		R.[SchemaName],
		R.[RoutineName],
		D.[ParameterName],
		D.[OrdinalPosition],
		D.[DataType],
		D.[CharacterMaximumLength],
		D.[CharacterOctetLength],
		D.[NumericPrecision],
		D.[NumericPrecisionRadix],
		D.[NumericScale],
		D.[DateTimePrecision],
		D.[CharacterSetCatalog],
		D.[CharacterSetSchema],
		D.[CharacterSetName],
		D.[CollationCatalog],
		D.[CollationSchema],
		D.[CollationName],
		D.[DomainCatalog],
		D.[DomainSchema],
		D.[DomainName]
From	[App_DataDictionary].[DatabaseRoutineParameter] D
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