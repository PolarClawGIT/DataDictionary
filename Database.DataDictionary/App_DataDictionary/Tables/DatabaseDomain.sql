CREATE TABLE [App_DataDictionary].[DatabaseDomain]
(
	-- Domain is a synonym for user defined type.
	-- ER Diagrams tools also refer to Domains with a similar definition.
	[DomainId]              UniqueIdentifier Not Null CONSTRAINT [DF_DatabaseDomainId] DEFAULT (newid()),
	[SchemaId]              UniqueIdentifier Not Null,
	[DomainName]            SysName Not Null,
	[ScopeId]               Int Not Null,
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
	-- TODO: Add System Version later once the schema is locked down. Not needed for Db Schema?
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DatabaseDomain_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DatabaseDomain_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DatabaseDomain_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DatabaseDomain] PRIMARY KEY CLUSTERED ([DomainId]),
	CONSTRAINT [FK_DatabaseDomainSchema] FOREIGN KEY ([SchemaId]) REFERENCES [App_DataDictionary].[DatabaseSchema] ([SchemaId]),
	CONSTRAINT [FK_DatabaseDomainScope] FOREIGN KEY ([ScopeId]) REFERENCES [App_DataDictionary].[AliasScope] ([ScopeId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DatabaseDomain]
    ON [App_DataDictionary].[DatabaseDomain]([DomainName], [SchemaId]);
GO
