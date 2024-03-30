CREATE TABLE [App_DataDictionary].[DatabaseTableColumn]
(
	-- Note: GetSchema returns the columns for both View and Tables.
	-- [INFORMATION_SCHEMA] has one view that represents columns from View and Tables.
	-- This structure matches the implementation for both.
	-- Computed and Generated Columns are also identified.
	[ColumnId]              UniqueIdentifier Not Null CONSTRAINT [DF_DatabaseTableColumId] DEFAULT (newid()),
	[TableId]               UniqueIdentifier Not Null,
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
	-- TODO: Add System Version later once the schema is locked down. Not needed for Db Schema?
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DatabaseTableColumn_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DatabaseTableColumn_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DatabaseTableColumn_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DatabaseTableColumn] PRIMARY KEY CLUSTERED ([ColumnId] ASC),
	CONSTRAINT [FK_DatabaseTableColumnTable] FOREIGN KEY ([TableId]) REFERENCES [App_DataDictionary].[DatabaseTable] ([TableId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DatabaseTableColumn]
    ON [App_DataDictionary].[DatabaseTableColumn]([ColumnName], [TableId]);
GO

