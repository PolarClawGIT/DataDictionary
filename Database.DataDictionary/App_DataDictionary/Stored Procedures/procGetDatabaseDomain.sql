CREATE PROCEDURE [App_DataDictionary].[procGetDatabaseDomain]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@DatabaseName SysName = Null,
		@SchemaName SysName = Null,
		@DomainName SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseDomain.
*/
Select	S.[CatalogId],
		S.[DatabaseName],
		S.[SchemaName],
		D.[DomainName],
		D.[DataType],
		D.[DomainDefault],
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
		D.[CollationName]
From	[App_DataDictionary].[DatabaseDomain] D
		Inner Join [App_DataDictionary].[DatabaseSchema_AK] S
		On	D.[SchemaId] = S.[SchemaId]
		Left Join [App_DataDictionary].[ModelCatalog] A
		On	S.[CatalogId] = A.[CatalogId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@CatalogId is Null or @CatalogId = S.[CatalogId]) And
		(@DatabaseName is Null or @DatabaseName = S.[DatabaseName]) And
		(@SchemaName is Null or @SchemaName = S.[SchemaName]) And
		(@DomainName is Null or @DomainName = D.[DomainName])
GO