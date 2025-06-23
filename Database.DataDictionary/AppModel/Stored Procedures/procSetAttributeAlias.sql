CREATE PROCEDURE [AppModel].[procSetAttributeAlias]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@Data [AppModel].[udttAttributeAlias] ReadOnly
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
	If Exists (
		Select	1
		From	@Data D
				Cross Apply [AppSecurity].[funcModelAttributeAuthorization](@ModelId, [AttributeId], 0))
	Throw 601020, 'Model Not Authorized', 2;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[AttributeId]		UniqueIdentifier Not Null,
		[AliasId]			UniqueIdentifier Not Null,
		[AliasScope]		[AppGeneral].[uddtScopeName] NOT NULL,
		--Unique ([AliasId], [AliasPath]) -- Cannot Index, [AliasPath] is too long
		Primary Key([AttributeId], [AliasId]))

	Declare @Alias [AppModel].[udttAlias];

	Insert Into @Alias ([AliasNameSpace])
	Select	[AliasPath]
	From	@Data

	Exec [AppModel].[procSetAlias] @ModelId = @ModelId, @Data = @Alias

	Insert Into @Values
	Select	D.[AttributeId],
			[AppModel].[funcAliasId] (D.[AliasPath]) As [AliasId],
			D.[AliasScope]
	From	@Data D
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
			On	T.[AttributeId] = V.[AttributeId] And
				T.[AliasId] = V.[AliasId] 
			Cross Apply [AppSecurity].[funcModelAttributeAuthorization](@ModelId, T.[AttributeId], 1)
	Where	V.[AttributeId] is Null And
			(@AttributeId is Not Null Or @ModelId is Not Null) And
			(@AttributeId is Null Or @AttributeId = T.[AttributeId])  And
			(@ModelId is Null Or T.[AttributeId] In (
				Select	[AttributeId]
				From	[AppModel].[ModelAttribute]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppModel].[AttributeAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[AttributeId],
				[AliasId],
				[AliasScope]
		From	@Values S
		Except
		Select	[AttributeId],
				[AliasId],
				[AliasScope]
		From	[AppModel].[AttributeAlias])
	Update	[AppModel].[AttributeAlias]
	Set		[AliasScope] = S.[AliasScope]
	From	[Delta] S
			Inner Join [AppModel].[AttributeAlias] T
			On	S.[AttributeId] = T.[AttributeId] And
				S.[AliasId] = T.[AliasId] 
			Cross Apply [AppSecurity].[funcModelAttributeAuthorization](@ModelId, S.[AttributeId], 1)
	Print FormatMessage ('Update [AppModel].[AttributeAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[AttributeAlias] (
			[AttributeId],
			[AliasId],
			[AliasScope])
	Select	S.[AttributeId],
			S.[AliasId],
			S.[AliasScope]
	From	@Values S
			Left Join [AppModel].[AttributeAlias] T
			On	S.[AttributeId] = T.[AttributeId] And
				S.[AliasId] = T.[AliasId] 
			Cross Apply [AppSecurity].[funcModelAttributeAuthorization](@ModelId, S.[AttributeId], 1)
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
