CREATE PROCEDURE [AppModel].[procSetProcessDataFlow]
		@ModelId UniqueIdentifier = Null,
		@ProcessId UniqueIdentifier = Null,
		@Data [AppModel].[typeProcessDataFlow] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on Model ProcessDataFlow.
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
		[ProcessId]			   UniqueIdentifier Not Null,
		[DataFlowAliasId]      UniqueIdentifier Not Null,
		[DataFlowKnownAs]	   [App_DataDictionary].[typeTitle] Not Null, -- What to call the DataFlow within this Process (default is the DataFlow Name)
		[IsInFlow]             Bit Null,
		[IsOutFlow]		       Bit Null,
		Primary Key ([ProcessId], [DataFlowAliasId]),
		Unique ([ProcessId], [DataFlowKnownAs]))

	Declare @Alias [AppModel].[typeAlias];

	Insert Into @Alias ([AliasNameSpace])
	Select	[DataFlowPath]
	From	@Data

	Exec [AppModel].[procSetAlias] @ModelId = @ModelId, @Data = @Alias

	Insert Into @Values
	Select	D.[ProcessId],
			[AppModel].[funcAliasId](D.[DataFlowPath]) As [DataFlowAliasId],
			NullIf(Trim(D.[DataFlowKnownAs]),'') As [DataFlowKnownAs],
			IsNull(D.[IsInFlow],0) As [IsInFlow],
			IsNull(D.[IsOutFlow],0) As [IsOutFlow]
	From	@Data D
			Left Join [AppModel].[ProcessDataFlowHs] H
			On	D.[ProcessId] = H.[ProcessId] And
				D.[DataFlowPath] = H.[DataFlowPath]
	Where	(@ProcessId is Null Or @ProcessId = D.[ProcessId]) And
			(@ModelId is Null Or D.[ProcessId] In (
				Select	[ProcessId]
				From	[AppModel].[ModelProcess]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppModel].[ProcessDataFlow]
	From	[AppModel].[ProcessDataFlow] T
			Left Join @Values V
			On	T.[ProcessId] = V.[ProcessId] And
				T.[DataFlowAliasId] = V.[DataFlowAliasId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, T.[ProcessId], 1)
	Where	V.[DataFlowAliasId] is Null And
			(@ProcessId is Not Null Or @ModelId is Not Null) And
			(@ProcessId is Null Or @ProcessId = T.[ProcessId])  And
			(@ModelId is Null Or T.[ProcessId] In (
				Select	[ProcessId]
				From	[AppModel].[ModelProcess]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppModel].[ProcessDataFlow]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[ProcessId],
				[DataFlowAliasId],
				[DataFlowKnownAs],
				[IsInFlow],
				[IsOutFlow]
		From	@Values
		Except
		Select	[ProcessId],
				[DataFlowAliasId],
				[DataFlowKnownAs],
				[IsInFlow],
				[IsOutFlow]
		From	[AppModel].[ProcessDataFlow])
	Update [AppModel].[ProcessDataFlow]
	Set		[DataFlowKnownAs] = S.[DataFlowKnownAs],
			[IsInFlow] = S.[IsInFlow],
			[IsOutFlow] = S.[IsOutFlow]
	From	[AppModel].[ProcessDataFlow] T
			Inner Join [Delta] S
			On	T.[ProcessId] = S.[ProcessId] And
				T.[DataFlowAliasId] = S.[DataFlowAliasId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, S.[ProcessId], 1)
	Print FormatMessage ('Update [AppModel].[ProcessDataFlow]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[ProcessDataFlow] (
			[ProcessId],
			[DataFlowAliasId],
			[DataFlowKnownAs],
			[IsInFlow],
			[IsOutFlow])
	Select	S.[ProcessId],
			S.[DataFlowAliasId],
			S.[DataFlowKnownAs],
			S.[IsInFlow],
			S.[IsOutFlow]
	From	@Values S
			Left Join [AppModel].[ProcessDataFlow] T
			On	S.[ProcessId] = T.[ProcessId] And
				S.[DataFlowAliasId] = T.[DataFlowAliasId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, S.[ProcessId], 1)
	Where	T.[DataFlowAliasId] is Null
	Print FormatMessage ('Insert [AppModel].[Process]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
