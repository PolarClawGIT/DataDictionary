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
		[MemberId]              UniqueIdentifier Not Null,
		[LibraryId]             UniqueIdentifier Not Null,
		[MemberParentId]        UniqueIdentifier Null,
		[MemberName]            [App_DataDictionary].[typeNameSpaceMember] Not Null,
		[MemberType]            [App_DataDictionary].[typeObjectSubType] Not Null,
		[MemberData]            XML Null,
		Primary Key ([MemberId]))

	Declare @NameSpace Table (
		[MemberId]              UniqueIdentifier Not Null,
		[LibraryId]             UniqueIdentifier Not Null,
		[MemberName]		    [App_DataDictionary].[typeNameSpaceMember] Not Null,
		[MemberType]			[App_DataDictionary].[typeObjectSubType] Not Null,
		[MemberNameSpace]       [App_DataDictionary].[typeNameSpacePath] Null,
		--[ParentNameSpace]       [App_DataDictionary].[typeNameSpacePath] Null,
		Primary Key ([MemberId]))

	;With [NameSpace] As (
		-- Existing Values
		Select	M.[MemberId],
				M.[LibraryId],
				M.[MemberName],
				M.[MemberType],
				N.[MemberNameSpace],
				N.[ParentNameSpace]
		From	[App_DataDictionary].[LibraryMember] M
				Cross Apply [App_DataDictionary].[funcGetMemberNameSpace](M.[MemberId]) N
				Left Join [App_DataDictionary].[ModelLibrary] L
				On	M.[LibraryId] = L.[LibraryId]
		Where	(@LibraryId is Null Or @LibraryId = M.[LibraryId]) And
				(@ModelId is Null Or @ModelId = L.[ModelId])),
	[Proposed] As (
		Select	Coalesce(D.[LibraryId], @LibraryId) As [LibraryId],
				IIF(X.[IsBase] = 1, D.[MemberId], Null) As [MemberId],
				X.[NameSpaceMember] As [MemberName],
				D.[MemberType],
				X.[ParentNameSpace] as [MemberNameSpace],
				Row_Number() Over (
					Partition By 
						Coalesce(D.[LibraryId], @LibraryId),
						X.[NameSpaceMember],
						X.[NameSpace]
					Order By IIF(X.[IsBase] = 1,0,1))
					As [RankIndex]
		From	@Data D
				Cross Apply [App_DataDictionary].[funcSplitNameSpace] (
					IIF(D.[MemberNameSpace] is Null, D.[MemberName], FormatMessage('%s.%s',D.[MemberNameSpace], D.[MemberName]))) X
				Left Join @Data P
				On	Coalesce(D.[LibraryId], @LibraryId) = Coalesce(P.[LibraryId], @LibraryId) And
					D.[MemberParentId] = P.[MemberId]
		Where	Coalesce(D.[MemberType],'NameSpace') In ('NameSpace','Type'))
	Insert Into @NameSpace
	Select	[MemberId],
			[LibraryId],
			[MemberName],
			[MemberType],
			[MemberNameSpace]
	From	[NameSpace]
	Union
	Select	Coalesce(N.[MemberId], P.[MemberId], NewId()) As [MemberId],
			P.[LibraryId],
			P.[MemberName],
			P.[MemberType],
			P.[MemberNameSpace]
	From	[Proposed] P
			Left Join [NameSpace] N
			On	P.[LibraryId] = N.[LibraryId] And
				P.[MemberName] = N.[MemberName] And
				P.[MemberNameSpace] = N.[MemberNameSpace]
	Where	P.[RankIndex] = 1
