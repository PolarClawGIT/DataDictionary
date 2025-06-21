CREATE PROCEDURE [AppModel].[procSetProcess]
		@ModelId UniqueIdentifier = Null,
		@ProcessId UniqueIdentifier = Null,
		@Data [AppModel].[typeProcess] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on Model Process.
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
	If Exists (
		Select	1
		From	@Data D
				Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, [ProcessId], 0))
	Throw 601020, 'Model Not Authorized', 2;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[ProcessId]			    UniqueIdentifier Not Null,
		[ProcessTitle]		    [AppGeneral].[dtTitle] Not Null,
		[ProcessDescription]	[AppGeneral].[dtDescription] Null,
		[ProcessName]			[AppModel].[typeQualifiedName] Null,
		Primary Key ([ProcessId]),
		Unique ([ProcessTitle]))

	Insert Into @Values
	Select	Coalesce(D.[ProcessId], H.[ProcessId], NewId()) As [ProcessId],
			NullIf(Trim(D.[ProcessTitle]),'') As [ProcessTitle],
			NullIf(Trim(D.[ProcessDescription]),'') As [ProcessDescription],
			N.[ProcessName]
	From	@Data D
			Left Join [AppModel].[ModelProcessHs] H
			On	(D.[ProcessId] = H.[ProcessId] Or
				 (H.[ModelId] = @ModelId And
				  D.[ProcessTitle] = H.[ProcessTitle]))
			Cross Apply (
				Select	Coalesce(D.[ProcessId], H.[ProcessId], NewId()) As [ProcessId]) X
			Outer Apply (
				Select	[QualifiedName] As [ProcessName]
				From	[AppGeneral].[funcParseName](D.[ProcessName])
				Where	[IsBase] = 1) N
	Where	(@ModelId is Null Or @ModelId = IsNull(H.[ModelId], @ModelId)) And
			(@ProcessId is Null Or @ProcessId = X.[ProcessId])
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Declare @Delete Table ([ProcessId] UniqueIdentifier Not Null)

	Insert Into @Delete
	Select	T.[ProcessId]
	From	[AppModel].[Process] T
			Left Join @Values S
			On	T.[ProcessId] = S.[ProcessId]
	Where	S.[ProcessId] is Null And
			(@ProcessId is Not Null Or @ModelId is Not Null) And
			(@ProcessId is Null Or @ProcessId = T.[ProcessId])  And
			(@ModelId is Null Or T.[ProcessId] In (
				Select	[ProcessId]
				From	[AppModel].[ModelProcess]
				Group By [ProcessId]
				Having Sum(Case When [ModelId] = @ModelId Then 0 Else 1 End) = 0))

	Delete From [AppModel].[ModelProcess]
	From	[AppModel].[ModelProcess] T
			Left Join @Values S
			On	T.[ProcessId] = S.[ProcessId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](T.[ModelId], T.[ProcessId], 1)
	Where	S.[ProcessId] is Null And
			(@ProcessId is Not Null Or @ModelId is Not Null) And
			(@ProcessId is Null Or @ProcessId = T.[ProcessId])  And
			(@ModelId is Null Or @ModelId = T.[ModelId])
	Print FormatMessage ('Delete [AppModel].[ModelProcess] (Process): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[ProcessAlias]
	From	[AppModel].[ProcessAlias] T
			Left Join @Values S
			On	T.[ProcessId] = S.[ProcessId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, T.[ProcessId], 1)
	Where	S.[ProcessId] is Null And
			T.[ProcessId] In (Select [ProcessId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[ProcessAlias] (Process): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[ProcessDefinition]
	From	[AppModel].[ProcessDefinition] T
			Left Join @Values S
			On	T.[ProcessId] = S.[ProcessId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, T.[ProcessId], 1)
	Where	S.[ProcessId] is Null And
			T.[ProcessId] In (Select [ProcessId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[ProcessDefinition] (Process): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[ProcessProperty]
	From	[AppModel].[ProcessProperty] T
			Left Join @Values S
			On	T.[ProcessId] = S.[ProcessId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, T.[ProcessId], 1)
	Where	S.[ProcessId] is Null And
			T.[ProcessId] In (Select [ProcessId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[ProcessProperty] (Process): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[ProcessSubjectArea]
	From	[AppModel].[ProcessSubjectArea] T
			Left Join @Values S
			On	T.[ProcessId] = S.[ProcessId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, T.[ProcessId], 1)
	Where	S.[ProcessId] is Null And
			T.[ProcessId] In (Select [ProcessId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[ProcessSubjectArea] (Process): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[ProcessArgument]
	From	[AppModel].[ProcessArgument] T
			Left Join @Values S
			On	T.[ProcessId] = S.[ProcessId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, T.[ProcessId], 1)
	Where	S.[ProcessId] is Null And
			T.[ProcessId] In (Select [ProcessId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[ProcessArgument] (Process): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[Process]
	From	[AppModel].[Process] T
			Left Join @Values S
			On	T.[ProcessId] = S.[ProcessId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, T.[ProcessId], 1)
	Where	S.[ProcessId] is Null And
			T.[ProcessId] In (Select [ProcessId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[Process]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[ProcessId],
				[ProcessTitle],
				[ProcessDescription],
				[ProcessName]
		From	@Values
		Except
		Select	[ProcessId],
				[ProcessTitle],
				[ProcessDescription],
				[ProcessName]
		From	[AppModel].[Process])
	Update [AppModel].[Process]
	Set		[ProcessTitle] = S.[ProcessTitle],
			[ProcessDescription] = S.[ProcessDescription],
			[ProcessName] = S.[ProcessName]
	From	[AppModel].[Process] T
			Inner Join [Delta] S
			On	T.[ProcessId] = S.[ProcessId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, S.[ProcessId], 1)
	Print FormatMessage ('Update [AppModel].[Process]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[Process] (
			[ProcessId],
			[ProcessTitle],
			[ProcessDescription],
			[ProcessName])
	Select	S.[ProcessId],
			S.[ProcessTitle],
			S.[ProcessDescription],
			S.[ProcessName]
	From	@Values S
			Left Join [AppModel].[Process] T
			On	S.[ProcessId] = T.[ProcessId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, S.[ProcessId], 1)
	Where	T.[ProcessId] is Null
	Print FormatMessage ('Insert [AppModel].[Process]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[ModelProcess] (
			[ModelId],
			[ProcessId])
	Select	@ModelId As [ModelId],
			S.[ProcessId]
	From	@Values S
			Left Join [AppModel].[ModelProcess] T
			On	S.[ProcessId] = T.[ProcessId] And
				@ModelId = T.[ModelId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, S.[ProcessId], 1)
	Where	T.[ProcessId] Is Null And
			@ModelId is Not Null
	Print FormatMessage ('Insert [AppModel].[ModelProcess]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
	-- Rollback Transaction
	If @TRN_IsNewTran = 1
	  Begin -- If this is the outer transaction, roll it back
		Rollback Transaction
		Print FormatMessage ('Rollback Transaction Issued ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
	  End -- Rollback Transaction
	-- This is a nested transaction, must be rolled back by outer transaction
	Else Print FormatMessage ('Rollback Transaction Pending ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))

	If ERROR_NUMBER() >= 50000 Exec [AppGeneral].[procThrowHelpSubject]
	Else If ERROR_SEVERITY() Not In (0, 11) Throw;
End Catch
GO
