﻿CREATE TYPE [App_DataDictionary].[typeDatabaseColumn] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[CatalogName]            SysName          Null,
	[SchemaName]             SysName          Null,
	[TableName]              SysName          Null,
	[ColumnName]             SysName          Null,
    [OrdinalPosition]        Int              Null,
	[IsNullable]             Bit              Null,
	[DataType]               SysName          Null,
	[ColumnDefault]          NVarChar(Max)    Null,
	[CharacterMaxiumLength]  Int              Null,
	[CharacterOctetLenght]   Int              Null,
	[NumericPercision]       TinyInt          Null,
	[NumericPercisionRaxix]  SmallInt         Null,
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
