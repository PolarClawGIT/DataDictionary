Begin Try;
	Begin Transaction;
	Set NoCount On;


	Declare @ModelId UniqueIdentifier = (Select [ModelId] From [AppModel].[Model] Where [ModelTitle] = 'Unit Test'),
			@TemplateId UniqueIdentifier = Null,
			@Data [AppScript].[udttTemplate],
			@Empty [AppScript].[udttTemplate]

	Insert Into @Data ([TemplateId], [TemplateTitle], [TemplateDescription])
	Values (@TemplateId, 'Unit Test', 'Testing'),
			(NewId(),'Test 2', Null)



	Print '--- Add Without Model or ID --'
	Exec [AppScript].[procSetTemplate] @ModelId = Null, @TemplateId = @TemplateId, @Data = @Data

	Print '-- Delete Template --'
	Set @TemplateId = (Select [TemplateId] From [AppScript].[Template] Where [TemplateTitle] In (Select [TemplateTitle] From @Data Where [TemplateId] is Null))
	Exec [AppScript].[procSetTemplate] @ModelId = Null, @TemplateId = @TemplateId, @Data = @Empty
	
	Print '-- Add to Model --'
	Set @TemplateId = newId()
	Update @Data Set [TemplateId] = @TemplateId Where [TemplateId] is Null

	Exec [AppScript].[procSetTemplate] @ModelId = Null, @TemplateId = Null, @Data = @Data
	Exec [AppScript].[procSetTemplate] @ModelId = @ModelId, @TemplateId = Null, @Data = @Data

	Print '-- Remove From Model --'
	Exec [AppScript].[procSetTemplate] @ModelId = @ModelId, @TemplateId = Null, @Data = @Empty

	Print '-- Update Template --'
	Update @Data Set [TemplateDescription] = 'Update Testing'
	Exec [AppScript].[procSetTemplate] @ModelId = Null, @TemplateId = Null, @Data = @Data

	Print '-- Remove Template --'
	Exec [AppScript].[procSetTemplate] @ModelId = Null, @TemplateId = @TemplateId, @Data = @Empty

	Select	*
	From	[AppScript].[TemplateHS] For System_Time All

	Select	*
	From	[AppScript].[TemplateModel]
	/*
	Delete From @Data
	Exec [AppScript].[procSetTemplate] @ModelId = @ModelId, @TemplateId = @TemplateId, @Data = @Data

	Select	*
	From	[AppScript].[TemplateHS] For System_Time All
	*/
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