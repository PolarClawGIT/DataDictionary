CREATE PROCEDURE [AppGeneral].[procRecordTransactionLog]
		@ProcId Int = null -- Pass @@ProcId
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Record on TransactionLog.
*/

-- Transaction Handling
Declare	@TRN_IsNewTran Bit = 0, -- Indicates that the stored procedure started the transaction. Used to handle nested Transactions
		@RowCount Int = 0 -- @@RowCount is reset just by reading @@RowCount. This is used to persist the value.

Begin Try
	-- Begin Transaction
	If @@TranCount = 0
	  Begin -- Not in a nested/distributed transaction, need to start a transaction
		Begin Transaction
		Select	@TRN_IsNewTran = 1
	  End; -- Begin Transaction

	Insert Into [AppGeneral].[TransactionLog] ([login_time], [session_id], [transaction_id], [CallerSchema], [CallerObject])
	Select	S.[login_time],
			S.[session_id],
			Current_Transaction_Id() As [transaction_id],
			Object_Schema_Name(@ProcId) As [CallerSchema],
			Object_Name(@ProcId) As [CallerObject]
	From	[sys].[dm_exec_sessions] S
			Left Join [AppGeneral].[TransactionLog] L
			On	S.[login_time] = L.[login_time] And
				S.[session_id] = L.[session_id] And
				Current_Transaction_Id() = L.[transaction_id]
	Where	L.[TransactionId] is Null And
			S.[session_id] = @@SPID
	Group By S.[session_id],
			S.[login_time]
	Set @RowCount = @@RowCount
	If @RowCount > 0 Print FormatMessage ('Insert [AppGeneral].[TransactionLog]: %i, %s', @RowCount, Convert(VarChar,GetDate()));

	-- Commit Transaction
	If @TRN_IsNewTran = 1
	  Begin -- If this is the outer transaction, commit it
		If XAct_State() = -1 Throw 103930, 'The current transaction cannot be committed and cannot support operations that write to the log file. Roll back the transaction. (Msg- 3930)', 100
		Commit Transaction
		Print FormatMessage ('Commit Transaction Issued ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
	  End -- Commit Transaction
	  -- This is a nested transaction, must be committed by outer transaction
	--Else Print FormatMessage ('Commit Transaction Pending ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
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
