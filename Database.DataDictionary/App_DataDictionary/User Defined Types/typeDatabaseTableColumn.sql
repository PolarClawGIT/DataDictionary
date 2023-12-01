﻿CREATE TYPE [App_DataDictionary].[typeDatabaseTableColumn] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[SchemaId]               UniqueIdentifier Null,
	[TableId]                UniqueIdentifier Null,
	[ColumnId]               UniqueIdentifier Null,
	[DatabaseName]           SysName          Null,
	[SchemaName]             SysName          Null,
	[TableName]              SysName          Null,
	[ColumnName]             SysName          Null,
	[ScopeName]              [App_DataDictionary].[typeScopeName] Null,
    [OrdinalPosition]        Int              Null,
	[IsNullable]             Bit              Null,
	[DataType]               SysName          Null,
	[ColumnDefault]          NVarChar(Max)    Null,
	[CharacterMaximumLength] Int              Null,
	[CharacterOctetLength]   Int              Null,
	[NumericPrecision]       TinyInt          Null,
	[NumericPrecisionRadix]  SmallInt         Null,
	[NumericScale]           Int              Null,
	[DateTimePrecision]      SmallInt         Null,
	[CharacterSetCatalog]    SysName          Null,
	[CharacterSetSchema]     SysName          Null,
	[CharacterSetName]       SysName          Null,
	[CollationCatalog]       SysName          Null,
	[CollationSchema]        SysName          Null,
	[CollationName]          SysName          Null,
	[DomainCatalog]          SysName          Null,
	[DomainSchema]           SysName          Null,
	[DomainName]             SysName          Null,
	[IsIdentity]             Bit              Null,
	[IsHidden]               Bit              Null,
	[IsComputed]             Bit              Null,
	[ComputedDefinition]     NVarChar(Max)    Null,
	[GeneratedAlwayType]     NVarChar(60)     Null
)
