Begin Try;
	Begin Transaction;
	Set NoCount On;

	Delete From [App_DataDictionary].[LibraryMember]
	Delete From [App_DataDictionary].[LibraryNameSpace]
	Delete From [App_DataDictionary].[LibrarySource]

	Declare @ModelId UniqueIdentifier = '00000000-0000-0000-0010-000000000100'
	Declare @LibraryId UniqueIdentifier = null
	Declare @Model [App_DataDictionary].[typeModel]
	Declare @Source [App_DataDictionary].[typeLibrarySource]
	Declare @Member [App_DataDictionary].[typeLibraryMember]
	Declare @AssemblyName NVarChar(1023) = 'Test.Data'
	Declare @DeleteAssemblyName NVarChar(1023) = 'DeleteMe'
	Declare @DeleteMemberName NVarChar(500) = 'DeleteMe'

	Insert Into @Model Values
	(@ModelId,'Test', Null, Null, Null)

	Insert Into @Source Values
	(Null,'Test',Null, @AssemblyName, 'DataDictionary.BusinessLayer.xml', Null, Null),
	(Null,'TestDelete',Null, @DeleteAssemblyName, 'DataDictionary.BusinessLayer.xml', Null, Null)

	Insert Into @Member Values
	(Null, @AssemblyName, 'DataDictionary.BusinessLayer','DbSchemaContext','T',Null),
	(Null, @AssemblyName, 'DataDictionary.BusinessLayer.DbSchemaContext.CatalogName','CatalogName','P',Null),
	(Null, @AssemblyName, 'DataDictionary.BusinessLayer.DbWorkItem.GetInformationSchema`1','GetInformationSchema','T',Null),
	(Null, @AssemblyName, 'DataDictionary.BusinessLayer','ModelData','T',Null),
	(Null, @AssemblyName, 'DataDictionary.BusinessLayer.ModelData','ModelKey','P',Null),
	(Null, @AssemblyName, 'DataDictionary.BusinessLayer.ModelData','Models','P',Null),
	(Null, @AssemblyName, 'DataDictionary.BusinessLayer.ModelData',@DeleteMemberName,'P',Null),

	(Null, @DeleteAssemblyName, 'DataDictionary.BusinessLayer','DbSchemaContext','T',Null),
	(Null, @DeleteAssemblyName, 'DataDictionary.BusinessLayer.DbSchemaContext.CatalogName','CatalogName','P',Null),
	(Null, @DeleteAssemblyName, 'DataDictionary.BusinessLayer.DbWorkItem.GetInformationSchema`1','GetInformationSchema','T',Null),
	(Null, @DeleteAssemblyName, 'DataDictionary.BusinessLayer','ModelData','T',Null),
	(Null, @DeleteAssemblyName, 'DataDictionary.BusinessLayer.ModelData','ModelKey','P',Null),
	(Null, @DeleteAssemblyName, 'DataDictionary.BusinessLayer.ModelData','Models','P',Null)

	Exec [App_DataDictionary].[procSetModel] @ModelId, @Model

	Exec [App_DataDictionary].[procSetLibrarySource] @ModelId, @LibraryId, @Source
	Exec [App_DataDictionary].[procSetLibraryMember] @ModelId, @LibraryId, @Member

	Select	@LibraryId = [LibraryId]
	From	[App_DataDictionary].[LibrarySource]
	Where	[AssemblyName] = @DeleteAssemblyName

	Delete	From @Source
	Where	[AssemblyName] = @DeleteAssemblyName

	Exec [App_DataDictionary].[procSetLibrarySource] @ModelId, @LibraryId, @Source

	Delete From @Member Where [MemberName] = @DeleteMemberName Or [AssemblyName] = @DeleteAssemblyName

	Exec [App_DataDictionary].[procSetLibraryMember] @ModelId, @LibraryId, @Member

	Select	@LibraryId = [LibraryId]
	From	[App_DataDictionary].[LibrarySource]
	Where	[AssemblyName] = @AssemblyName

	Exec [App_DataDictionary].[procSetLibraryMember] @ModelId, @LibraryId, @Member


--	Exec [App_DataDictionary].[procGetLibrarySource]
--	Exec [App_DataDictionary].[procGetLibraryMember]

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
