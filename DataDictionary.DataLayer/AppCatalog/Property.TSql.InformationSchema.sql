With [Parameter] As (
	Select Convert(SysName,Null) As [Level0Type], Convert(SysName,Null) As [Level0Name], Convert(SysName,Null) As [Level1Type], Convert(SysName,Null) As [Level1Name], Convert(SysName,Null) As [Level2Type], Convert(SysName,Null) As [Level2Name]
	Where 1=2
	Union
	Select 'SCHEMA' As [Level0Type], [SCHEMA_NAME] As [Level0Name], Null As [Level1Type], Null As [Level1Name], Null As [Level2Type], Null As [Level2Name]
	From [INFORMATION_SCHEMA].[SCHEMATA]
	Union
	Select 'SCHEMA' As [Level0Type], [TABLE_SCHEMA] As [Level0Name], 'TABLE' As [Level1Type], [TABLE_NAME] As [Level1Name], Null As [Level2Type], Null As [Level2Name]
	From [INFORMATION_SCHEMA].[TABLES]
	Union
	Select 'SCHEMA' As [Level0Type], [TABLE_SCHEMA] As [Level0Name], 'VIEW' As [Level1Type], [TABLE_NAME] As [Level1Name], Null As [Level2Type], Null As [Level2Name]
	From [INFORMATION_SCHEMA].[VIEWS]
	Union
	Select 'SCHEMA' As [Level0Type], C.[TABLE_SCHEMA] As [Level0Name], 'TABLE' As [Level1Type], C.[TABLE_NAME] As [Level1Name], 'COLUMN' As [Level2Type], C.[COLUMN_NAME] As [Level2Name]
	From [INFORMATION_SCHEMA].[COLUMNS] C INNER Join [INFORMATION_SCHEMA].[TABLES] T On C.[TABLE_CATALOG] = T.[TABLE_CATALOG] And C.[TABLE_SCHEMA] = T.[TABLE_SCHEMA] And C.[TABLE_NAME] = T.[TABLE_NAME]
	Union
	Select 'SCHEMA' As [Level0Type], C.[TABLE_SCHEMA] As [Level0Name], 'VIEW' As [Level1Type], C.[TABLE_NAME] As [Level1Name], 'COLUMN' As [Level2Type], C.[COLUMN_NAME] As [Level2Name]
	From [INFORMATION_SCHEMA].[COLUMNS] C INNER Join [INFORMATION_SCHEMA].[VIEWS] T On C.[TABLE_CATALOG] = T.[TABLE_CATALOG] And C.[TABLE_SCHEMA] = T.[TABLE_SCHEMA] And C.[TABLE_NAME] = T.[TABLE_NAME]
	Union
	Select 'SCHEMA' As [Level0Type], [ROUTINE_SCHEMA] As [Level0Name], [ROUTINE_TYPE] As [Level1Type], [ROUTINE_NAME] As [Level1Name], Null As [Level2Type], Null As [Level2Name]
	From [INFORMATION_SCHEMA].[ROUTINES]
	Union
	Select 'SCHEMA' As [Level0Type], [ROUTINE_SCHEMA] As [Level0Name], [ROUTINE_TYPE] As [Level1Type], [ROUTINE_NAME] As [Level1Name], 'PARAMETER' As [Level2Type], IIF([IS_RESULT] = 'YES' And NullIf([PARAMETER_NAME],'') is Null, 'RETURN', [PARAMETER_NAME]) As [Level2Name]
	From [INFORMATION_SCHEMA].[ROUTINES] R INNER Join [INFORMATION_SCHEMA].[PARAMETERS] P On	R.[SPECIFIC_CATALOG] = P.[SPECIFIC_CATALOG] And R.[SPECIFIC_SCHEMA] = P.[SPECIFIC_SCHEMA] And R.[SPECIFIC_NAME] = P.[SPECIFIC_NAME]
	Union
	Select 'SCHEMA' As [Level0Type], [ROUTINE_SCHEMA] As [Level0Name], [ROUTINE_TYPE] As [Level1Type], [ROUTINE_NAME] As [Level1Name], 'COLUMN' As [Level2Type], [COLUMN_NAME] As [Level2Name]
	From [INFORMATION_SCHEMA].[ROUTINES] R INNER Join [INFORMATION_SCHEMA].[ROUTINE_COLUMNS] P On	R.[SPECIFIC_CATALOG] = P.[TABLE_CATALOG] And R.[SPECIFIC_SCHEMA] = P.[TABLE_SCHEMA] And R.[SPECIFIC_NAME] = P.[TABLE_NAME]),
[Exceptions] As (
	Select Convert(SysName,Null) As [Level0Name], Convert(SysName,Null) As [Level1Name], Convert(SysName,Null) As [Level2Name]
	Where 1=2
	Union Select 'dbo', 'fn_diagramobjects', Null
	Union Select 'dbo', 'sp_alterdiagram', Null
	Union Select 'dbo', 'sp_creatediagram', Null
	Union Select 'dbo', 'sp_dropdiagram', Null
	Union Select 'dbo', 'sp_helpdiagramdefinition', Null
	Union Select 'dbo', 'sp_helpdiagrams', Null
	Union Select 'dbo', 'sp_renamediagram', Null
	Union Select 'dbo', 'sp_upgraddiagrams', Null
	Union Select 'dbo', '__RefactorLog', Null
	Union Select 'dbo', 'sysdiagrams', Null)
Select	Db_Name() [DatabaseName],
	P.[Level0Type] As [Level0Type],
	P.[Level0Name] As [Level0Name],
	P.[Level1Type] As [Level1Type],
	P.[Level1Name] As [Level1Name],
	P.[Level2Type] As [Level2Type],
	P.[Level2Name] As [Level2Name],
	[name] As [PropertyName],
	Convert(NVarChar(Max),[value]) As [PropertyValue]
FROM [Parameter] P
	Left Join [Exceptions] E
	On IsNull(P.[Level0Name],'') = IsNull(E.[Level0Name],'') And
	IsNull(P.[Level1Name],'') = IsNull(E.[Level1Name],'') And
	IsNull(P.[Level2Name],'') = IsNull(E.[Level2Name],'')		
	Cross Apply [fn_listextendedproperty] (Null, P.[Level0Type], P.[Level0Name], P.[Level1Type], P.[Level1Name], P.[Level2Type], P.[Level2Name])
WHERE E.[Level0Name] is Null