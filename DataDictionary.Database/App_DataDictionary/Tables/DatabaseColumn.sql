CREATE TABLE [App_DataDictionary].[DatabaseColumn]
(
	[CatalogId] UniqueIdentifier Not Null,
	[SchemaName] SysName Not Null,
	[ObjectName] SysName Not Null,
	[ColumnName] SysName Not Null,
    [OrdinalPosition] Int Null,
    [ColumnDefault] NVarChar(Max) Null,
    [IsNullable] Bit Null,
    [DataType] NVarChar(100) Null,
    [CharacterMaximumLength] Int Null,
    [CharacterOctetLength] Int Null,
    [NumericPrecision] TinyInt Null,
    [NumericPrecisionRadix] SmallInt Null,
    [NumericScale] Int Null,
    [DateTimePrecision] SmallInt Null,
    [CharacterSetCatalog] SysName Null,
    [CharacterSetSchema] SysName Null,
    [CharacterSetName] SysName Null,
    [CollationCatalog] SysName Null,
    [IsSparse] Bit Null,
    [IsColumnSet] Bit Null,
    [IsFileStream] Bit Null,
	-- Keys
	CONSTRAINT [PK_DatabaseColumn] PRIMARY KEY CLUSTERED ([CatalogId] ASC, [SchemaName] ASC, [ObjectName] ASC, [ColumnName] ASC),
	CONSTRAINT [FK_DatabaseColumnCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),

)
