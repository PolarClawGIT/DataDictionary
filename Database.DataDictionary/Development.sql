Begin Try;
	Begin Transaction;
	Set NoCount On;

	Declare	@TemplateId UniqueIdentifier = newid(),
			@Template [AppScript].[udttTemplate],
			@Node [AppScript].[udttTemplateNode],
			@Parent [AppScript].[udttTemplateNodeOwner]

	Insert Into @Template([TemplateId], [TemplateTitle])
	Values (@TemplateId, 'Test Script')

	Insert Into @Node ([TemplateId], [NodeId], [NodeName], [RenderValueAs])
	Values	(@TemplateId, newid(), 'Root', 'Element'),
			(@TemplateId, newid(), 'Child1', 'Element'),
			(@TemplateId, newid(), 'Child2', 'Element'),
			(@TemplateId, newid(), 'GrandChild2', 'Element'),
			(@TemplateId, newid(), 'GrandChild3', 'Element')

	Insert Into @Parent ([TemplateId], [NodeId], [NodeOwnerPath])
	Select	@TemplateId As [TemplateId],
			[NodeId],
			'[Root]'
	From	@Node
	Where	[NodeName] In ('Child1','Child2')
	Union
	Select	@TemplateId As [TemplateId],
			[NodeId],
			'[Root].[Child2]'
	From	@Node
	Where	[NodeName] In ('GrandChild2')
	Union
	Select	@TemplateId As [TemplateId],
			[NodeId],
			'[Root].[MissingChild]'
	From	@Node
	Where	[NodeName] In ('GrandChild2')
	Union
	Select	@TemplateId As [TemplateId],
			[NodeId],
			'[Root].[Child1]'
	From	@Node
	Where	[NodeName] In ('GrandChild3')
	Union
	Select	@TemplateId As [TemplateId],
			[NodeId],
			'[Root].[Child2]'
	From	@Node
	Where	[NodeName] In ('GrandChild3')

	Exec [AppScript].[procSetTemplate] @TemplateId = @TemplateId, @Data = @Template
	Exec [AppScript].[procSetTemplateNode] @TemplateId = @TemplateId, @Data = @Node
	Exec [AppScript].[procSetTemplateNodeOwner] @TemplateId = @TemplateId, @Data = @Parent

--Select	'Debug', * From	[AppScript].[TemplateNodeHS]

Select	'Debug', * From	[AppScript].[TemplateNodeOwnerHS]

	--Delete From @Node
	--Where	[NodeName] In ('Child2')

	--Exec [AppScript].[procSetTemplateNode] @TemplateId = @TemplateId, @Data = @Node
	--Exec [AppScript].[procSetTemplateNodeOwner] @TemplateId = @TemplateId, @Data = @Parent


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
	--Throw;
End Catch;
