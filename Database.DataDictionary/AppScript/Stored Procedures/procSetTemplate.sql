CREATE PROCEDURE [AppScript].[procSetTemplate]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@Data [AppScript].[udttTemplate] ReadOnly
AS
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
	If Exists (
		Select	1
		From	@Data D
				Cross Apply [AppSecurity].[funcScriptingAuthorization]([TemplateId], 0))
	Throw 601020, 'Template Not Authorized', 2;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[TemplateId]            UniqueIdentifier Not Null,
		[TemplateTitle]			[AppGeneral].[uddtTitle] Not Null,
		[TemplateDescription]	[AppGeneral].[uddtDescription] Null,
		Primary Key([TemplateId]))

	Insert Into @Values
	Select	X.[TemplateId],
			NullIf(Trim(D.[TemplateTitle]),'') As [TemplateTitle],
			NullIf(Trim(D.[TemplateDescription]),'') As [TemplateDescription]
	From	@Data D
			Cross Apply (
				Select	Coalesce(D.[TemplateId], NewId()) As [TemplateId]) X
	Where	(@TemplateId is Null Or @TemplateId = X.[TemplateId])
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Declare @Delete Table ([TemplateId] UniqueIdentifier Not Null)

	Insert Into @Delete
	Select	T.[TemplateId]
	From	[AppScript].[Template] T
			Left Join @Values S
			On	T.[TemplateId] = S.[TemplateId]
	Where	S.[TemplateId] is Null And
			T.[TemplateId] Not In (
				Select	[TemplateId]
				From	[AppScript].[TemplateModel])
	Print FormatMessage ('@Delete: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Deal with Ownership, Sets up Row Level Security
	Insert Into [AppSecurity].[SecurableOwner] (
			[PrincipalId],
			[SecurableId])
	Select	S.[PrincipalId],
			V.[TemplateId]
	From	@Values V
			Cross Apply [AppSecurity].[funcScriptingAuthorization](V.[TemplateId], 1) S
	Where	S.[IsScriptOwner] = 1 And
			S.[IsScriptAdmin] = 0 And
			S.[HasOwner] = 0 And
			S.[PrincipalId] is not null
	Print FormatMessage ('Insert [AppSecurity].[SecurityOwner]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	-- Apply Changes
	Delete From [AppScript].[TemplateModel]
	From	[AppScript].[TemplateModel] T
			Left Join @Values S
			On	T.[TemplateId] = S.[TemplateId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[TemplateId], 1)
	Where	S.[TemplateId] is Null And
			(@TemplateId is Not Null Or @ModelId is Not Null) And
			(@TemplateId is Null Or @TemplateId = T.[TemplateId])  And
			(@ModelId is Null Or @ModelId = T.[ModelId])
	Print FormatMessage ('Delete [AppScript].[TemplateModel] (Template): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppSecurity].[SecurableOwner]
	From	[AppSecurity].[SecurableOwner] T
			Left Join @Values S
			On	T.[SecurableId] = S.[TemplateId]
	Where	S.[TemplateId] is Null And
			T.[SecurableId] In (Select [TemplateId] From @Delete)
	Print FormatMessage ('Delete [AppSecurity].[SecurableOwner] (Template): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppScript].[Template]
	From	[AppScript].[Template] T
			Left Join @Values S
			On	T.[TemplateId] = S.[TemplateId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[TemplateId], 1)
	Where	S.[TemplateId] is Null And
			T.[TemplateId] In (Select [TemplateId] From @Delete)
	Print FormatMessage ('Delete [AppScript].[Template]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));


	-- TODO: Add child table delete


	;With [Delta] As (
		Select	[TemplateId],
				[TemplateTitle],
				[TemplateDescription]
		From	@Values
		Except
		Select	[TemplateId],
				[TemplateTitle],
				[TemplateDescription]
		From	[AppScript].[Template])
	Update [AppScript].[Template]
	Set		[TemplateTitle] = S.[TemplateTitle],
			[TemplateDescription] = S.[TemplateDescription]
	From	[AppScript].[Template] T
			Inner Join [Delta] S
			On	T.[TemplateId] = S.[TemplateId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[TemplateId], 1)
	Print FormatMessage ('Update [AppScript].[Template]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[Template] (
			[TemplateId],
			[TemplateTitle],
			[TemplateDescription])
	Select	S.[TemplateId],
			S.[TemplateTitle],
			S.[TemplateDescription]
	From	@Values S
			Left Join [AppScript].[Template] T
			On	S.[TemplateId] = T.[TemplateId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[TemplateId], 1)
	Where	T.[TemplateId] is Null
	Print FormatMessage ('Insert [AppScript].[Template]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[TemplateModel] (
			[ModelId],
			[TemplateId])
	Select	@ModelId As [ModelId],
			S.[TemplateId]
	From	@Values S
			Left Join [AppScript].[TemplateModel] T
			On	S.[TemplateId] = T.[TemplateId] And
				@ModelId = T.[ModelId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[TemplateId], 1)
	Where	T.[TemplateId] Is Null And
			@ModelId is Not Null
	Print FormatMessage ('Insert [AppScript].[TemplateModel]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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

