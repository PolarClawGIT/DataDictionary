Select	[ROUTINE_CATALOG] As [DatabaseName],
		[ROUTINE_SCHEMA] As [SchemaName],
		[ROUTINE_NAME] As [RoutineName],
		Case
			When [ROUTINE_TYPE] In ('PROCEDURE') Then 'Procedure'
			When [ROUTINE_TYPE] In ('FUNCTION') Then 'Function'
			Else [ROUTINE_TYPE] 
			End As [RoutineType]
From	[INFORMATION_SCHEMA].[ROUTINES]
