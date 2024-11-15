CREATE TABLE [AppCatalog].[TableColumn]
(
	-- See notes for [Table].
	-- Column behavior is modified by [Table].[TableType]
	[ColumnId]              UniqueIdentifier Not Null CONSTRAINT [DF_DatabaseTableColumId] DEFAULT (newid()),
	[TableId]               UniqueIdentifier Not Null,
	-- Note: TableColumn, RoutineColumn and ConstraintColumn all use the same base definitions.
	[ColumnName]            SysName Not Null,
    [OrdinalPosition]       Int Not Null,
	[IsNullable]            Bit Null,
	[DataType]              SysName Null,
	[ColumnDefault]         NVarChar(Max) Null,
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
	[DomainCatalog]         SysName Null,
	[DomainSchema]          SysName Null,
	[DomainName]            SysName Null,
	[IsIdentity]            Bit Null,
	[IsHidden]              Bit Null,
	[IsComputed]            Bit Null,
	[ComputedDefinition]    NVarChar(Max) Null,
	[GeneratedAlwayType]    NVarChar(60) Null,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TableColumn_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TableColumn_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_TableColumn] PRIMARY KEY CLUSTERED ([ColumnId] ASC),
	CONSTRAINT [FK_TableColumnTable] FOREIGN KEY ([TableId]) REFERENCES [AppCatalog].[Table] ([TableId]),
)  WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsCatalog].[TableColumn]))
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_TableColumn]
    ON [AppCatalog].[TableColumn]([ColumnName], [TableId]);
GO

