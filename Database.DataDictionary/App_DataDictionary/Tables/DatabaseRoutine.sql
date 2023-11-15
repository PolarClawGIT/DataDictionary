CREATE TABLE [App_DataDictionary].[DatabaseRoutine]
(
	[RoutineId]          UniqueIdentifier Not Null CONSTRAINT [DF_DatabaseRoutineId] DEFAULT (newid()),
	[SchemaId]           UniqueIdentifier Not Null,
	[RoutineName]        SysName Not Null,
	[ScopeId]            Int Not Null,
	[RoutineType]        NVarChar(60) Null, -- Known types: PROCEDURE, FUNCTION
	-- TODO: Add System Version later once the schema is locked down. Not needed for Db Schema?
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DatabaseRoutine_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DatabaseRoutine_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DatabaseRoutine_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DatabaseRoutine] PRIMARY KEY CLUSTERED ([RoutineId] ASC),
	CONSTRAINT [FK_DatabaseRoutineSchema] FOREIGN KEY ([SchemaId]) REFERENCES [App_DataDictionary].[DatabaseSchema] ([SchemaId]),
	CONSTRAINT [FK_DatabaseRoutineScope] FOREIGN KEY ([ScopeId]) REFERENCES [App_DataDictionary].[ApplicationScope] ([ScopeId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DatabaseRoutine]
    ON [App_DataDictionary].[DatabaseRoutine]([RoutineName], [SchemaId]);
GO
