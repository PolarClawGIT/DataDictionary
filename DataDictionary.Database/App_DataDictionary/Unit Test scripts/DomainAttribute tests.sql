Begin Try;
	Begin Transaction;
	Set NoCount On;

	Declare @ModelId UniqueIdentifier,
			@AttributeId UniqueIdentifier,
			@Date DateTime2

	Exec [App_DataDictionary].[procSetApplicationModel] @ModelId = null, @ModelTitle = 'Unit Test', @ModelDescription = Null, @Obsolete = 0, @SysStart = null

	Select	@ModelId = [ModelId],
			@Date = GetDate()
	From	[App_DataDictionary].[ApplicationModel]


	Exec [App_DataDictionary].[procSetApplicationModel] @ModelId = @ModelId, @ModelTitle = 'Update', @ModelDescription = Null, @Obsolete = 1, @SysStart = null


	Declare @Data [App_DataDictionary].[typeDomainAttribute]
	Insert Into @Data Values (Null,Null,'Attribute 1', Null, 0, null)
	Insert Into @Data Values (Null,Null,'Attribute 2', Null, 0, null)

	Exec [App_DataDictionary].[procSetDomainAttribute] @ModelId = @ModelId, @Data = @Data

	Select	@AttributeId = [AttributeId]
	From	[App_DataDictionary].[DomainAttribute]
	Where	[AttributeTitle] = 'Attribute 1'

	Declare @Alias [App_DataDictionary].[typeDomainAttributeAlias]
	Insert Into @Alias Values (@AttributeId,'Catalog1','Shcema1','Object1','Element1', Null)

	Exec [App_DataDictionary].[procSetDomainAttributeAlias] @ModelId = @ModelId, @Data = @Alias

	Declare @Property [App_DataDictionary].[typeDomainAttributeProperty]
	Insert Into @Property Values (@AttributeId,'My Stuff','Some Text', null)

	Exec [App_DataDictionary].[procSetDomainAttributeProperty] @ModelId = @ModelId, @Data = @Property

	Select	*
	From	[App_DataDictionary].[ApplicationModel]

	Select	*
	From	[App_DataDictionary].[DomainAttribute]

	Select	*
	From	[App_DataDictionary].[ApplicationAttribute]

	Select	*
	From	[App_DataDictionary].[DomainAttributeAlias]

	Select	*
	From	[App_DataDictionary].[DomainAttributeProperty]

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
