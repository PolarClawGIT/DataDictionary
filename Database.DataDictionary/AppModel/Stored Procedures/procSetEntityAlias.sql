CREATE PROCEDURE [AppModel].[procSetEntityAlias]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null,
		@Data [AppModel].[typeEntityAlias] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on Model EntityAlias.
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
				Cross Apply [AppSecurity].[funcModelEntityAuthorization](@ModelId, [EntityId], 0))
	Throw 601020, 'Model Not Authorized', 2;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[EntityId]		    UniqueIdentifier Not Null,
		[AliasId]			UniqueIdentifier Not Null,
		[AliasScope]		[AppModel].[typeScopeName] NOT NULL,
		--Unique ([AliasId], [AliasNameSpace]) -- Cannot Index, [AliasNameSpace] is too long
		Primary Key([EntityId], [AliasId]))

	Declare @Alias [AppModel].[typeAlias];

	Insert Into @Alias ([AliasNameSpace])
	Select	[AliasNameSpace]
	From	@Data

	Exec [AppModel].[procSetAlias] @ModelId = @ModelId, @Data = @Alias

	Insert Into @Values
	Select	D.[EntityId],
			[AppModel].[funcAliasId] (D.[AliasNameSpace]) As [AliasId],
			D.[AliasScope]
	From	@Data D
	Where	(@EntityId is Null Or @EntityId = D.[EntityId]) And
			(@ModelId is Null Or D.[EntityId] In (
				Select	[EntityId]
				From	[AppModel].[ModelEntity]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppModel].[EntityAlias]
	From	[AppModel].[EntityAlias] T
			Left Join @Values V
			On	T.[EntityId] = V.[EntityId] And
				T.[AliasId] = V.[AliasId]
			Cross Apply [AppSecurity].[funcModelEntityAuthorization](@ModelId, T.[EntityId], 1)
	Where	V.[EntityId] is Null And
			(@EntityId is Not Null Or @ModelId is Not Null) And
			(@EntityId is Null Or @EntityId = T.[EntityId])  And
			(@ModelId is Null Or T.[EntityId] In (
				Select	[EntityId]
				From	[AppModel].[ModelEntity]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppModel].[EntityAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[EntityId],
				[AliasId],				
				[AliasScope]
		From	@Values S
		Except
		Select	[EntityId],
				[AliasId],				
				[AliasScope]
		From	[AppModel].[EntityAlias])
	Update	[AppModel].[EntityAlias]
	Set		[AliasScope] = S.[AliasScope]
	From	[Delta] S
			Inner Join [AppModel].[EntityAlias] T
			On	S.[EntityId] = T.[EntityId] And
				S.[AliasId] = T.[AliasId]
			Cross Apply [AppSecurity].[funcModelEntityAuthorization](@ModelId, S.[EntityId], 1)
	Print FormatMessage ('Update [AppModel].[EntityAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[EntityAlias] (
			[EntityId],
			[AliasId],
			[AliasScope])
	Select	S.[EntityId],
			S.[AliasId],
			S.[AliasScope]
	From	@Values S
			Left Join [AppModel].[EntityAlias] T
			On	S.[EntityId] = T.[EntityId] And
				S.[AliasId] = T.[AliasId]
			Cross Apply [AppSecurity].[funcModelEntityAuthorization](@ModelId, S.[EntityId], 1)
	Where	T.[EntityId] is Null
	Print FormatMessage ('Insert [AppModel].[EntityAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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