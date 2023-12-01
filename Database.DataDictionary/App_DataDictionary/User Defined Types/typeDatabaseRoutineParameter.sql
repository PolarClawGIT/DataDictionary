﻿CREATE TYPE [App_DataDictionary].[typeDatabaseRoutineParameter] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[SchemaId]               UniqueIdentifier Null,
	[RoutineId]              UniqueIdentifier Null,
	[ParameterId]            UniqueIdentifier Null,
	[DatabaseName]           SysName Null,
	[SchemaName]             SysName Null,
	[RoutineName]            SysName Null,
	[ParameterName]          SysName Null,
	[ScopeName]              [App_DataDictionary].[typeScopeName] Null,
	[OrdinalPosition]        Int Null,
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
	[DomainName]             SysName Null
)
