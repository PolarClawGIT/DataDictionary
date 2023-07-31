CREATE TABLE [App_DataDictionary].[DatabaseTableColumn]
(
	-- Note: GetSchema returns the columns for both View and Tables.
	-- [INFORMATION_SCHEMA] has one view that represents columns from View and Tables.
	-- This structure matches the implementation for both.
	-- Computed and Generated Columns are also identified.
	[CatalogId]             UniqueIdentifier Not Null,
	[SchemaName]            SysName Not Null,
	[TableName]             SysName Not Null,
	[ColumnName]            SysName Not Null,
    [OrdinalPosition]       Int Not Null,
	[IsNullable]            Bit Null,
	[DataType]              SysName Null,
	[ColumnDefault]         NVarChar(Max) Null,
	[CharacterMaxiumLength] Int Null,
	[CharacterOctetLenght]  Int Null,
	[NumericPercision]      TinyInt Null,
	[NumericPercisionRaxix] SmallInt Null,
	[NumericScale]          Int Null,
	[DateTimePrecision]     SmallInt Null,
	[CharacterSetCatalog]   SysName Null,
	[CharacterSetSchema]    SysName Null,
	[CharacterSetName]      SysName Null,
	[CollationCatalog]      SysName Null,
	[CollationSchema]       SysName Null,
	[CollationName]         SysName Null,
	[DomainCatalog]         SysName Null,
	[DomainSchema]          SysName Null,
	[DomainName]            SysName Null,
	[IsIdentity]            Bit Null,
	[IsHidden]              Bit Null,
	[IsComputed]            Bit Null,
	[ComputedDefinition]    NVarChar(Max) Null,
	[GeneratedAlwayType]    NVarChar(60) Null,
	-- Keys
	CONSTRAINT [PK_DatabaseTableColumn] PRIMARY KEY CLUSTERED ([CatalogId] ASC, [SchemaName] ASC, [TableName] ASC, [ColumnName] ASC),
--	CONSTRAINT [FK_DatabaseColumnCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
	CONSTRAINT [FK_DatabaseTableColumnTable] FOREIGN KEY ([CatalogId], [SchemaName], [TableName]) REFERENCES [App_DataDictionary].[DatabaseTable] ([CatalogId], [SchemaName], [TableName])

)
/*
Select	Convert(UniqueIdentifier,Null) As [CatalogId],
		I.[TABLE_CATALOG],
		I.[TABLE_SCHEMA],
		I.[TABLE_NAME],
		I.[COLUMN_NAME],
		I.[ORDINAL_POSITION],
		I.[COLUMN_DEFAULT],
		I.[IS_NULLABLE],
		I.[DATA_TYPE],
		I.[CHARACTER_MAXIMUM_LENGTH],
		I.[CHARACTER_OCTET_LENGTH],
		I.[NUMERIC_PRECISION],
		I.[NUMERIC_PRECISION_RADIX],
		I.[NUMERIC_SCALE],
		I.[DATETIME_PRECISION],
		I.[CHARACTER_SET_CATALOG],
		I.[CHARACTER_SET_SCHEMA],
		I.[CHARACTER_SET_NAME],
		I.[COLLATION_CATALOG],
		I.[COLLATION_SCHEMA],
		I.[COLLATION_NAME],
		I.[DOMAIN_CATALOG],
		I.[DOMAIN_SCHEMA],
		I.[DOMAIN_NAME],
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
