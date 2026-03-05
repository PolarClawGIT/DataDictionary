CREATE PROCEDURE [Obsolete].[procSetDataSource]
		@ModelId UniqueIdentifier = Null,
		@DataSourceId UniqueIdentifier = Null,
		@Data [Obsolete].[udttDataSource] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DataSource.
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

	If Exists (
		Select	1
		From	@Data D
				Cross Apply [AppSecurity].[funcScriptingAuthorization]([DataSourceId], 0))
	Throw 601020, 'DataSource Not Authorized', 2;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[DataSourceId]          UniqueIdentifier Not Null,
		[DataSourceTitle]		[AppGeneral].[uddtTitle] Not Null,
		[DataSourceDescription]	[AppGeneral].[uddtDescription] Null,
		Primary Key ([DataSourceId]))

	Declare @Delete Table ([DataSourceId] UniqueIdentifier Not Null)
		
	Insert Into @Values
	Select	X.[DataSourceId],
			NullIf(Trim(D.[DataSourceTitle]),'') As [DataSourceTitle],
			NullIf(Trim(D.[DataSourceDescription]),'') As [DataSourceDescription]
	From	@Data D
			Cross Apply (
				Select	Coalesce(D.[DataSourceId], @DataSourceId, NewId()) As [DataSourceId]) X
	Where	(@DataSourceId is Null And X.[DataSourceId] is Not Null) Or
			(@DataSourceId is Not Null And IsNull(X.[DataSourceId], @DataSourceId) = @DataSourceId)
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into @Delete
	Select	T.[DataSourceId]
	From	[Obsolete].[DataSource] T
			Left Join @Values S
			On	T.[DataSourceId] = S.[DataSourceId]
	Where	S.[DataSourceId] is Null And
			(@DataSourceId is Not Null Or @ModelId is Not Null) And
			(@DataSourceId is Null Or @DataSourceId = T.[DataSourceId])  And
			(@ModelId is Null Or T.[DataSourceId] In (
				Select	[DataSourceId]
				From	[Obsolete].[ScriptingModel]
				Group By [DataSourceId]
				Having Sum(Case When [ModelId] = @ModelId Then 0 Else 1 End) = 0))
	Print FormatMessage ('@Delete: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Insert Into [Obsolete].[ScriptingModel] ([ModelId], [TemplateId])
	Select	T.[ModelId],
			T.[TemplateId]
	From	[Obsolete].[ScriptingModel] T
			Left Join [Obsolete].[ScriptingModel] M
			On	T.[ModelId] = M.[ModelId] And
				T.[TemplateId] = M.[TemplateId] And
				M.[DataSourceId] is Null
	Where	M.[TemplateId] is Null And
			T.[DataSourceId] In (Select [DataSourceId] From @Delete)
	Print FormatMessage ('Insert [AppScript].[ScriptingModel] (DataSource, Keep Template): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [Obsolete].[ScriptingModel]
	From	[Obsolete].[ScriptingModel] T
			Left Join @Values S
			On	T.[DataSourceId] = S.[DataSourceId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[DataSourceId], 1)
	Where	S.[DataSourceId] is Null And
			T.[DataSourceId] In (Select [DataSourceId] From @Delete)
	Print FormatMessage ('Delete [AppScript].[ScriptingModel] (DataSource): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [Obsolete].[DataObjectName]
	From	[Obsolete].[DataObjectName] T
			Left Join @Values S
			On	T.[DataSourceId] = S.[DataSourceId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[DataSourceId], 1)
	Where	S.[DataSourceId] is Null And
			T.[DataSourceId] In (Select [DataSourceId] From @Delete)
	Print FormatMessage ('Delete [AppScript].[DataObjectName] (DataSource): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [Obsolete].[DataObject]
	From	[Obsolete].[DataObject] T
			Left Join @Values S
			On	T.[DataSourceId] = S.[DataSourceId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[DataSourceId], 1)
	Where	S.[DataSourceId] is Null And
			T.[DataSourceId] In (Select [DataSourceId] From @Delete)
	Print FormatMessage ('Delete [AppScript].[DataObject] (DataSource): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [Obsolete].[DataSource]
	From	[Obsolete].[DataSource] T
			Left Join @Values S
			On	T.[DataSourceId] = S.[DataSourceId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[DataSourceId], 1)
	Where	S.[DataSourceId] is Null And
			T.[DataSourceId] In (Select [DataSourceId] From @Delete)
	Print FormatMessage ('Delete [AppScript].[DataSource] (DataSource): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[DataSourceId],
				[DataSourceTitle],
				[DataSourceDescription]
		From	@Values
		Except
		Select	[DataSourceId],
				[DataSourceTitle],
				[DataSourceDescription]
		From	[Obsolete].[DataSource])
	Update	[Obsolete].[DataSource]
	Set		[DataSourceTitle] = S.[DataSourceTitle],
			[DataSourceDescription] = S.[DataSourceDescription]
	From	[Obsolete].[DataSource] T
			Inner Join [Delta] S
			On	T.[DataSourceId] = S.[DataSourceId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[DataSourceId], 1)
	Print FormatMessage ('Update [AppScript].[DataSource]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [Obsolete].[DataSource] (
			[DataSourceId],
			[DataSourceTitle],
			[DataSourceDescription])
	Select	S.[DataSourceId],
			S.[DataSourceTitle],
			S.[DataSourceDescription]
	From	@Values S
			Left Join [Obsolete].[DataSource] T
			On	S.[DataSourceId] = T.[DataSourceId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[DataSourceId], 1)
	Where	T.[DataSourceId] is Null
	Print FormatMessage ('Insert [AppScript].[DataSource]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [Obsolete].[ScriptingModel] (
			[ModelId],
			[DataSourceId])
	Select	@ModelId As [ModelId],
			S.[DataSourceId]
	From	@Values S
			Left Join [Obsolete].[ScriptingModel] T
			On	S.[DataSourceId] = T.[DataSourceId] And
				@ModelId = T.[ModelId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[DataSourceId], 1)
	Where	T.[DataSourceId] Is Null And
			@ModelId is Not Null
	Print FormatMessage ('Insert [AppScript].[ScriptingModel]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));


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
