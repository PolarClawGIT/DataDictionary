CREATE PROCEDURE [App_DataDictionary].[procSetLibraryMember]
		@ModelId UniqueIdentifier = null,
		@LibraryId UniqueIdentifier = null,
		@Data [App_DataDictionary].[typeLibraryMember] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on LibraryMember.
*/

-- Transaction Handling
Declare	@TRN_IsNewTran Bit = 0 -- Indicates that the stored procedure started the transaction. Used to handle nested Transactions

Begin Try
	-- Begin Transaction
	If @@TranCount = 0
	  Begin -- Not in a nested/distributed transaction, need to start a transaction
		Begin Transaction
		Select	@TRN_IsNewTran = 1
	  End; -- Begin Transaction

	-- Validation
	If @ModelId is Null and @LibraryId is Null
	Throw 50000, '@ModelId or @LibraryId must be specified', 1;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[LibraryId]             UniqueIdentifier Not Null,
		[MemberId]              UniqueIdentifier Not Null,
		[MemberParentId]        UniqueIdentifier Null,
		[MemberName]            [App_DataDictionary].[typeNameSpaceMember] Not Null,
		[MemberType]            [App_DataDictionary].[typeObjectSubType] Not Null,
		[MemberData]            XML Null,
		Primary Key ([MemberId]))

	Declare @NameSpace Table (
		[LibraryId]             UniqueIdentifier Not Null,
		[MemberId]              UniqueIdentifier Not Null,
		[MemberName]		    [App_DataDictionary].[typeNameSpaceMember] Not Null,
		[MemberNameSpace]       [App_DataDictionary].[typeNameSpacePath] Null,
		[ParentNameSpace]       [App_DataDictionary].[typeNameSpacePath] Null,
		Primary Key ([MemberId]))


;With [Existing] As (
	Select	M.[LibraryId],
			N.[MemberId],
			N.[MemberName],
			N.[MemberNameSpace],
			N.[ParentNameSpace]
	From	[App_DataDictionary].[LibraryMember] M
			Left Join [App_DataDictionary].[ModelLibrary] L
			On	M.[LibraryId] = L.[LibraryId]
			Cross Apply [App_DataDictionary].[funcGetMemberName](M.[MemberId]) N
	Where	M.[MemberType] In ('NameSpace','Type') And
			(@ModelId is Null or L.[ModelId] = @ModelId) And
			(@LibraryId is Null or M.[LibraryId] = @LibraryId)),
	[BaseData] As (
		Select	Coalesce(D.[LibraryId], @LibraryId) As [LibraryId],
				IIF(X.[IsBase] = 1, D.[MemberId], NewId()) As [MemberId],
				X.[MemberName],
				X.[NameSpace] As [MemberNameSpace],
				X.[ParentNameSpace],
				Row_Number() Over (
						Partition By 
							Coalesce(D.[LibraryId], @LibraryId),
							X.[MemberName],
							X.[NameSpace]
						Order By IIF(X.[IsBase] = 1,0,1))
						As [RankIndex]
		From	@Data D
				Cross Apply [App_DataDictionary].[funcSplitNameSpace] (D.[MemberNameSpace]) X
		Where	Coalesce(D.[MemberType],'NameSpace') In ('NameSpace','Type')),
	[Data] As (
		Select	[LibraryId],
				[MemberId],
				[MemberName],
				[MemberNameSpace],
				[ParentNameSpace]
		From	[BaseData]
		Where	[RankIndex] = 1)
	Insert Into @NameSpace
	Select	IsNull(X.[LibraryId], D.[LibraryId]) As [LibraryId],
			IsNull(X.[MemberId], D.[MemberId]) As [MemberId],
			IsNull(X.[MemberName], D.[MemberName]) As [MemberName],
			IsNull(X.[MemberNameSpace], D.[MemberNameSpace]) As [MemberNameSpace],
			IsNull(X.[ParentNameSpace], D.[ParentNameSpace]) As [ParentNameSpace]
	From	[Existing] X
			Full Outer Join [Data] D
			On	X.[LibraryId] = D.[LibraryId] And
				X.[MemberNameSpace] = D.[MemberNameSpace]

;With [Data] As (
	Select	Coalesce(D.[LibraryId], @LibraryId) As [LibraryId],
			Coalesce(D.[MemberId], NewId()) As [MemberId],
			Coalesce(P.[MemberId], R.[MemberId]) As [MemberParentId],
			X.[NameSpace] As [MemberNameSpace],
			X.[MemberName] As [MemberName],
			D.[MemberType],
			D.[MemberData]
	From	@Data D
			Cross Apply [App_DataDictionary].[funcSplitNameSpace] (D.[MemberNameSpace]) X
			-- Possible Parents
			Left Join @Data P
			On	D.[MemberParentId] = P.[MemberId]
			Left Join @NameSpace R
			On	X.[ParentNameSpace] = R.[MemberNameSpace]
	Where	X.[IsBase] = 1),
