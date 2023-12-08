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
		[MemberName]            [App_DataDictionary].[typeAliasElement] Not Null,
		[ScopeId]               Int Not Null,
		[MemberType]            NVarChar(10) Null,
		[MemberData]            XML Null,
		Primary Key ([MemberId]))

	Declare @NameSpace Table (
		[MemberId]              UniqueIdentifier Not Null,
		[LibraryId]             UniqueIdentifier Not Null,
		[MemberName]		    [App_DataDictionary].[typeAliasElement] Not Null,
		[NameSpace]             [App_DataDictionary].[typeAliasName] Not Null
		Primary Key ([MemberId]))

	Declare @Dummy Table (
		[MemberId]              UniqueIdentifier Not Null,
		[LibraryId]             UniqueIdentifier Not Null,
		[MemberName]		    [App_DataDictionary].[typeAliasElement] Not Null,
		[NameSpace]             [App_DataDictionary].[typeAliasName] Not Null)

	;With [NameSpace] As (
		Select	[LibraryId],
				[MemberId],
				[MemberParentId],
				[MemberName],
				Convert(NVarChar(max),FormatMessage('[%s]', [MemberName])) As [NameSpace]
		From	[App_DataDictionary].[LibraryMember]
		Where	[MemberParentId] is Null
		Union All
		Select	P.[LibraryId],
				C.[MemberId],
				C.[MemberParentId],
				C.[MemberName],
				Convert(NVarChar(max),FormatMessage('%s.[%s]', P.[NameSpace], C.[MemberName])) As [NameSpace]
		From	[NameSpace] P
				Inner Join [App_DataDictionary].[LibraryMember] C
				On	P.[LibraryId] = C.[LibraryId] And
					P.[MemberId] = C.[MemberParentId]),
	[Alias] As (
		Select	Distinct
				Coalesce(D.[LibraryId], @LibraryId) As [LibraryId],
				IIF(X.[IsBase] = 1, D.[MemberId], Null) As [MemberId],
				IIF(X.[IsBase] = 1, D.[MemberName], X.[AliasElement]) As [MemberName],
				X.[AliasName] As [NameSpace]
		From	@Data D
				Cross Apply [App_DataDictionary].[funcSplitAliasName] (D.[NameSpace]) X)
	Insert Into @NameSpace
	Select	Distinct
			Coalesce(N.[MemberId], A.[MemberId], NewId()) As [MemberId],
			IsNull(N.[LibraryId],A.[LibraryId]) As [LibraryId],
			IsNull(N.[MemberName], A.[MemberName]) As [MemberName],
			IsNull(N.[NameSpace], A.[NameSpace]) As [NameSpace]
	From	[NameSpace] N
			Full Outer Join [Alias] A
			On	N.[LibraryId] = A.[LibraryId] And
				N.[NameSpace] = A.[NameSpace]

	;With [Scope] As (
		Select	S.[ScopeId],
				F.[ScopeName]
		From	[App_DataDictionary].[ApplicationScope] S
				Cross Apply [App_DataDictionary].[funcGetScopeName](S.[ScopeId]) F),
	[Data] As (
		Select	Coalesce(D.[LibraryId], @LibraryId) As [LibraryId],
				X.[ParentAliasName] As [ParentNameSpace],
				X.[AliasName] As [NameSpace],
				Row_Number() Over (
					Partition By Coalesce(D.[LibraryId], @LibraryId), X.[AliasName]
					Order By D.[NameSpace])
					As [RankIndex],
				IIF(X.[IsBase] = 1, D.[MemberType], 'N') As [MemberType],
				IIF(X.[IsBase] = 1, D.[ScopeName], 'Library.NameSpace') As [ScopeName],
				IIF(X.[IsBase] = 1, D.[MemberData], Null) As [MemberData]
		From	@Data D
				Cross Apply [App_DataDictionary].[funcSplitAliasName] (D.[NameSpace]) X )
	Insert Into @Values
	Select	N.[MemberId],
			N.[LibraryId],
			P.[MemberId] As [MemberParentId],
			N.[MemberName],
			S.[ScopeId],
			D.[MemberType],
			D.[MemberData]
	From	[Data] D
			Inner Join @NameSpace N
			On	D.[LibraryId] = N.[LibraryId] And
				D.[NameSpace] = N.[NameSpace]
			Left Join @NameSpace P
			On	N.[LibraryId] = P.[LibraryId] And
				D.[ParentNameSpace] = P.[NameSpace]
			Left Join [Scope] S
			On	D.[ScopeName] = S.[ScopeName]
	Where	D.[RankIndex] = 1

	-- Apply Changes
	Delete From [App_DataDictionary].[LibraryMember]
	From	[App_DataDictionary].[LibraryMember] T
			Left Join @Values S
			On	T.[MemberId] = S.[MemberId]
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
				[ScopeId],
				[MemberType],
				Convert(NVarChar(Max),[MemberData]) As [MemberData]
		From	@Values
		Except
		Select	[MemberId],
				[LibraryId],
				[MemberParentId],
				[MemberName],
				[ScopeId],
				[MemberType],
				Convert(NVarChar(Max),[MemberData]) As [MemberData]
		From	[App_DataDictionary].[LibraryMember])
	Update [App_DataDictionary].[LibraryMember]
	Set		[LibraryId] = S.[LibraryId],
			[MemberParentId] = S.[MemberParentId],
			[MemberName] = S.[MemberName],
			[ScopeId] = S.[ScopeId],
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
			[ScopeId],
			[MemberType],
			[MemberData])
	Select	S.[MemberId],
			S.[LibraryId],
			S.[MemberParentId],
			S.[MemberName],
			S.[ScopeId],
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

	Declare @AssemblyName NVarChar(100) = 'DataDictionary.DataLayer',
			@LibraryId UniqueIdentifier = newid()
	Declare @Source [App_DataDictionary].[typeLibrarySource]
	Declare @Member [App_DataDictionary].[typeLibraryMember]

	Insert Into @Source	Values
		(Null,'Test','Test Item',@AssemblyName,'Library','SomeFileName.XML',GetDate())

	Insert Into @Member	Values
		(Null, Null, Null, @AssemblyName, 'DataDictionary.DataLayer.ApplicationData.Help.IHelpItem', 'IHelpItem', 'Library.Type', 'T', Null),
		(Null, NewId(), Null, @AssemblyName, 'DataDictionary.DataLayer.ApplicationData.Help.IHelpItem.HelpSubject', 'HelpSubject', 'Library.Property', 'P', Null),
		(Null, Null, Null, @AssemblyName, 'DataDictionary.DataLayer.ApplicationData.Help.IHelpItem.HelpText', 'HelpText', 'Library.Property', 'P', Null),
		(Null, Null, Null, @AssemblyName, 'DataDictionary.DataLayer.ApplicationData.Help.IHelpItem.BadValue', 'BadValue', 'Library.Property', 'P', Null),
		(Null, NewId(), Null, @AssemblyName, 'DataDictionary.DataLayer.ApplicationData.Help.HelpItemExtension', 'HelpItemExtension', 'Library.Type', 'T', Null)

	Exec [App_DataDictionary].[procSetLibrarySource] Null, @LibraryId, @Source

	Insert Into [App_DataDictionary].[LibraryMember] Values
		(NewId(), @LibraryId, Null, 'DataDictionary',17,Null,Null),
		(NewId(), @LibraryId, Null, 'BadNameSpace',17,Null,Null)

	Exec [App_DataDictionary].[procSetLibraryMember] Null, @LibraryId, @Member

	Insert Into @Member Values
		(Null, Null, Null, @AssemblyName, 'DataDictionary.DataLayer.ApplicationData.Help.IHelpItem.HelpId', 'HelpId', 'Library.Property', 'P', Null),
		(Null, Null, Null, @AssemblyName, 'DataDictionary.DataLayer.ApplicationData.Help.IHelpItem.HelpSubject', 'HelpSubject', 'Library.Property', 'P', Null)

	Delete From @Member
	Where	[MemberName] = 'BadValue'

	Exec [App_DataDictionary].[procSetLibraryMember] Null, @LibraryId, @Member

	Select	*
	From	[App_DataDictionary].[LibraryMember]
	/*
	Select	*
	From	[App_DataDictionary].[ApplicationScope] S
			Cross Apply [App_DataDictionary].[funcGetScopeName](S.[ScopeId]) X
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
*/