CREATE TABLE [HsCatalog].[Domain]
(
	[DomainId]              UniqueIdentifier Not Null,
	[SchemaId]              UniqueIdentifier Not Null,
	[DomainName]            SysName Not Null,
	[DataType]              SysName Null, -- Can be a system defined data type or "table type"
	[DomainDefault]         NVarChar(Max) Null,
	[CharacterMaximumLength] Int Null,
	[CharacterOctetLength]  Int Null,
	[NumericPrecision]      TinyInt Null,
	[NumericPrecisionRadix] SmallInt Null,
	[NumericScale]          Int Null,
	[DateTimePrecision]     SmallInt Null,
	[CharacterSetCatalog]   SysName Null,
	[CharacterSetSchema]    SysName Null,
	[CharacterSetName]      SysName Null,
	[CollationCatalog]      SysName Null,
	[CollationSchema]       SysName Null,
	[CollationName]         SysName Null,
	[CreatedBy]             SysName Not Null,
	[SysStart]              DATETIME2 (7) NOT NULL,
	[SysEnd]                DATETIME2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_Domain]
    ON [HsCatalog].[Domain]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Catalog]
    ON [HsCatalog].[Domain]([SchemaId] ASC)
GO
