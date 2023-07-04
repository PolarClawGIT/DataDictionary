CREATE TABLE [App_DataDictionary].[DatabaseColumn]
(
	-- Note: GetSchema returns the columns for both View and Tables.
	-- [INFORMATION_SCHEMA] has one view that represents columns from View and Tables.
	-- This structure matches the implementation for both.
	-- Computed and Generated Columns are also identified.
	[CatalogId] UniqueIdentifier Not Null,
	[SchemaName] SysName Not Null,
	[TableName] SysName Not Null,
	[ColumnName] SysName Not Null,
    [OrdinalPosition] Int Null,
	[ColumnDefault] NVarChar(Max) Null,
	[IsNullable] Bit Null,
	[DataType] SysName Null,
	[CharacterMaxiumLength] Int Null,
	[CharacterOctetLenght] Int Null,
	[NumericPercision] TinyInt Null,
	[NumericPercisionRaxix] SmallInt Null,
	[NumericScale] Int Null,
	[DateTimePrecision] SmallInt Null,
	[CharacterSetCatalog] SysName Null,
	[CharacterSetSchema] SysName Null,
	[CharacterSetName] SysName Null,
	[CollationCatalog] SysName Null,
	[CollationSchema] SysName Null,
	[CollationName] SysName Null,
	[DomainCatalog] SysName Null,
	[DomainSchema] SysName Null,
	[DomainName] SysName Null,
	[IsIdentity] Bit Null,
	[IsHidden] Bit Null,
	[IsComputed] Bit Null,
	[ComputedDefinition] NVarChar(Max) Null,
	[GeneratedAlwayType] NVarChar(60) Null,
	-- Keys
	CONSTRAINT [PK_DatabaseColumn] PRIMARY KEY CLUSTERED ([CatalogId] ASC, [SchemaName] ASC, [TableName] ASC, [ColumnName] ASC),
	CONSTRAINT [FK_DatabaseColumnCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
	CONSTRAINT [FK_DatabaseColumnTable] FOREIGN KEY ([CatalogId], [SchemaName], [TableName]) REFERENCES [App_DataDictionary].[DatabaseTable] ([CatalogId], [SchemaName], [TableName]),
)
/*
Select	I.[TABLE_CATALOG] As [TableCatalog],
		I.[TABLE_SCHEMA] As [TableSchema],
		I.[TABLE_NAME] As [TableName],
		I.[COLUMN_NAME] As [ColumnName],
		I.[ORDINAL_POSITION] As [OrdinalPosition],
		I.[COLUMN_DEFAULT] As [ColumnDefault],
		I.[IS_NULLABLE] As [IsNullable],
		I.[DATA_TYPE] As [DataType],
		I.[CHARACTER_MAXIMUM_LENGTH] As [CharacterMaxiumLength],
		I.[CHARACTER_OCTET_LENGTH] As [CharacterOctetLenght],
		I.[NUMERIC_PRECISION] As [NumericPercision],
		I.[NUMERIC_PRECISION_RADIX] As [NumericPercisionRaxix],
		I.[NUMERIC_SCALE] As [NumericScale],
		I.[DATETIME_PRECISION] As [DateTimePrecision],
		I.[CHARACTER_SET_CATALOG] As [CharacterSetCatalog],
		I.[CHARACTER_SET_SCHEMA] As [CharacterSetSchema],
		I.[CHARACTER_SET_NAME] As [CharacterSetName], 
		I.[COLLATION_CATALOG] As [CollationCatalog],
		I.[COLLATION_SCHEMA] As [CollationSchema],
		I.[COLLATION_NAME] As [CollationName],
		I.[DOMAIN_CATALOG] As [DomainCatalog],
		I.[DOMAIN_SCHEMA] As [DomainSchema],
		I.[DOMAIN_NAME] As [DomainName],
		C.[is_identity] As [IsIdentity],
		C.[is_hidden] As [IsHidden],
		C.[is_computed] As [IsComputed],
		P.[definition] As [ComputedDefinition],
		NullIf(C.[generated_always_type_desc],'NOT_APPLICABLE') As [GeneratedAlwayType]
From	[INFORMATION_SCHEMA].[COLUMNS] I
		Left Join [sys].[Columns] C
		On	I.[TABLE_SCHEMA] = Object_Schema_Name(C.[object_id]) And
			I.[TABLE_NAME] = Object_Name(C.[object_id]) And
			I.[COLUMN_NAME] = C.[name]
		Left Join [sys].[computed_columns] P
		On	C.[object_id] = P.[object_id] And
			C.[column_id] = P.[column_id]
*/
