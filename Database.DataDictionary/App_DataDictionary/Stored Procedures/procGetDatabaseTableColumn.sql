CREATE PROCEDURE [App_DataDictionary].[procGetDatabaseTableColumn]
		@ModelId UniqueIdentifier = Null,
		@CatalogName SysName = Null,
		@SchemaName SysName = Null,
		@TableName SysName = Null,
		@ColumnName SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseColumn.
*/
Select	D.[CatalogId],
		C.[CatalogName],
		D.[SchemaName],
		D.[TableName],
		P.[TableType],
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
		Left Join [App_DataDictionary].[ModelCatalog] A
		On	D.[CatalogId] = A.[CatalogId]
		Inner Join [App_DataDictionary].[DatabaseCatalog] C
		On	D.[CatalogId] = C.[CatalogId]
		Inner Join [App_DataDictionary].[DatabaseTable] P
		On	D.[CatalogId] = P.[CatalogId] And
			D.[SchemaName] = P.[SchemaName] And
			D.[TableName] = P.[TableName]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@CatalogName is Null or @CatalogName = C.[CatalogName]) And
		(@SchemaName is Null or @SchemaName = D.[SchemaName]) And
		(@TableName is Null or @TableName = D.[TableName]) And
		(@ColumnName is Null or @ColumnName = D.[ColumnName])
GO