CREATE TABLE [HsCatalog].[RoutineColumn]
(
	[RoutineColumnId]                UniqueIdentifier Not Null,
	[RoutineId]               UniqueIdentifier Not Null,
	-- Note: TableColumn, RoutineColumn and ConstraintColumn all use the same base definitions
	[ColumnName]              SysName Not Null,
    [OrdinalPosition]         Int Not Null,
	[IsNullable]              Bit Null,
	[DataType]                SysName Null,
	[ColumnDefault]           NVarChar(Max) Null,
	[CharacterMaximumLength]  Int Null,
	[CharacterOctetLength]		Int Null,
	[NumericPrecision]			TinyInt Null,
	[NumericPrecisionRadix]		SmallInt Null,
	[NumericScale]				Int Null,
	[DateTimePrecision]			SmallInt Null,
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
	--[IsHidden]					Bit Null,
	[IsComputed]				Bit Null,
	[ComputedDefinition]		NVarChar(Max) Null,
	--[GeneratedAlwayType]		NVarChar(60) Null,
	[SysStart]                  DateTime2 (7) Not Null,
	[SysEnd]                    DateTime2 (7)  Not Null,)  
GO
CREATE CLUSTERED INDEX [IX_RoutineColumn]
    ON [HsCatalog].[RoutineColumn]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_RoutineColumn]
    ON [HsCatalog].[RoutineColumn]([RoutineColumnId] ASC)
GO