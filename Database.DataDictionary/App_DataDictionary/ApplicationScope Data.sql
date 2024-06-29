Begin Try;
	Begin Transaction;
	Set NoCount On;

Throw 50000, 'Scope is now Application Only',255;

	--Delete From [App_DataDictionary].[ApplicationScope]

	Declare @Scope [App_DataDictionary].[typeApplicationScope] 
	Insert Into @Scope Values 
		(Null, 'Database', 'MS SQL Database, Root Node'),
		(Null, 'Library', '.Net Class Library or Assembly, Root Node'),
		(Null, 'Library.Event', NULL),
		(Null, 'Library.Field', NULL),
		(Null, 'Library.Method', 'Includes special methods, such as constructors and operators'),
		(Null, 'Library.NameSpace', NULL),
		(Null, 'Library.Property', 'Includes indexers or other indexed properties'),
		(Null, 'Library.Parameter', NULL),
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
		(Null, 'Database.Schema.Function.Parameter', NULL),
		(Null, 'Model', 'Application Data Model, Root Node'),
		(Null, 'Model.Entity', Null),
		(Null, 'Model.Entity.Alias', NULL),
		(Null, 'Model.Entity.Property', NULL),
		(Null, 'Model.Entity.Attribute', NULL),
		(Null, 'Model.Attribute', NULL),
		(Null, 'Model.Attribute.Alias', NULL),
		(Null, 'Model.Attribute.Property', NULL),
		(Null, 'Model.Relationship', NULL),
		(Null, 'Model.Relationship.Alias', NULL),
		(Null, 'Model.Relationship.Property', NULL),
		(Null, 'Model.Relationship.Entity', NULL),
		(Null, 'Model.Relationship.Attribute', NULL),
		(Null, 'Model.Process', NULL),
		(Null, 'Model.Process.Alias', NULL),
		(Null, 'Model.Process.Property', NULL),
		(Null, 'Model.Process.Flow', NULL),
		(Null, 'Model.Process.Flow.Inflow', NULL),
		(Null, 'Model.Process.Flow.OutFlow', NULL),
		(Null, 'Scripting', NULL),
		(Null, 'Scripting.Transform', NULL),
		(Null, 'Scripting.Schema', NULL),
		(Null, 'Scripting.Schema.Element', NULL)

	Exec [App_DataDictionary].[procSetApplicationScope] @Scope

	Select	*
	From	[App_DataDictionary].[ApplicationScope]

	Select	S.[ScopeId],
			F.[ScopeName]
	From	[App_DataDictionary].[ApplicationScope] S
			Cross Apply [App_DataDictionary].[funcGetScopeName](S.[ScopeId]) F
	Order By F.[ScopeName]

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
