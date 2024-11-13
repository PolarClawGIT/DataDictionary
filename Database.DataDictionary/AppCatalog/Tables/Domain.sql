CREATE TABLE [AppCatalog].[Domain]
(
	-- Domain is a synonym for user defined type.
	-- ER Diagrams tools also refer to Domains with a similar definition.
	[DomainId]              UniqueIdentifier Not Null CONSTRAINT [DF_DomainId] DEFAULT (newid()),
	[SchemaId]              UniqueIdentifier Not Null,
	[DomainName]            SysName Not Null,
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
	-- Temporal History Support
	[CreatedBy] SysName Not Null CONSTRAINT [DF_Domain_CreatedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Domain_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Domain_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Domain] PRIMARY KEY CLUSTERED ([DomainId]),
	CONSTRAINT [FK_DomainSchema] FOREIGN KEY ([SchemaId]) REFERENCES [AppCatalog].[Schema] ([SchemaId]),
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsCatalog].[Domain]))
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Domain]
    ON [AppCatalog].[Domain]([DomainName], [SchemaId]);
GO