--Select	'@NameSpace', * From	@NameSpace

	;With [Data] As (
		Select	D.[MemberId],
				D.[LibraryId],
				D.[MemberParentId],
				D.[AssemblyName],
				X.[NameSpace] As [MemberNameSpace],
				D.[MemberName],
				D.[MemberType],
				D.[MemberData]
		From	@Data D
				Cross Apply [App_DataDictionary].[funcSplitNameSpace] (D.[MemberNameSpace]) X
		Where	X.[IsBase] = 1),
	[Combine] As ( -- Catches any missing NameSpaces
		Select	Coalesce(D.[MemberId], N.[MemberId]) As [MemberId],
				Coalesce(D.[LibraryId], N.[LibraryId]) As [LibraryId],
				D.[MemberParentId],
				Coalesce(D.[MemberNameSpace], N.[MemberNameSpace]) As [MemberNameSpace],
				Coalesce(D.[MemberName], N.[MemberName]) As [MemberName],
				Coalesce(D.[MemberType], N.[MemberType]) As [MemberType],
				D.[MemberData]
		From	[Data] D
				Full Outer Join @NameSpace N
				On	D.[LibraryId] = N.[LibraryId] And
					D.[MemberNameSpace] = N.[MemberNameSpace] And
					D.[MemberName] = N.[MemberName])
	Insert Into @Values
	Select	C.[MemberId],
			C.[LibraryId],
			IsNull(P.[MemberId], C.[MemberParentId]) As [MemberParentId],
			--C.[MemberNameSpace],
			C.[MemberName],
			C.[MemberType],
			C.[MemberData]
	From	[Combine] C
			Left Join @NameSpace P
			On	C.[MemberNameSpace] = IIF(P.[MemberNameSpace] is Null,
					FormatMessage('[%s]',P.[MemberName]), FormatMessage('%s.[%s]',P.[MemberNameSpace],P.[MemberName]))
--Select '@Values', * From @Values

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

