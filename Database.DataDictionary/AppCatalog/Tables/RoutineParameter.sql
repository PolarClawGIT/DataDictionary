CREATE TABLE [AppCatalog].[RoutineParameter]
(
	-- [INFORMATION_SCHEMA] has a [INFORMATION_SCHEMA].[PARAMETERS] that contains
	-- the parameters for both Procedures and Function (possibly others). 
	-- A design decision was made to treat Procedures and Functions as a Sub-Type (de-normalized).
	-- The RoutineType delineates each of the sub-types.
	[RoutineParameterId]     UniqueIdentifier Not Null CONSTRAINT [DF_RoutineParameterId] DEFAULT (newid()),
	[RoutineId]              UniqueIdentifier Not Null,
	[ParameterName]          SysName Not Null,
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
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_RoutineParameter_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_RoutineParameter_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_RoutineParameter] PRIMARY KEY CLUSTERED ([RoutineParameterId] ASC),
	CONSTRAINT [FK_RoutineParameterRoutine] FOREIGN KEY ([RoutineId]) REFERENCES [AppCatalog].[Routine] ([RoutineId]),
)  WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsCatalog].[RoutineParameter]))
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_RoutineParameter]
    ON [AppCatalog].[RoutineParameter]([ParameterName], [RoutineId]);
GO
