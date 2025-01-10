CREATE TABLE [HsCatalog].[Domain]
(
	[DomainId]					UniqueIdentifier Not Null,
	[SchemaId]					UniqueIdentifier Not Null,
	[DomainName]				SysName Not Null,
	[DataType]					SysName Null, -- Can be a system defined data type or "table type"
	[DomainDefault]				NVarChar(Max) Null,
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
	[SysStart]					DateTime2 (7) NOT NULL,
	[SysEnd]					DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_Domain]
    ON [HsCatalog].[Domain]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Domain]
    ON [HsCatalog].[Domain]([SchemaId] ASC)
GO
