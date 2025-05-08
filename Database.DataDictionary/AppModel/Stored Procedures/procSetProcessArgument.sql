CREATE PROCEDURE [AppModel].[procSetProcessArgument]
		@ModelId UniqueIdentifier = Null,
		@ProcessId UniqueIdentifier = Null,
		@Data [AppModel].[typeProcessArgument] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on Model ProcessArgument.
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
		[ProcessId]				UniqueIdentifier Not Null,
		[ArgumentId]			UniqueIdentifier Not Null,
		[ArgumentTitle]			[App_DataDictionary].[typeTitle]       Not Null,
		[ArgumentDescription]	[App_DataDictionary].[typeDescription] Null,
		[ArgumentName]			[AppModel].[typeQualifiedName]         Null,
		[ArgumentType]			[AppModel].[typeQualifiedName]         Null, -- An Entity, Attribute, or system
		[OrdinalPosition]       Int Not Null,
		[IsInput]               Bit Null, -- Input Can be bidirectional or not defined (contributes)
		[IsOutput]              Bit Null, -- Output Can be bidirectional or not defined (contributes)
		Primary Key ([ProcessId], [ArgumentId]),
		Unique ([ProcessId], [ArgumentTitle]),
		Unique ([ProcessId], [OrdinalPosition]))

	Insert Into @Values
	Select	D.[ProcessId],
			Coalesce(H.[ArgumentId], NewId()) As [ArgumentId],
			NullIf(Trim(D.[ArgumentTitle]),'') As [ProcessTitle],
			NullIf(Trim(D.[ArgumentDescription]),'') As [ProcessDescription],
			N.[ArgumentName],
			T.[ArgumentType],
			D.[OrdinalPosition],
			IsNull(D.[IsInput],0) As [IsInput],
			IsNull(D.[IsOutput],0) As [IsOutput]
	From	@Data D
			Left Join [AppModel].[ProcessArgumentHs] H
			On	D.[ProcessId] = H.[ProcessId] And
				D.[OrdinalPosition] = H.[OrdinalPosition]
			Outer Apply (
				Select	[QualifiedName] As [ArgumentName]
				From	[AppModel].[funcParseName](D.[ArgumentName])
				Where	[IsBase] = 1) N
			Outer Apply (
				Select	[QualifiedName] As [ArgumentType]
				From	[AppModel].[funcParseName](D.[ArgumentType])
				Where	[IsBase] = 1) T
	Where	(@ProcessId is Null Or @ProcessId = D.[ProcessId]) And
			(@ModelId is Null Or D.[ProcessId] In (
				Select	[ProcessId]
				From	[AppModel].[ModelProcess]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppModel].[ProcessArgument]
	From	[AppModel].[ProcessArgument] T
			Left Join @Values V
			On	T.[ProcessId] = V.[ProcessId] And
				T.[ArgumentId] = V.[ArgumentId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, T.[ProcessId], 1)
	Where	V.[ArgumentId] is Null And
			(@ProcessId is Not Null Or @ModelId is Not Null) And
			(@ProcessId is Null Or @ProcessId = T.[ProcessId])  And
			(@ModelId is Null Or T.[ProcessId] In (
				Select	[ProcessId]
				From	[AppModel].[ModelProcess]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppModel].[ProcessArgument]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[ProcessId],
				[ArgumentId],
				[ArgumentTitle],
				[ArgumentDescription],
				[ArgumentName],
				[ArgumentType],
				[OrdinalPosition],
				[IsInput],
				[IsOutput]
		From	@Values
		Except
		Select	[ProcessId],
				[ArgumentId],
				[ArgumentTitle],
				[ArgumentDescription],
				[ArgumentName],
				[ArgumentType],
				[OrdinalPosition],
				[IsInput],
				[IsOutput]
		From	[AppModel].[ProcessArgument])
	Update [AppModel].[ProcessArgument]
	Set		[ArgumentTitle] = S.[ArgumentTitle],
			[ArgumentDescription] = S.[ArgumentDescription],
			[ArgumentName] = S.[ArgumentName],
			[ArgumentType] = S.[ArgumentType],
			[OrdinalPosition] = S.[OrdinalPosition],
			[IsInput] = S.[IsInput],
			[IsOutput] = S.[IsOutput]
	From	[AppModel].[ProcessArgument] T
			Inner Join [Delta] S
			On	T.[ProcessId] = S.[ProcessId] And
				T.[ArgumentId] = S.[ArgumentId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, S.[ProcessId], 1)
	Print FormatMessage ('Update [AppModel].[ProcessArgument]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[ProcessArgument] (
			[ProcessId],
			[ArgumentId],
			[ArgumentTitle],
			[ArgumentDescription],
			[ArgumentName],
			[ArgumentType],
			[OrdinalPosition],
			[IsInput],
			[IsOutput])
	Select	S.[ProcessId],
			S.[ArgumentId],
			S.[ArgumentTitle],
			S.[ArgumentDescription],
			S.[ArgumentName],
			S.[ArgumentType],
			S.[OrdinalPosition],
			S.[IsInput],
			S.[IsOutput]
	From	@Values S
			Left Join [AppModel].[ProcessArgument] T
			On	S.[ProcessId] = T.[ProcessId] And
				S.[ArgumentId] = T.[ArgumentId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, S.[ProcessId], 1)
	Where	T.[ArgumentId] is Null
	Print FormatMessage ('Insert [AppModel].[ProcessArgument]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
