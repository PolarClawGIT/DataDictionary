-- Remember: Resource files do not update until they are saved directly.
Select	[DOMAIN_CATALOG] As[DatabaseName],
		[DOMAIN_SCHEMA] As[SchemaName],
		[DOMAIN_NAME] As[DomainName],
		[DATA_TYPE] As[DataType],
		[DOMAIN_DEFAULT] As[DomainDefault],
		[CHARACTER_MAXIMUM_LENGTH] As[CharacterMaximumLength],
		[CHARACTER_OCTET_LENGTH] As[CharacterOctetLength],
		[NUMERIC_PRECISION] As[NumericPrecision],
		[NUMERIC_PRECISION_RADIX] As[NumericPrecisionRadix],
		[NUMERIC_SCALE] As[NumericScale],
		[DATETIME_PRECISION] As[DateTimePrecision],
		[CHARACTER_SET_CATALOG] As[CharacterSetCatalog],
		[CHARACTER_SET_SCHEMA] As[CharacterSetSchema],
		[CHARACTER_SET_NAME] As[CharacterSetName],
		[COLLATION_CATALOG] As[CollationCatalog],
		[COLLATION_SCHEMA] As[CollationSchema], 
		[COLLATION_NAME] As[CollationName]
From	[INFORMATION_SCHEMA].[DOMAINS]