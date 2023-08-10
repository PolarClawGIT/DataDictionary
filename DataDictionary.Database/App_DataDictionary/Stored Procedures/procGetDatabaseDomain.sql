CREATE PROCEDURE [App_DataDictionary].[procGetDatabaseDomain]
		@ModelId UniqueIdentifier = Null,
		@CatalogName SysName = Null,
		@SchemaName SysName = Null,
		@DomainName SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseDomain.
*/
Select	A.[ModelId],
		D.[CatalogId],
		C.[CatalogName],
		D.[SchemaName],
		D.[DomainName],
		D.[DataType],
		D.[DomainDefault],
		D.[CharacterMaxiumLength],
		D.[CharacterOctetLenght],
		D.[NumericPercision],
		D.[NumericPercisionRadix],
		D.[NumericScale],
		D.[DateTimePrecision],
		D.[CharacterSetCatalog],
		D.[CharacterSetSchema],
		D.[CharacterSetName],
		D.[CollationCatalog],
		D.[CollationSchema],
		D.[CollationName]
From	[App_DataDictionary].[DatabaseDomain] D
		Inner Join [App_DataDictionary].[ApplicationCatalog] A
		On	D.[CatalogId] = A.[CatalogId]
		Inner Join [App_DataDictionary].[DatabaseCatalog] C
		On	D.[CatalogId] = C.[CatalogId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@CatalogName is Null or @CatalogName = C.[CatalogName]) And
		(@SchemaName is Null or @SchemaName = D.[SchemaName]) And
		(@DomainName is Null or @DomainName = D.[DomainName])
GO