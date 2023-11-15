CREATE TABLE [App_DataDictionary].[DatabaseRoutineParameter]
(
	[ParameterId]            UniqueIdentifier Not Null CONSTRAINT [DF_DatabaseRoutineParameterId] DEFAULT (newid()),
	[RoutineId]              UniqueIdentifier Not Null,
	[ParameterName]          SysName Not Null,
	[ScopeId]                Int Not Null,
	[OrdinalPosition]        Int Not Null,
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
	[DomainName]             SysName Null,
	-- TODO: Add System Version later once the schema is locked down. Not needed for Db Schema?
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DatabaseRoutineParameter_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DatabaseRoutineParameter_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DatabaseRoutineParameter_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DatabaseRoutineParameter] PRIMARY KEY CLUSTERED ([ParameterId] ASC),
	CONSTRAINT [FK_DatabaseRoutineParameterRoutine] FOREIGN KEY ([RoutineId]) REFERENCES [App_DataDictionary].[DatabaseRoutine] ([RoutineId]),
	CONSTRAINT [FK_DatabaseRoutineParameterScope] FOREIGN KEY ([ScopeId]) REFERENCES [App_DataDictionary].[ApplicationScope] ([ScopeId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DatabaseRoutineParameter]
    ON [App_DataDictionary].[DatabaseRoutineParameter]([ParameterName], [RoutineId]);
GO