[NameSpace] As (
	Select	N.[LibraryId],
			N.[MemberId],
			P.[MemberId] As [MemberParentId],
			N.[MemberNameSpace],
			N.[MemberName]
	From	@NameSpace N
			Left Join @NameSpace P
			On	N.[ParentNameSpace] = P.[MemberNameSpace])
Insert Into @Values
Select	IsNull(D.[LibraryId], N.[LibraryId]) As [LibraryId],
		IsNull(D.[MemberId], N.[MemberId]) As [MemberId],
		IsNull(D.[MemberParentId], N.[MemberParentId]) As [MemberParentId],
		IsNull(D.[MemberName], N.[MemberName]) As [MemberName],
		IsNull(D.[MemberType], 'NameSpace') As [MemberType],
		D.[MemberData]
From	[Data] D
		Full Outer Join [NameSpace] N
		On	D.[LibraryId] = N.[LibraryId] And
			D.[MemberId] = N.[MemberId]

	-- Apply Changes
	Delete From [App_DataDictionary].[LibraryMember]
	From	[App_DataDictionary].[LibraryMember] T
			Left Join @Values S
			On	T.[MemberId] = S.[MemberId] And
				((T.[MemberParentId] is Null And S.[MemberParentId] is Null) Or
				 (T.[MemberParentId] = S.[MemberParentId]))
	Where	S.[MemberId] is Null And
			T.[LibraryId] In (
			Select	A.[LibraryId]
			From	[App_DataDictionary].[LibrarySource] A
					Left Join [App_DataDictionary].[ModelLibrary] C
					On	A.[LibraryId] = C.[LibraryId]
			Where	(@LibraryId is Null Or @LibraryId = A.[LibraryId]) And
					(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[LibraryMember]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[MemberId],
				[LibraryId],
				[MemberParentId],
				[MemberName],
				[MemberType],
				Convert(NVarChar(Max),[MemberData]) As [MemberData]
		From	@Values
		Except
		Select	[MemberId],
				[LibraryId],
				[MemberParentId],
				[MemberName],
				[MemberType],
				Convert(NVarChar(Max),[MemberData]) As [MemberData]
		From	[App_DataDictionary].[LibraryMember])
	Update [App_DataDictionary].[LibraryMember]
	Set		[LibraryId] = S.[LibraryId],
			[MemberParentId] = S.[MemberParentId],
			[MemberName] = S.[MemberName],
			[MemberType] = S.[MemberType],
			[MemberData] = S.[MemberData]
	From	[App_DataDictionary].[LibraryMember] T
			Inner Join [Delta] S
			On	T.[MemberId] = S.[MemberId]
	Print FormatMessage ('Update [App_DataDictionary].[LibraryMember]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[LibraryMember] (
			[MemberId],
			[LibraryId],
			[MemberParentId],
			[MemberName],
			[MemberType],
			[MemberData])
	Select	S.[MemberId],
			S.[LibraryId],
			S.[MemberParentId],
			S.[MemberName],
			S.[MemberType],
			S.[MemberData]
	From	@Values S
			Left Join [App_DataDictionary].[LibraryMember] T
			On	S.[MemberId] = T.[MemberId]
	Where	T.[MemberId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[LibraryMember]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Commit Transaction
	If @TRN_IsNewTran = 1
	  Begin -- If this is the outer transaction, commit it
		If XAct_State() = -1 Throw 103930, 'The current transaction cannot be committed and cannot support operations that write to the log file. Roll back the transaction. (Msg- 3930)', 100
		Commit Transaction
		Print FormatMessage ('Commit Transaction Issued ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
	  End -- Commit Transaction
	  -- This is a nested transaction, must be committed by outer transaction
	Else Print FormatMessage ('Commit Transaction Pending ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
End Try
Begin Catch
	-- Debug Data
	Print FormatMessage ('*** Error Report: %s ***', Object_Name(@@ProcID))
	Print FormatMessage (' Message- %s', ERROR_MESSAGE())
	Print FormatMessage (' Number- %i', ERROR_NUMBER())
	Print FormatMessage (' Severity- %i', ERROR_SEVERITY())
	Print FormatMessage (' State- %i', ERROR_STATE())
	Print FormatMessage (' Procedure- %s', ERROR_PROCEDURE())
	Print FormatMessage (' Line- %i', ERROR_LINE())
	Print FormatMessage (' @@TranCount - %i', @@TranCount)
	Print FormatMessage (' @@NestLevel - %i', @@NestLevel)
	Print FormatMessage (' Original_Login - %s', Original_Login())
	Print FormatMessage (' Current_User - %s', Current_User)
	Print FormatMessage (' XAct_State - %i', XAct_State())
	Print '*** Debug Report ***'

	Print FormatMessage ('*** End Report: %s ***', Object_Name(@@ProcID))

	-- Rollback Transaction
	If @TRN_IsNewTran = 1
	  Begin -- If this is the outer transaction, roll it back
		Rollback Transaction
		Print FormatMessage ('Rollback Transaction Issued ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
	  End -- Rollback Transaction
	-- This is a nested transaction, must be rolled back by outer transaction
	Else Print FormatMessage ('Rollback Transaction Pending ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))

	If ERROR_SEVERITY() Not In (0, 11) Throw -- Re-throw the Error
End Catch
GO
/*
----------------------------------------------------------
Begin Try;
	Begin Transaction;
	Set NoCount On;

	--Delete From [App_DataDictionary].[LibraryMember]


	declare @p2 App_DataDictionary.typeLibraryMember
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','E5160896-7823-467E-8E5D-637E604EFAFB',NULL,N'Sample.Library',N'[SampleLibrary]',N'SampleLibrary',N'NameSpace',NULL)
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','EB23DDB6-A377-4A86-B972-6256009DFC63','E5160896-7823-467E-8E5D-637E604EFAFB',N'Sample.Library',N'[SampleLibrary].[ISampleArray]',N'ISampleArray',N'Type',N'<member name="T:SampleLibrary.ISampleArray"><summary>              Sample Interface.              </summary></member>')
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','56F00E84-C382-4AC9-9EEA-6CB0825630B5','E5160896-7823-467E-8E5D-637E604EFAFB',N'Sample.Library',N'[SampleLibrary].[SampleClass]',N'SampleClass',N'Type',N'<member name="T:SampleLibrary.SampleClass"><summary>              Sample Class              </summary></member>') 
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','D2B60C6E-651C-409A-A222-341AB0868302','56F00E84-C382-4AC9-9EEA-6CB0825630B5',N'Sample.Library',N'[SampleLibrary].[SampleClass].[InternalClass]',N'InternalClass',N'Type',N'<member name="T:SampleLibrary.SampleClass.InternalClass"><summary>              Internal Class              </summary></member>')  
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','4F561F5B-5DEE-4C48-87FD-56E6D73ACDFC','56F00E84-C382-4AC9-9EEA-6CB0825630B5',N'Sample.Library',N'[SampleLibrary].[SampleClass].[Item]',N'Item',N'Property',N'<member name="P:SampleLibrary.SampleClass.Item(System.Int32)"><summary>              Sample Indexer              </summary><param name="index" /><returns /></member>')  
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','B83C9AB3-1D14-4D0C-9E0D-3BF43F725D9C','4F561F5B-5DEE-4C48-87FD-56E6D73ACDFC',N'Sample.Library',N'[SampleLibrary].[SampleClass].[Item].[index]',N'index',N'Parameter',N'<param name="index" type="System.Int32" />')  
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','98825DAA-E792-4F7C-B17C-B9BA0B6F7621','56F00E84-C382-4AC9-9EEA-6CB0825630B5',N'Sample.Library',N'[SampleLibrary].[SampleClass].[SampleArray]',N'SampleArray',N'Field',N'<member name="F:SampleLibrary.SampleClass.SampleArray"><summary>              Sample Array/Field.              </summary></member>')  
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','DDDD1B2E-7978-4D6B-BD97-6ADF88E587EF','56F00E84-C382-4AC9-9EEA-6CB0825630B5',N'Sample.Library',N'[SampleLibrary].[SampleClass].[SampleClassDelgate]',N'SampleClassDelgate',N'Type',N'<member name="T:SampleLibrary.SampleClass.SampleClassDelgate"><summary>              Sample Class Delegate Function              </summary><param name="value" /><returns /></member>')  
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','BB9DE015-5104-4498-ACB4-B5FC9D2A82BE','56F00E84-C382-4AC9-9EEA-6CB0825630B5',N'Sample.Library',N'[SampleLibrary].[SampleClass].[SampleEvent]',N'SampleEvent',N'Event',N'<member name="E:SampleLibrary.SampleClass.SampleEvent"><summary>              Sample Event              </summary></member>')  
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','177713D1-EFFD-48FE-91CB-E07F1FA17F07','56F00E84-C382-4AC9-9EEA-6CB0825630B5',N'Sample.Library',N'[SampleLibrary].[SampleClass].[SampleGenericMethod``1]',N'SampleGenericMethod``1',N'Method',N'<member name="M:SampleLibrary.SampleClass.SampleGenericMethod``1(``0)"><summary>              Sample generic method              </summary><typeparam name="T" /><param name="data" /></member>')  
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','7052D5BA-01DC-4A94-B643-FDA3255D282F','177713D1-EFFD-48FE-91CB-E07F1FA17F07',N'Sample.Library',N'[SampleLibrary].[SampleClass].[SampleGenericMethod``1].[data]',N'data',N'Parameter',N'<param name="data" type="``0" />')  
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','BC8B4DD5-1D98-4B6A-9063-91888AA2CD7A','56F00E84-C382-4AC9-9EEA-6CB0825630B5',N'Sample.Library',N'[SampleLibrary].[SampleClass].[SampleMethod]',N'SampleMethod',N'Method',N'<member name="M:SampleLibrary.SampleClass.SampleMethod(System.String)"><summary>              Sample Function              </summary><param name="sampleParm01" /><returns /></member>') 
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','9F31ED73-0EB0-4CF9-B503-B0D4BC8CECD4','56F00E84-C382-4AC9-9EEA-6CB0825630B5',N'Sample.Library',N'[SampleLibrary].[SampleClass].[SampleMethod]',N'SampleMethod',N'Method',N'<member name="M:SampleLibrary.SampleClass.SampleMethod(System.String,System.String)"><summary>              Sample Method              </summary><param name="sampleParm01">Value 01</param><param name="samplePar02">Value 02</param></member>')  
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','3E40CD1E-F7D1-40BE-BB30-D0F6EEC0302E','9F31ED73-0EB0-4CF9-B503-B0D4BC8CECD4',N'Sample.Library',N'[SampleLibrary].[SampleClass].[SampleMethod].[samplePar02]',N'samplePar02',N'Parameter',N'<param name="samplePar02" type="System.String">Value 02</param>')  
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','6AFFEC7D-9729-4D72-B35C-873F6503C09D','BC8B4DD5-1D98-4B6A-9063-91888AA2CD7A',N'Sample.Library',N'[SampleLibrary].[SampleClass].[SampleMethod].[sampleParm01]',N'sampleParm01',N'Parameter',N'<param name="sampleParm01" type="System.String" />')  
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','7EB15981-FB1D-49AD-B7D3-06113978D696','9F31ED73-0EB0-4CF9-B503-B0D4BC8CECD4',N'Sample.Library',N'[SampleLibrary].[SampleClass].[SampleMethod].[sampleParm01]',N'sampleParm01',N'Parameter',N'<param name="sampleParm01" type="System.String">Value 01</param>')  
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','0E72A287-D303-4E14-9C0A-86B0CA4DCB1F','56F00E84-C382-4AC9-9EEA-6CB0825630B5',N'Sample.Library',N'[SampleLibrary].[SampleClass].[SampleProperty]',N'SampleProperty',N'Property',N'<member name="P:SampleLibrary.SampleClass.SampleProperty"><summary>              Sample Property              </summary></member>') 
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','48848F00-40A2-4CF2-853B-E13409D3508E','E5160896-7823-467E-8E5D-637E604EFAFB',N'Sample.Library',N'[SampleLibrary].[SampleDelegate]',N'SampleDelegate',N'Type',N'<member name="T:SampleLibrary.SampleDelegate"><summary>              Sample NameSpace Delegate method              </summary><param name="sampleParm" /></member>') 
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','5633FEDF-352A-49EF-BD90-BD497407CFCD','E5160896-7823-467E-8E5D-637E604EFAFB',N'Sample.Library',N'[SampleLibrary].[SampleDelegateNoParm]',N'SampleDelegateNoParm',N'Type',N'<member name="T:SampleLibrary.SampleDelegateNoParm"><summary>              Sample NameSpace Delegate method with No Parameters              </summary></member>')
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','BFBC3D16-E07B-4713-9377-23A42E481281','E5160896-7823-467E-8E5D-637E604EFAFB',N'Sample.Library',N'[SampleLibrary].[SampleFunctionDelegate]',N'SampleFunctionDelegate',N'Type',N'<member name="T:SampleLibrary.SampleFunctionDelegate"><summary>              Sample NameSpace Delegate Function              </summary><returns /></member>')  
	insert into @p2 values('54C98BD6-9BD1-4D67-AB9D-7886276DF399','8A894442-F743-4F0C-AF31-360A7C2AB7D5','E5160896-7823-467E-8E5D-637E604EFAFB',N'Sample.Library',N'[SampleLibrary].[SampleGeneric`1]',N'SampleGeneric`1',N'Type',N'<member name="T:SampleLibrary.SampleGeneric`1"><summary>              Sample Generic Class              </summary><typeparam name="T" /></member>')  
	
	exec [App_DataDictionary].[procSetLibraryMember] @ModelId='71BFEB28-409E-456B-89AF-0260864837B5',@Data=@p2

	exec [App_DataDictionary].[procSetLibraryMember] @ModelId='71BFEB28-409E-456B-89AF-0260864837B5',@Data=@p2

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
*/