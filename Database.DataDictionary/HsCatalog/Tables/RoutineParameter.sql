CREATE TABLE [HsCatalog].[RoutineParameter]
(
	[ParameterId]            UniqueIdentifier Not Null,
	[RoutineId]              UniqueIdentifier Not Null,
	[ParameterName]          SysName Not Null,
	[OrdinalPosition]        Int Not Null,
	[DataType]               SysName Null,
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
	[CollationName]          SysName Null,
	[DomainCatalog]          SysName Null,
	[DomainSchema]           SysName Null,
	[DomainName]             SysName Null,
	[SysStart]               DateTime2 (7) Not Null,
	[SysEnd]                 DateTime2 (7)  Not Null,)
GO
CREATE CLUSTERED INDEX [IX_RoutineParameter]
    ON [HsCatalog].[RoutineParameter]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_RoutineParameter]
    ON [HsCatalog].[RoutineParameter]([ParameterId] ASC)
GO