----------------------------------------------------------
/*
Begin Try;
	Begin Transaction;
	Set NoCount On;

	declare @Lbirary App_DataDictionary.typeLibrarySource
	insert into @Lbirary values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF',N'DataDictionary.DataLayer',NULL,N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.xml','2024-04-03 11:40:16.9306344')

	exec [App_DataDictionary].[procSetLibrarySource] @LibraryId='CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF',@Data=@Lbirary


	declare @Member App_DataDictionary.typeLibraryMember
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','F3099983-D9C7-4F7A-A63B-409C2C2AE19E',NULL,N'DataDictionary.DataLayer',NULL,N'DataDictionary',N'NameSpace',NULL)
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','33E30882-A691-43C7-B298-CE51B4488DD7','F3099983-D9C7-4F7A-A63B-409C2C2AE19E',N'DataDictionary.DataLayer',N'DataDictionary',N'DataLayer',N'NameSpace',NULL)
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','5A57EB1F-C7A6-4DAC-AA07-6CF070094A52','33E30882-A691-43C7-B298-CE51B4488DD7',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer',N'ApplicationData',N'NameSpace',NULL)
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','F9103FBD-4136-405E-8C7A-843E0AE39497','5A57EB1F-C7A6-4DAC-AA07-6CF070094A52',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.ApplicationData',N'Help',N'NameSpace',NULL)
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','66C1B111-F0F3-47CA-B203-29C141C47079','F9103FBD-4136-405E-8C7A-843E0AE39497',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.ApplicationData.Help',N'HelpCollection`1',N'Type',N'&lt;member name="T:DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1"&gt;&lt;summary&gt;
				Generic Base class for Help Items.
				&lt;/summary&gt;&lt;typeparam name="TItem"&gt;&lt;/typeparam&gt;&lt;remarks&gt;Base class, implements the Read and Write.&lt;/remarks&gt;&lt;/member&gt;')
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','AC9CAEBD-047D-4E27-AC92-836CD9C2DA75','66C1B111-F0F3-47CA-B203-29C141C47079',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1',N'LoadCommand',N'Method',N'&lt;member name="M:DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1.LoadCommand(Toolbox.DbContext.IConnection,DataDictionary.DataLayer.ApplicationData.Help.IHelpKey)"&gt;&lt;inheritdoc /&gt;&lt;/member&gt;')
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','CA94003F-67F2-4C1C-A66C-E2411BE414A1','AC9CAEBD-047D-4E27-AC92-836CD9C2DA75',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1.LoadCommand',N'parameter00',N'Parameter',N'&lt;param type="Toolbox.DbContext.IConnection" /&gt;')
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','ED856F1C-13BB-4E0C-93AF-A147F5DDB5DA','AC9CAEBD-047D-4E27-AC92-836CD9C2DA75',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1.LoadCommand',N'parameter01',N'Parameter',N'&lt;param type="DataDictionary.DataLayer.ApplicationData.Help.IHelpKey" /&gt;')
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','16A468E9-58D8-4581-A62D-9C4549B6D34B','66C1B111-F0F3-47CA-B203-29C141C47079',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1',N'LoadCommand',N'Method',N'&lt;member name="M:DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1.LoadCommand(Toolbox.DbContext.IConnection)"&gt;&lt;inheritdoc /&gt;&lt;/member&gt;')
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','B96090F4-3C76-4502-9263-63C24418FD4C','16A468E9-58D8-4581-A62D-9C4549B6D34B',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1.LoadCommand',N'parameter00',N'Parameter',N'&lt;param type="Toolbox.DbContext.IConnection" /&gt;')
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','14E085A6-EAA6-441F-AC8D-D01162DF5DC3','66C1B111-F0F3-47CA-B203-29C141C47079',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1',N'SaveCommand',N'Method',N'&lt;member name="M:DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1.SaveCommand(Toolbox.DbContext.IConnection)"&gt;&lt;inheritdoc /&gt;&lt;/member&gt;')
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','0B89D4AF-89FA-486F-B524-1CA6DCFE4767','14E085A6-EAA6-441F-AC8D-D01162DF5DC3',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1.SaveCommand',N'parameter00',N'Parameter',N'&lt;param type="Toolbox.DbContext.IConnection" /&gt;')
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','0844F17B-59CE-452C-8FBD-A88A38340FB4','66C1B111-F0F3-47CA-B203-29C141C47079',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1',N'SaveCommand',N'Method',N'&lt;member name="M:DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1.SaveCommand(Toolbox.DbContext.IConnection,DataDictionary.DataLayer.ApplicationData.Help.IHelpKey)"&gt;&lt;inheritdoc /&gt;&lt;/member&gt;')
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','D358DEB7-44BD-479E-9138-E39514B0A307','0844F17B-59CE-452C-8FBD-A88A38340FB4',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1.SaveCommand',N'parameter00',N'Parameter',N'&lt;param type="Toolbox.DbContext.IConnection" /&gt;')
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','24FE0A5C-5CE9-4C6E-A7C6-6E667E064053','0844F17B-59CE-452C-8FBD-A88A38340FB4',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1.SaveCommand',N'parameter01',N'Parameter',N'&lt;param type="DataDictionary.DataLayer.ApplicationData.Help.IHelpKey" /&gt;')
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','93FB77B4-C1D6-4D58-877B-BF51EEEB8D0D','66C1B111-F0F3-47CA-B203-29C141C47079',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1',N'Validate',N'Method',N'&lt;member name="M:DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1.Validate"&gt;&lt;inheritdoc /&gt;&lt;/member&gt;')
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','0276E86D-5D4C-4012-956D-C414A07F54D4','66C1B111-F0F3-47CA-B203-29C141C47079',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1',N'Remove',N'Method',N'&lt;member name="M:DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1.Remove(DataDictionary.DataLayer.ApplicationData.Help.IHelpKey)"&gt;&lt;inheritdoc /&gt;&lt;/member&gt;')
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','5E4D937B-B019-4454-8AA2-1448C8A2AAC2','0276E86D-5D4C-4012-956D-C414A07F54D4',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.ApplicationData.Help.HelpCollection`1.Remove',N'parameter00',N'Parameter',N'&lt;param type="DataDictionary.DataLayer.ApplicationData.Help.IHelpKey" /&gt;')
	insert into @Member values('CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF','417E11D8-6FE9-40F2-A0CC-11CCD71489A4','F9103FBD-4136-405E-8C7A-843E0AE39497',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.ApplicationData.Help',N'HelpCollection',N'Type',N'&lt;member name="T:DataDictionary.DataLayer.ApplicationData.Help.HelpCollection"&gt;&lt;summary&gt;
				Default List/Collection of Help Items.
				&lt;/summary&gt;&lt;/member&gt;')

	exec [App_DataDictionary].[procSetLibraryMember] @LibraryId='CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF',@Data=@Member

	Exec [App_DataDictionary].[procGetLibraryMember] @LibraryId='CB6BC858-6A6B-4CBD-AF7C-FCD0B579D4DF'



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

*/