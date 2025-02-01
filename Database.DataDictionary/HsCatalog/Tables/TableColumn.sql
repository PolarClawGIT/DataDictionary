CREATE TABLE [HsCatalog].[TableColumn]
(
	[TableColumnId]				UniqueIdentifier Not Null,
	[TableId]					UniqueIdentifier Not Null,
	[ColumnName]				SysName Not Null,
    [OrdinalPosition]			Int Not Null,
	[IsNullable]				Bit Null,
	[DataType]					SysName Null,
	[ColumnDefault]				NVarChar(Max) Null,
	[CharacterMaximumLength]	SmallInt Null,
	[CharacterOctetLength]		SmallInt Null,
	[NumericPrecision]			TinyInt Null,
	[NumericPrecisionRadix]		TinyInt Null,
	[NumericScale]				TinyInt Null,
	[DateTimePrecision]			TinyInt Null,
	[CharacterSetCatalog]		SysName Null,
	[CharacterSetSchema]		SysName Null,
	[CharacterSetName]			SysName Null,
	[CollationCatalog]			SysName Null,
	[CollationSchema]			SysName Null,
	[CollationName]				SysName Null,
	[DomainCatalog]				SysName Null,
	[DomainSchema]				SysName Null,
	[DomainName]				SysName Null,
	[IsIdentity]				Bit Null,
	[IsHidden]					Bit Null,
	[IsComputed]				Bit Null,
	[ComputedDefinition]		NVarChar(Max) Null,
	[GeneratedAlwayType]		NVarChar(60) Null,
	[SysStart]					DateTime2 (7) NOT NULL,
	[SysEnd]					DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_TableColumn]
    ON [HsCatalog].[TableColumn]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_TableColumn]
    ON [HsCatalog].[TableColumn]([TableColumnId])
GO