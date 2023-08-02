﻿CREATE TABLE [App_DataDictionary].[DatabaseDomain]
(
	-- Domain is a synonym for user defined type.
	-- ER Diagrams tools also refer to Domains with a similar definition.
	[CatalogId]             UniqueIdentifier Not Null,
	[SchemaName]            SysName Not Null,
	[DomainName]            SysName Not Null,
	[DataType]              SysName Null, -- Can be a system defined data type or "table type"
	[DomainDefault]         NVarChar(Max) Null,
	[CharacterMaxiumLength] Int Null,
	[CharacterOctetLenght]  Int Null,
	[NumericPercision]      TinyInt Null,
	[NumericPercisionRaxix] SmallInt Null,
	[NumericScale]          Int Null,
	[DateTimePrecision]     SmallInt Null,
	[CharacterSetCatalog]   SysName Null,
	[CharacterSetSchema]    SysName Null,
	[CharacterSetName]      SysName Null,
	[CollationCatalog]      SysName Null,
	[CollationSchema]       SysName Null,
	[CollationName]         SysName Null,
	CONSTRAINT [PK_DatabaseDomain] PRIMARY KEY CLUSTERED ([CatalogId] ASC, [SchemaName] ASC, [DomainName] ASC),
	CONSTRAINT [FK_DatabaseDomainSchema] FOREIGN KEY ([CatalogId], [SchemaName]) REFERENCES [App_DataDictionary].[DatabaseSchema] ([CatalogId], [SchemaName]),

)