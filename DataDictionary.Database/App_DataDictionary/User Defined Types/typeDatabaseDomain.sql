CREATE TYPE [App_DataDictionary].[typeDatabaseDomain] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[CatalogName]            SysName Null,
	[SchemaName]             SysName Null,
	[DomainName]             SysName Null,
	[DataType]               SysName Null,
	[DomainDefault]          NVarChar(Max) Null,
	[CharacterMaxiumLength]  Int Null,
	[CharacterOctetLenght]   Int Null,
	[NumericPercision]       TinyInt Null,
	[NumericPercisionRadix]  SmallInt Null,
	[NumericScale]           Int Null,
	[DateTimePrecision]      SmallInt Null,
	[CharacterSetCatalog]    SysName Null,
	[CharacterSetSchema]     SysName Null,
	[CharacterSetName]       SysName Null,
	[CollationCatalog]       SysName Null,
	[CollationSchema]        SysName Null,
	[CollationName]          SysName Null
)
