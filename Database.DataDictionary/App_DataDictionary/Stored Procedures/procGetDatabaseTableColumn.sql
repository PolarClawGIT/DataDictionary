CREATE PROCEDURE [App_DataDictionary].[procGetDatabaseTableColumn]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@DatabaseName SysName = Null,
		@SchemaName SysName = Null,
		@TableName SysName = Null,
		@ColumnName SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseColumn.
*/
Select	T.[CatalogId],
		T.[DatabaseName],
		T.[SchemaName],
		T.[TableName],
		D.[ColumnName],
		D.[OrdinalPosition],
		D.[ColumnDefault],
		D.[IsNullable],
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
		D.[DomainName],
		D.[IsIdentity],
		D.[IsHidden],
		D.[IsComputed],
		D.[ComputedDefinition],
		D.[GeneratedAlwayType]
From	[App_DataDictionary].[DatabaseTableColumn] D
		Inner Join [App_DataDictionary].[DatabaseTable_AK] T
		On	D.[TableId] = T.[TableId]
		Left Join [App_DataDictionary].[ModelCatalog] A
		On	T.[CatalogId] = A.[CatalogId]
		
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@CatalogId is Null or @CatalogId = T.[CatalogId]) And
		(@DatabaseName is Null or @DatabaseName = T.[DatabaseName]) And
		(@SchemaName is Null or @SchemaName = T.[SchemaName]) And
		(@TableName is Null or @TableName = T.[TableName]) And
		(@ColumnName is Null or @ColumnName = D.[ColumnName])
GO