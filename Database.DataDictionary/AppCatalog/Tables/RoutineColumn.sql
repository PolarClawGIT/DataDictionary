CREATE TABLE [AppCatalog].[RoutineColumn]
(
	-- [INFORMATION_SCHEMA] has a [INFORMATION_SCHEMA].ROUTINE_COLUMNS that contains
	-- the columns for Table Valued Functions (possibly others). 
	-- A design decision was made to treat Procedures and Functions as a Sub-Type (de-normalized).
	-- The RoutineType delineates each of the sub-types.
	[RoutineColumnId]        UniqueIdentifier Not Null CONSTRAINT [DF_RoutineColumnId] DEFAULT (newid()),
	[RoutineId]              UniqueIdentifier Not Null,
	-- Note: TableColumn and RoutineColumn use the same base definitions.
	[ColumnName]             SysName Not Null,
    [OrdinalPosition]        Int Not Null,
	[IsNullable]             Bit Null,
	[DataType]               SysName Null,
	[ColumnDefault]          NVarChar(Max) Null,
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
	[IsIdentity]             Bit Null,
	--[IsHidden]               Bit Null,
	[IsComputed]             Bit Null,
	[ComputedDefinition]     NVarChar(Max) Null,
	--[GeneratedAlwayType]     NVarChar(60) Null,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_RoutineColumn_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_RoutineColumn_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_RoutineColumn] PRIMARY KEY CLUSTERED ([RoutineColumnId] ASC),
	CONSTRAINT [FK_RoutineColumnRoutine] FOREIGN KEY ([RoutineId]) REFERENCES [AppCatalog].[Routine] ([RoutineId]),
)  WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsCatalog].[RoutineColumn]))
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_RoutineColumn]
    ON [AppCatalog].[RoutineColumn]([ColumnName], [RoutineId]);
GO
