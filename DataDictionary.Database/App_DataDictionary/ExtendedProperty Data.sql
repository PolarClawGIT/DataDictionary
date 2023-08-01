-- TODO: Obsolete, needs to be re-done for new structure
Begin Try;
	Begin Transaction;
	Set NoCount On;

Declare @Property Table (
	[PropertyId] Int Not Null,
	[PropertyName] SysName Not Null,
	[PropertyTitle] NVarChar(100) Not Null)
Insert into @Property Values (1,'MS_Description','Description');

Merge [App_DataDictionary].[ApplicationProperty] T
Using @Property S
On	T.[PropertyId] = S.[PropertyId]
When Matched Then Update Set
	[PropertyTitle] = S.[PropertyTitle]
When Not Matched by Target Then
	Insert ([PropertyId], [PropertyTitle])
	Values ([PropertyId], [PropertyTitle]);
	

-- This script is used to build and maintain the data in the DomainObjectType list.
-- This list is intended to support the diffrent things MS SQL extended properties can be placed upon
-- AND ones going to be supported by the application
-- List is sourced from: https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16
With [Level0] As (
	Select	Convert(sysname,Null) As [level0type] Where 1 = 2
	--Union Select 'ASSEMBLY'
	--Union Select 'CONTRACT'
	--Union Select 'EVENT NOTIFICATION'
	--Union Select 'FILEGROUP'
	--Union Select 'MESSAGE TYPE'
	--Union Select 'PARTITION FUNCTION'
	--Union Select 'PARTITION SCHEME'
	--Union Select 'REMOTE SERVICE BINDING'
	--Union Select 'ROUTE'
	Union Select 'SCHEMA'
	--Union Select 'SERVICE'
	--Union Select 'USER'
	--Union Select 'TRIGGER'
	--Union Select 'TYPE'
	--Union Select 'PLAN GUIDE'
	),
[Level1] As (
	Select	Convert(sysname,Null) As [level0type], Convert(sysname,Null) As [level1type] Where 1 = 2
	--Union Select 'SCHEMA', 'AGGREGATE'
	--Union Select 'SCHEMA', 'DEFAULT'
	Union Select 'SCHEMA', 'FUNCTION'
	--Union Select 'SCHEMA', 'LOGICAL FILE NAME'
	Union Select 'SCHEMA', 'PROCEDURE'
	--Union Select 'SCHEMA', 'QUEUE'
	--Union Select 'SCHEMA', 'RULE'
	--Union Select 'SCHEMA', 'SEQUENCE'
	--Union Select 'SCHEMA', 'SYNONYM'
	Union Select 'SCHEMA', 'TABLE'
	--Union Select 'SCHEMA', 'TABLE_TYPE'
	--Union Select 'SCHEMA', 'TYPE'
	Union Select 'SCHEMA', 'VIEW'
	--Union Select 'SCHEMA', 'XML SCHEMA COLLECTION'
	),
[Level2] As (
	Select	Convert(sysname,Null) As [level1type], Convert(sysname,Null) As [level2type] Where 1 = 2
	Union Select 'TABLE',	'COLUMN'
	Union Select 'VIEW',	'COLUMN'
	--Union Select 'TABLE',	'CONSTRAINT'
	--Union Select 'TABLE',	'EVENT NOTIFICATION'
	--Union Select 'TABLE',	'INDEX'
	--Union Select 'VIEW',	'INDEX'
	Union Select 'PROCEDURE', 'PARAMETER'
	Union Select 'FUNCTION', 'PARAMETER'
	--Union Select 'TABLE',	'TRIGGER'
	--Union Select 'VIEW',	'TRIGGER'
	),
[Combine] As (
	Select	Convert(sysname,Null) As [level0type],
			Convert(sysname,Null) As [level1type],
			Convert(sysname,Null) As [level2type]
	Where	1 = 2
	Union
	Select	[level0type],
			Null As [level1type],
			Null As [level2type]
	From	[Level0]
	Union
	Select	P.[level0type],
			C.[level1type],
			Null As [level2type]
	From	[Level1] C
			Inner Join [Level0] P
			On	C.[level0type] = P.[level0type]
	Union
	Select	G.[level0type],
			P.[level1type],
			C.[level2type]
	From	[Level2] C
			Inner Join [Level1] P
			On	C.[level1type] = P.[level1type]
			Inner Join [Level0] G
			On	P.[level0type] = G.[level0type]),
[Data] As (
	Select	X.[PropertyId],
			X.[PropertyName],
			[level0type] As [ScopeType],
			[level1type] As [ObjectType],
			[level2type] As [ElementType]
	From	[Combine] C
			Cross Join @Property X
			Left Join [App_DataDictionary].[ApplicationPropertyScope] P
			On	IsNull(C.[level0type],'') = IsNull(P.[ScopeType],'') And
				IsNull(C.[level1type],'') = IsNull(P.[ObjectType],'') And
				IsNull(C.[level2type],'') = IsNull(P.[ElementType],'')
				)
Merge [App_DataDictionary].[ApplicationPropertyScope] As T
Using [Data] S
On T.[PropertyId] = S.[PropertyId]
When Matched Then Update Set 
	PropertyId = S.PropertyId,
	PropertyName = S.PropertyName,
	ScopeType = S.ScopeType,
	ObjectType = S.ObjectType,
	ElementType = S.ElementType
When Not Matched By Target Then
	Insert (PropertyId, PropertyName, ScopeType, ObjectType, ElementType)
	Values (PropertyId, PropertyName, ScopeType, ObjectType, ElementType)
When Not Matched by Source Then Delete;

Select	*
From	[App_DataDictionary].[ApplicationProperty]

Select	*
From	[App_DataDictionary].[ApplicationPropertyScope]

	-- By default, throw and error and exit without committing
;	Throw 50000, 'Abort process, comment out this line when ready to actual Commit the transaction',255;
	
	Commit Transaction;
	Print 'Commit Issued';
End Try
Begin Catch
	Print FormatMessage ('*** Error Report: %s ***', Object_Name(@@ProcID));
	Print FormatMessage (' Message- %s', ERROR_MESSAGE());
	Print FormatMessage (' Number- %i', ERROR_NUMBER());
	Print FormatMessage (' Severity- %i', ERROR_SEVERITY());
	Print FormatMessage (' State- %i', ERROR_STATE());
	Print FormatMessage (' Procedure- %s', ERROR_PROCEDURE());
	Print FormatMessage (' Line- %i', ERROR_LINE());
	Print FormatMessage (' @@TranCount - %i', @@TranCount);
	Print FormatMessage (' @@NestLevel - %i', @@NestLevel);
	Print FormatMessage (' Original_Login - %s', Original_Login());
	Print FormatMessage (' Current_User - %s', Current_User);
	Print FormatMessage (' XAct_State - %i', XAct_State());
	Print '--- Debug Data ---';

	-- Rollback Transaction
	Print 'Rollback Issued';
	Rollback Transaction;
	Throw;
End Catch;
