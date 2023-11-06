Begin Try;
	Begin Transaction;
	Set NoCount On;

	Delete From [App_DataDictionary].[ApplicationScope]

	Declare @Scope [App_DataDictionary].[typeApplicationScope] 
	Insert Into @Scope Values 
		(Null, 'Database', 'MS SQL Database, Root Node'),
		(Null, 'Library', '.Net Class Library or Assembly, Root Node'),
		(Null, 'Library.Event', NULL),
		(Null, 'Library.Field', NULL),
		(Null, 'Library.Method', 'Includes special methods, such as constructors and operators'),
		(Null, 'Library.NameSpace', NULL),
		(Null, 'Library.Property', 'Includes indexers or other indexed properties'),
		(Null, 'Library.Type', 'A type is a class, interface, struct, enum, or delegate'),
		(Null, 'Database.Schema', NULL),
		(Null, 'Database.Schema.Aggregate', NULL),
		(Null, 'Database.Schema.Default', NULL),
		(Null, 'Database.Schema.Function', NULL),
		(Null, 'Database.Schema.Procedure', NULL),
		(Null, 'Database.Schema.Table', NULL),
		(Null, 'Database.Schema.Type', NULL),
		(Null, 'Database.Schema.View', NULL),
		(Null, 'Database.Schema.View.Column', NULL),
		(Null, 'Database.Schema.View.Index', NULL),
		(Null, 'Database.Schema.Table.Column', NULL),
		(Null, 'Database.Schema.Table.Constraint', NULL),
		(Null, 'Database.Schema.Table.Index', NULL),
		(Null, 'Database.Schema.Procedure.Parameter', NULL),
		(Null, 'Database.Schema.Function.Parameter', NULL)

	Exec [App_DataDictionary].[procSetApplicationScope] @Scope

	-- By default, throw and error and exit without committing
--;	Throw 50000, 'Abort process, comment out this line when ready to actual Commit the transaction',255;
	
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
