CREATE PROCEDURE [App_DataDictionary].[procSetLibrarySource]
		@ModelId UniqueIdentifier = null,
		@LibraryId UniqueIdentifier = null,
		@Data [App_DataDictionary].[typeLibrarySource] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on LibrarySource.
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
		[LibraryTitle]          [App_DataDictionary].[typeTitle] Not Null,
		[LibraryDescription]    [App_DataDictionary].[typeDescription] Null,
		[AssemblyName]          NVarChar(128) Not Null, -- Natural Key
		[ScopeId]               Int Not Null,
		[SourceFile]            NVarChar(500) Not Null, 
		[SourceDate]            DateTime2 (7) Not Null,
	Primary Key ([LibraryId]))

	;With [Scope] As (
		Select	S.[ScopeId],
				F.[ScopeName]
		From	[App_DataDictionary].[ApplicationScope] S
				Cross Apply [App_DataDictionary].[funcGetScopeName](S.[ScopeId]) F)
	Insert Into @Values
	Select	Coalesce(A.[LibraryId], D.[LibraryId], @LibraryId, NewId()) As [LibraryId],
			NullIf(Trim(D.[LibraryTitle]),'') As [LibraryTitle],
			NullIf(Trim(D.[LibraryDescription]),'') As [LibraryDescription],
			NullIf(Trim(D.[AssemblyName]),'') As [AssemblyName],
			S.[ScopeId],
			NullIf(Trim(D.[SourceFile]),'') As [SourceFile],
			D.[SourceDate]
	From	@Data D
			Left Join [Scope] S
			On	D.[ScopeName] = S.[ScopeName]
			Left Join [App_DataDictionary].[LibrarySource] A
			On	D.[AssemblyName] = A.[AssemblyName]

	-- Apply Changes
	If @LibraryId is Not Null And Not Exists (
		Select	[LibraryId]
		From	@Values V
		Where [LibraryId] = @LibraryId)
		Exec [App_DataDictionary].[procSetLibraryMember] @LibraryId = @LibraryId -- Cascades Delete

	Delete From [App_DataDictionary].[ModelLibrary]
	From	[App_DataDictionary].[ModelLibrary] M
			Left Join @Values S
			On	M.[LibraryId] = S.[LibraryId] And
				M.[ModelId] = @ModelId
	Where	S.[LibraryId] is Null And
			(@LibraryId is Null or M.[LibraryId] = @LibraryId) And
			(@ModelId is Null or M.[ModelId] = @ModelId)
	Print FormatMessage ('Delete [App_DataDictionary].[ModelLibrary]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[LibrarySource]
	From	[App_DataDictionary].[LibrarySource] T
			Left Join @Values S
			On	T.[LibraryId] = S.[LibraryId]
	Where	S.[LibraryId] is Null And
			T.[LibraryId] In (
			Select	A.[LibraryId]
			From	[App_DataDictionary].[LibrarySource] A
					Left Join [App_DataDictionary].[ModelLibrary] C
					On	A.[LibraryId] = C.[LibraryId]
			Where	(@LibraryId is Null Or @LibraryId = A.[LibraryId]) And
					(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[LibrarySource]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[LibraryId],   
				[LibraryTitle],
				[LibraryDescription],
				[AssemblyName],
				[ScopeId],
				[SourceFile],
				[SourceDate]
		From	@Values
		Except
		Select	[LibraryId],   
				[LibraryTitle],
				[LibraryDescription],
				[AssemblyName],
				[ScopeId],
				[SourceFile],
				[SourceDate]
		From	[App_DataDictionary].[LibrarySource])
	Update [App_DataDictionary].[LibrarySource]
	Set		[LibraryTitle] = S.[LibraryTitle],
			[LibraryDescription] = S.[LibraryDescription],
			[AssemblyName] = S.[AssemblyName],
			[ScopeId] = S.[ScopeId],
			[SourceFile] = S.[SourceFile],
			[SourceDate] = S.[SourceDate]
	From	[App_DataDictionary].[LibrarySource] T
			Inner Join [Delta] S
			On	T.[LibraryId] = S.[LibraryId]
	Print FormatMessage ('Update [App_DataDictionary].[LibrarySource]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[LibrarySource] (
			[LibraryId],   
			[LibraryTitle],
			[LibraryDescription],
			[AssemblyName],
			[ScopeId],
			[SourceFile],
			[SourceDate])
	Select	S.[LibraryId],   
			S.[LibraryTitle],
			S.[LibraryDescription],
			S.[AssemblyName],
			S.[ScopeId],
			S.[SourceFile],
			S.[SourceDate]
	From	@Values S
			Left Join [App_DataDictionary].[LibrarySource] T
			On	S.[LibraryId] = T.[LibraryId]
	Where	T.[LibraryId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[LibrarySource]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ModelLibrary] ([ModelId], [LibraryId])
	Select	@ModelId As [ModelId],
			S.[LibraryId]
	from	@Values S
			Left Join [App_DataDictionary].[ModelLibrary] T
			On	S.[LibraryId] = T.[LibraryId] And
				@ModelId = T.[ModelId]
	Where	T.[ModelId] is Null And
			@ModelId is Not Null
	Print FormatMessage ('Insert [App_DataDictionary].[ModelLibrary]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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