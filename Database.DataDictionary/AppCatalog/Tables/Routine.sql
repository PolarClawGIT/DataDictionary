CREATE TABLE [AppCatalog].[Routine]
(
	-- [INFORMATION_SCHEMA] has a [INFORMATION_SCHEMA].[ROUTINES] that contains
	-- both Procedures and Function (possibly others). 
	-- A design decision was made to treat Procedures and Functions as a Sub-Type (de-normalized).
	-- The RoutineType delineates each of the sub-types.
	[RoutineId]          UniqueIdentifier Not Null CONSTRAINT [DF_RoutineId] DEFAULT (newid()),
	[SchemaId]           UniqueIdentifier Not Null,
	[RoutineName]        SysName Not Null,
	[RoutineType]        [AppGeneral].[typeObjectType] Null, -- Known types: PROCEDURE, FUNCTION
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Routine_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Routine_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Routine] PRIMARY KEY CLUSTERED ([RoutineId] ASC),
	CONSTRAINT [FK_RoutineSchema] FOREIGN KEY ([SchemaId]) REFERENCES [AppCatalog].[Schema] ([SchemaId]),
    CONSTRAINT [CK_RoutineType] CHECK ([RoutineType]='Procedure' OR [RoutineType]='Function' OR [RoutineType] IS NULL),
)  WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsCatalog].[Routine]))
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Routine]
    ON [AppCatalog].[Routine]([RoutineName], [SchemaId]);
GO
