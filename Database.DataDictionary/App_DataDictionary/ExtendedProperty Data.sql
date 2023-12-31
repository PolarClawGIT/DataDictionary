﻿Begin Try;
	Begin Transaction;
	Set NoCount On;

	Delete From [App_DataDictionary].[ApplicationProperty]

	Declare @Data [App_DataDictionary].[typeApplicationProperty]

	-- The natural sort order for GUID is right to left.
	-- GUID's used here will not be generated by the system.
	-- This is the only place these values are hard coded and it is just for initializing data.

	-- Business Values
	Insert Into @Data Values (
		'00000000-0000-0000-0010-000000000010',
		'Business Definition',
		'Definition of the item in Business Terms.',
		1,0,0,0,null,null)
	Insert Into @Data Values (
		'00000000-0000-0000-0020-000000000010',
		'Technical Definition',
		'Definition of the item in Technical Terms.',
		1,0,0,0,null,null)
	Insert Into @Data Values (
		'00000000-0000-0000-0010-000000000020',
		'MS Description',
		'Commonly used by Microsoft tools to store the user defined Description of the element.',
		0,1,0,0,'MS_Description',null)
	Insert Into @Data Values (
		'00000000-0000-0000-0010-000000000030',
		'.Net Summary',
		'Summary Documentation block for .Net Framework Code',
		0,0,1,0,null,null)

	Insert Into @Data Values (
		'00000000-0000-0000-0020-000000000030',
		'.Net System Type',
		'The types for .Net Framework Code. (SQL Supported types only, https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-data-type-mappings)',
		0,0,0,1,null,'Boolean, Byte , Byte[], Char[], DateTime, DateTimeOffset, Decimal, Double, Guid, Int16, Int32, Int64, Single, String, TimeSpan, Xml, Object')

	Select	*
	From	@Data

	Exec [App_DataDictionary].[procSetApplicationProperty] @Data

	Select	*
	From	[App_DataDictionary].[ApplicationProperty]

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
