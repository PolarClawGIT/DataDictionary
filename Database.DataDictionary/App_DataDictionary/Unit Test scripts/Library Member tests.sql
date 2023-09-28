Begin Try;
	Begin Transaction;
	Set NoCount On;

	Delete From [App_DataDictionary].[LibraryMember]
	Delete From [App_DataDictionary].[LibraryNameSpace]
	Delete From [App_DataDictionary].[LibrarySource]

	Declare @ModelId UniqueIdentifier = '00000000-0000-0000-0010-000000000100'
	Declare @Model [App_DataDictionary].[typeModel]
	Declare @Source [App_DataDictionary].[typeLibrarySource]
	Declare @Member [App_DataDictionary].[typeLibraryMember]

	Insert Into @Model Values
	(@ModelId,'Test', Null, Null, Null)

	Insert Into @Source Values
	(Null,'Test',Null, 'DataDictionary.BusinessLayer', 'DataDictionary.BusinessLayer.xml', Null, Null)

	Insert Into @Member Values
	(Null,'DataDictionary.BusinessLayer','DataDictionary.BusinessLayer','DbSchemaContext','T',Null),
	(Null,'DataDictionary.BusinessLayer','DataDictionary.BusinessLayer.DbSchemaContext.CatalogName','CatalogName','P',Null),
	(Null,'DataDictionary.BusinessLayer','DataDictionary.BusinessLayer.DbWorkItem.GetInformationSchema`1','GetInformationSchema','T',Null),
	(Null,'DataDictionary.BusinessLayer','DataDictionary.BusinessLayer','ModelData','T',Null),
	(Null,'DataDictionary.BusinessLayer','DataDictionary.BusinessLayer.ModelData','ModelKey','P',Null),
	(Null,'DataDictionary.BusinessLayer','DataDictionary.BusinessLayer.ModelData','Models','P',Null)

	Exec [App_DataDictionary].[procSetModel] @ModelId, @Model
	Exec [App_DataDictionary].[procSetLibrarySource] @ModelId, @Source
	Exec [App_DataDictionary].[procSetLibraryMember] @ModelId, @Member


	Exec [App_DataDictionary].[procGetLibrarySource]
	Exec [App_DataDictionary].[procGetLibraryMember]



	Select	*
	From	[App_DataDictionary].[LibrarySource]

	Select	*
	From	[App_DataDictionary].[ModelLibrary]

	Select	*
	From	[App_DataDictionary].[LibraryNameSpace]

	Select	*
	From	[App_DataDictionary].[LibraryMember]
	

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
