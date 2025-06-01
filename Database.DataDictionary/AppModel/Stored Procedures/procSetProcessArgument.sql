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
		[ArgumentAliasId]       UniqueIdentifier Not Null,
		[ArgumentKnownAs]	    [App_DataDictionary].[typeTitle] Not Null,
		[OrdinalPosition]       Int Not Null,
		[IsPassed]				Bit Not Null,
		[IsReturned]			Bit Not Null,
		[IsContributor]			Bit Not Null,
		[IsAltered]				Bit Not Null,
		[AsValue]				Bit Not Null,
		[AsReference]			Bit Not Null,
		Primary Key ([ProcessId], [ArgumentAliasId]),
		Unique ([ProcessId], [ArgumentKnownAs]),
		Unique ([ProcessId], [OrdinalPosition]))

	Declare @Alias [AppModel].[typeAlias];

	Insert Into @Alias ([AliasNameSpace])
	Select	[ArgumentName]
	From	@Data
	Group By [ArgumentName]

	Exec [AppModel].[procSetAlias] @ModelId = @ModelId, @Data = @Alias

	Insert Into @Values
	Select	D.[ProcessId],
			[AppModel].[funcAliasId](D.[ArgumentName]) As [ArgumentAliasId],
			NullIf(Trim(D.[ArgumentKnownAs]),'') As [ArgumentKnownAs],
			D.[OrdinalPosition],
			IsNull(D.[IsPassed],0) As [IsPassed],
			IsNull(D.[IsReturned],0) As [IsReturned],
			IsNull(D.[IsContributor],0) As [IsContributor],
			IsNull(D.[IsAltered],0) As [IsAltered],
			IsNull(D.[AsValue],0) As [AsValue],
			IsNull(D.[AsReference],0) As [AsReference]
	From	@Data D
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
				T.[ArgumentAliasId] = V.[ArgumentAliasId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, T.[ProcessId], 1)
	Where	V.[ArgumentAliasId] is Null And
			(@ProcessId is Not Null Or @ModelId is Not Null) And
			(@ProcessId is Null Or @ProcessId = T.[ProcessId])  And
			(@ModelId is Null Or T.[ProcessId] In (
				Select	[ProcessId]
				From	[AppModel].[ModelProcess]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppModel].[ProcessArgument]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[ProcessId],
				[ArgumentAliasId],
				[ArgumentKnownAs],
				[OrdinalPosition],
				[IsPassed],
				[IsReturned],
				[IsContributor],
				[IsAltered],
				[AsValue],
				[AsReference]
		From	@Values
		Except
		Select	[ProcessId],
				[ArgumentAliasId],
				[ArgumentKnownAs],
				[OrdinalPosition],
				[IsPassed],
				[IsReturned],
				[IsContributor],
				[IsAltered],
				[AsValue],
				[AsReference]
		From	[AppModel].[ProcessArgument])
	Update [AppModel].[ProcessArgument]
	Set		[ArgumentKnownAs] = S.[ArgumentKnownAs],
			[OrdinalPosition] = S.[OrdinalPosition],
			[IsPassed] = S.[IsPassed],
			[IsReturned] = S.[IsReturned],
			[IsContributor] = S.[IsContributor],
			[IsAltered] = S.[IsAltered],
			[AsValue] = S.[AsValue],
			[AsReference] = S.[AsReference]
	From	[AppModel].[ProcessArgument] T
			Inner Join [Delta] S
			On	T.[ProcessId] = S.[ProcessId] And
				T.[ArgumentAliasId] = S.[ArgumentAliasId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, S.[ProcessId], 1)
	Print FormatMessage ('Update [AppModel].[ProcessArgument]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[ProcessArgument] (
			[ProcessId],
			[ArgumentAliasId],
			[ArgumentKnownAs],
			[OrdinalPosition],
			[IsPassed],
			[IsReturned],
			[IsContributor],
			[IsAltered],
			[AsValue],
			[AsReference])
	Select	S.[ProcessId],
			S.[ArgumentAliasId],
			S.[ArgumentKnownAs],
			S.[OrdinalPosition],
			S.[IsPassed],
			S.[IsReturned],
			S.[IsContributor],
			S.[IsAltered],
			S.[AsValue],
			S.[AsReference]
	From	@Values S
			Left Join [AppModel].[ProcessArgument] T
			On	S.[ProcessId] = T.[ProcessId] And
				S.[ArgumentAliasId] = T.[ArgumentAliasId]
			Cross Apply [AppSecurity].[funcModelProcessAuthorization](@ModelId, S.[ProcessId], 1)
	Where	T.[ArgumentAliasId] is Null
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
