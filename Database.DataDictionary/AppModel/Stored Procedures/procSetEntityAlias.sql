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

	-- Clean the Data, helps performance
	Declare @Values Table (
		[AliasId]			UniqueIdentifier Not Null,
		[EntityId]		    UniqueIdentifier Not Null,
		[AliasScope]		[App_DataDictionary].[typeScopeName] NOT NULL,
		[AliasNameSpace]	[App_DataDictionary].[typeNameSpacePath] Null,
		--Unique ([AliasId], [AliasNameSpace]) -- Cannot Index, [AliasNameSpace] is too long
		Primary Key([AliasId]))

	Insert Into @Values
	Select	Coalesce(H.[AliasId], NewId()) As [AliasId],
			D.[EntityId],
			D.[AliasScope],
			N.[AliasNameSpace]
	From	@Data D
			Cross Apply (
				Select	[QualifiedName] As [AliasNameSpace]
				From	[AppModel].[funcParseName](D.[AliasNameSpace])
				Where	[IsBase] = 1) N
			Left Join [AppModel].[EntityAliasHs] H
			On	D.[EntityId] = H.[EntityId] And
				N.[AliasNameSpace] = H.[AliasNameSpace]
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
			On	T.[AliasId] = V.[AliasId]
	Where	V.[EntityId] is Null And
			(@EntityId is Not Null Or @ModelId is Not Null) And
			(@EntityId is Null Or @EntityId = T.[EntityId])  And
			(@ModelId is Null Or T.[EntityId] In (
				Select	[EntityId]
				From	[AppModel].[ModelEntity]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppModel].[EntityAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[AliasId],
				[EntityId],
				[AliasScope],
				[AliasNameSpace]
		From	@Values S
		Except
		Select	[AliasId],
				[EntityId],
				[AliasScope],
				[AliasNameSpace]
		From	[AppModel].[EntityAlias])
	Update	[AppModel].[EntityAlias]
	Set		[AliasScope] = S.[AliasScope],
			[AliasNameSpace] = S.[AliasNameSpace]
	From	[Delta] S
			Inner Join [AppModel].[EntityAlias] T
			On	S.[AliasId] = T.[AliasId]
	Print FormatMessage ('Update [AppModel].[EntityAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[EntityAlias] (
			[AliasId],
			[EntityId],
			[AliasScope],
			[AliasNameSpace])
	Select	S.[AliasId],
			S.[EntityId],
			S.[AliasScope],
			S.[AliasNameSpace]
	From	@Values S
			Left Join [AppModel].[EntityAlias] T
			On	S.[AliasId] = T.[AliasId]
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