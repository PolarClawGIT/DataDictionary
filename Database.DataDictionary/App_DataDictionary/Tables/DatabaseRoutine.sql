CREATE TABLE [App_DataDictionary].[DatabaseRoutine]
(
	[RoutineId]          UniqueIdentifier Not Null CONSTRAINT [DF_DatabaseRoutineId] DEFAULT (newid()),
	[SchemaId]           UniqueIdentifier Not Null,
	[RoutineName]        SysName Not Null,
	[RoutineType]        [App_DataDictionary].[typeObjectType] Null, -- Known types: PROCEDURE, FUNCTION
	-- TODO: Add System Version later once the schema is locked down. Not needed for Db Schema?
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_DatabaseRoutine_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DatabaseRoutine_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DatabaseRoutine_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DatabaseRoutine] PRIMARY KEY CLUSTERED ([RoutineId] ASC),
	CONSTRAINT [FK_DatabaseRoutineSchema] FOREIGN KEY ([SchemaId]) REFERENCES [App_DataDictionary].[DatabaseSchema] ([SchemaId]),
    CONSTRAINT [CK_DatabaseRoutineType] CHECK ([RoutineType]='Procedure' OR [RoutineType]='Function' OR [RoutineType] IS NULL),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DatabaseRoutine]
    ON [App_DataDictionary].[DatabaseRoutine]([RoutineName], [SchemaId]);
GO
