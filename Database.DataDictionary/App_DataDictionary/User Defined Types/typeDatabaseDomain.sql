CREATE TYPE [App_DataDictionary].[typeDatabaseDomain] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[DatabaseName]           SysName Null,
	[SchemaName]             SysName Null,
	[DomainName]             SysName Null,
	[DataType]               SysName Null,
	[DomainDefault]          NVarChar(Max) Null,
	[CharacterMaximumLength]  Int Null,
	[CharacterOctetLength]   Int Null,
	[NumericPrecision]       TinyInt Null,
	[NumericPrecisionRadix]  SmallInt Null,
	[NumericScale]           Int Null,
	[DateTimePrecision]      SmallInt Null,
	[CharacterSetCatalog]    SysName Null,
	[CharacterSetSchema]     SysName Null,
	[CharacterSetName]       SysName Null,
	[CollationCatalog]       SysName Null,
	[CollationSchema]        SysName Null,
	[CollationName]          SysName Null
)
