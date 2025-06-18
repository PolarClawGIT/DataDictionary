CREATE TABLE [HsCatalog].[Routine]
(
	-- [INFORMATION_SCHEMA] has a [INFORMATION_SCHEMA].[ROUTINES] that contains
	-- both Procedures and Function (possibly others). 
	-- A design decision was made to treat Procedures and Functions as a Sub-Type (de-normalized).
	-- The RoutineType delineates each of the sub-types.
	[RoutineId]          UniqueIdentifier Not Null CONSTRAINT [DF_RoutineId] DEFAULT (newid()),
	[SchemaId]           UniqueIdentifier Not Null,
	[RoutineName]        SysName Not Null,
	[RoutineType]        [AppGeneral].[typeObjectType] Null, -- Known types: PROCEDURE, FUNCTION
	[SysStart]           DateTime2 (7) Not Null,
	[SysEnd]             DateTime2 (7)  Not Null,)
GO
CREATE CLUSTERED INDEX [IX_Routine]
    ON [HsCatalog].[Routine]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Routine]
    ON [HsCatalog].[Routine]([RoutineId] ASC)
GO