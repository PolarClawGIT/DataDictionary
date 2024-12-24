CREATE PROCEDURE [AppModel].[procSetAttributeAlias]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@Data [AppModel].[typeAttributeAlias] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on Model AttributeAlias.
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
		[AttributeId]		UniqueIdentifier Not Null,
		[AliasScope]		[App_DataDictionary].[typeScopeName] NOT NULL,
		[AliasNameSpace]	[App_DataDictionary].[typeNameSpacePath] Null,
		--Unique ([AliasId], [AliasNameSpace]) -- Cannot Index, [AliasNameSpace] is too long
		Primary Key([AliasId]))

	Insert Into @Values
	Select	Coalesce(H.[AliasId], NewId()) As [AliasId],
			D.[AttributeId],
			D.[AliasScope],
			N.[AliasNameSpace]
	From	@Data D
			Cross Apply (
				Select	[QualifiedName] As [AliasNameSpace]
				From	[AppModel].[funcParseName](D.[AliasNameSpace])
				Where	[IsBase] = 1) N
			Left Join [AppModel].[AttributeAliasHs] H
			On	D.[AttributeId] = H.[AttributeId] And
				N.[AliasNameSpace] = H.[AliasNameSpace]
	Where	(@AttributeId is Null Or @AttributeId = D.[AttributeId]) And
			(@ModelId is Null Or D.[AttributeId] In (
				Select	[AttributeId]
				From	[AppModel].[ModelAttribute]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppModel].[AttributeAlias]
	From	[AppModel].[AttributeAlias] T
			Left Join @Values V
			On	T.[AliasId] = V.[AliasId]
	Where	V.[AttributeId] is Null And
			(@AttributeId is Not Null Or @ModelId is Not Null) And
			(@AttributeId is Null Or @AttributeId = T.[AttributeId])  And
			(@ModelId is Null Or T.[AttributeId] In (
				Select	[AttributeId]
				From	[AppModel].[ModelAttribute]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppModel].[AttributeAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[AliasId],
				[AttributeId],
				[AliasScope],
				[AliasNameSpace]
		From	@Values S
		Except
		Select	[AliasId],
				[AttributeId],
				[AliasScope],
				[AliasNameSpace]
		From	[AppModel].[AttributeAlias])
	Update	[AppModel].[AttributeAlias]
	Set		[AliasScope] = S.[AliasScope],
			[AliasNameSpace] = S.[AliasNameSpace]
	From	[Delta] S
			Inner Join [AppModel].[AttributeAlias] T
			On	S.[AliasId] = T.[AliasId]
	Print FormatMessage ('Update [AppModel].[AttributeAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[AttributeAlias] (
			[AliasId],
			[AttributeId],
			[AliasScope],
			[AliasNameSpace])
	Select	S.[AliasId],
			S.[AttributeId],
			S.[AliasScope],
			S.[AliasNameSpace]
	From	@Values S
			Left Join [AppModel].[AttributeAlias] T
			On	S.[AliasId] = T.[AliasId]
	Where	T.[AttributeId] is Null
	Print FormatMessage ('Insert [AppModel].[AttributeAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
