CREATE PROCEDURE [AppModel].[procSetRelationshipAlias]
		@ModelId UniqueIdentifier = Null,
		@RelationshipId UniqueIdentifier = Null,
		@Data [AppModel].[typeRelationshipAlias] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on Model RelationshipAlias.
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
				Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](@ModelId, [RelationshipId], 0))
	Throw 601020, 'Model Not Authorized', 2;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[RelationshipId]	UniqueIdentifier Not Null,
		[AliasId]			UniqueIdentifier Not Null,
		[AliasScope]		[AppModel].[typeScopeName] NOT NULL,
		--Unique ([AliasId], [AliasPath]) -- Cannot Index, [AliasPath] is too long
		Primary Key([RelationshipId], [AliasId]))

	Declare @Alias [AppModel].[typeAlias];

	Insert Into @Alias ([AliasNameSpace])
	Select	[AliasPath]
	From	@Data

	Exec [AppModel].[procSetAlias] @ModelId = @ModelId, @Data = @Alias

	Insert Into @Values
	Select	D.[RelationshipId],
			[AppModel].[funcAliasId] (D.[AliasPath]) As [AliasId],
			D.[AliasScope]
	From	@Data D
	Where	(@RelationshipId is Null Or @RelationshipId = D.[RelationshipId]) And
			(@ModelId is Null Or D.[RelationshipId] In (
				Select	[RelationshipId]
				From	[AppModel].[ModelRelationship]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppModel].[RelationshipAlias]
	From	[AppModel].[RelationshipAlias] T
			Left Join @Values V
			On	T.[RelationshipId] = V.[RelationshipId] And
				T.[AliasId] = V.[AliasId]
			Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](@ModelId, T.[RelationshipId], 1)
	Where	V.[RelationshipId] is Null And
			(@RelationshipId is Not Null Or @ModelId is Not Null) And
			(@RelationshipId is Null Or @RelationshipId = T.[RelationshipId])  And
			(@ModelId is Null Or T.[RelationshipId] In (
				Select	[RelationshipId]
				From	[AppModel].[ModelRelationship]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppModel].[RelationshipAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[RelationshipId],
				[AliasId],				
				[AliasScope]
		From	@Values S
		Except
		Select	[RelationshipId],
				[AliasId],				
				[AliasScope]
		From	[AppModel].[RelationshipAlias])
	Update	[AppModel].[RelationshipAlias]
	Set		[AliasScope] = S.[AliasScope]
	From	[Delta] S
			Inner Join [AppModel].[RelationshipAlias] T
			On	S.[RelationshipId] = T.[RelationshipId] And
				S.[AliasId] = T.[AliasId]
			Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](@ModelId, S.[RelationshipId], 1)
	Print FormatMessage ('Update [AppModel].[RelationshipAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[RelationshipAlias] (
			[RelationshipId],
			[AliasId],
			[AliasScope])
	Select	S.[RelationshipId],
			S.[AliasId],
			S.[AliasScope]
	From	@Values S
			Left Join [AppModel].[RelationshipAlias] T
			On	S.[RelationshipId] = T.[RelationshipId] And
				S.[AliasId] = T.[AliasId]
			Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](@ModelId, S.[RelationshipId], 1)
	Where	T.[RelationshipId] is Null
	Print FormatMessage ('Insert [AppModel].[RelationshipAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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