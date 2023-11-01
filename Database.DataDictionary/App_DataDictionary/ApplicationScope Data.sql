Begin Try;
	Begin Transaction;
	Set NoCount On;

	Delete From [App_DataDictionary].[ApplicationScope]

	Insert Into [App_DataDictionary].[ApplicationScope] Values
	-- Database: sourced from: https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16
	(0001,null,'Database','MS SQL Database, Root Node',0,0),
	-- Level 0
	--(0101,0001,'ASSEMBLY',Null,1,0),
	--(0102,0001,'CONTRACT',Null,1,0),
	--(0103,0001,'EVENT NOTIFICATION',Null,1,0),
	--(0104,0001,'FILEGROUP',Null,1,0),
	--(0105,0001,'MESSAGE TYPE',Null,1,0),
	--(0106,0001,'PARTITION FUNCTION',Null,1,0),
	--(0107,0001,'PARTITION SCHEME',Null,1,0),
	--(0108,0001,'REMOTE SERVICE BINDING',Null,1,0),
	--(0109,0001,'ROUTE',Null,1,0),
	(0110,0001,'Schema',Null,1,0),
	--(0111,0001,'SERVICE',Null,1,0),
	--(0112,0001,'USER',Null,1,0),
	--(0113,0001,'TRIGGER',Null,1,0),
	--(0114,0001,'TYPE',Null,1,0),
	--(0115,0001,'PLAN GUIDE',Null,1,0),
	-- Level 1, Selected Object only
	(0201,0110,'Aggregate',Null,1,0),
	(0202,0110,'Default',Null,1,0),
	(0203,0110,'Function',Null,1,0),
	--(0204,0110,'LOGICAL FILE NAME',Null,1,0),
	(0205,0110,'Procedure',Null,1,0),
	--(0206,0110,'QUEUE',Null,1,0),
	--(0207,0110,'RULE',Null,1,0),
	--(0208,0110,'SEQUENCE',Null,1,0),
	--(0209,0110,'SYNONYM',Null,1,0),
	(0210,0110,'Table',Null,1,0),
	--(0211,0110,'TABLE_TYPE',Null,1,0),
	(0212,0110,'Type',Null,1,0),
	(0213,0110,'View',Null,1,0),
	--(0214,0110,'XML SCHEMA COLLECTION',Null,1,0),
	-- Level 2, Selected Object only
	(0301,0210,'Column',Null,1,0),
	(0302,0210,'Constraimt',Null,1,0),
	(0304,0210,'Index',Null,1,0),
	(0305,0213,'Column',Null,1,0),
	(0306,0213,'Index',Null,1,0),
	(0307,0205,'Parameter',Null,1,0),
	(0308,0203,'Parameter',Null,1,0),
	-- Library: sourced from https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/
	(1001,Null,'Library','.Net Class Library or Assembly, Root Node',0,0),
	(1011,1001,'NameSpace',Null,0,1),
	(1012,1001,'Type','A type is a class, interface, struct, enum, or delegate.',0,1),
	(1013,1001,'Field',Null,0,1),
	(1014,1001,'Property','Includes indexers or other indexed properties.',0,1),
	(1015,1001,'Method','Includes special methods, such as constructors and operators.',0,1),
	(1016,1001,'Event',Null,0,1)

	Select	*
	From	[App_DataDictionary].[ApplicationScope]

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
