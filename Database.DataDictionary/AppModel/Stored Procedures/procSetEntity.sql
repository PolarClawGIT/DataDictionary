CREATE PROCEDURE [AppModel].[procSetEntity]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null,
		@Data [AppModel].[typeEntity] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on Model Entity.
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
		[EntityId]			    UniqueIdentifier Not Null,
		[EntityTitle]		    [App_DataDictionary].[typeTitle] Not Null,
		[EntityDescription]	    [App_DataDictionary].[typeDescription] Null,
		[EntityName]			[AppModel].[typeQualifiedName] Null,
		Primary Key ([EntityId]),
		Unique ([EntityTitle]))

	Insert Into @Values
	Select	Coalesce(D.[EntityId], H.[EntityId], NewId()) As [EntityId],
			NullIf(Trim(D.[EntityTitle]),'') As [EntityTitle],
			NullIf(Trim(D.[EntityDescription]),'') As [EntityDescription],
			N.[EntityName]
	From	@Data D
			Left Join [AppModel].[ModelEntityHs] H
			On	(D.[EntityId] = H.[EntityId] Or
				 (H.[ModelId] = @ModelId And
				  D.[EntityTitle] = H.[EntityTitle]))
			Cross Apply (
				Select	[QualifiedName] As [EntityName]
				From	[AppModel].[funcParseName](D.[EntityName])
				Where	[IsBase] = 1) N
	Where	(@ModelId is Null Or @ModelId = H.[ModelId]) And
			(@EntityId is Null Or @EntityId = Coalesce(D.[EntityId], H.[EntityId]))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Declare @Delete Table ([EntityId] UniqueIdentifier Not Null)

	Insert Into @Delete
	Select	T.[EntityId]
	From	[AppModel].[Entity] T
			Left Join @Values S
			On	T.[EntityId] = S.[EntityId]
	Where	S.[EntityId] is Null And
			(@EntityId is Not Null Or @ModelId is Not Null) And
			(@EntityId is Null Or @EntityId = T.[EntityId])  And
			(@ModelId is Null Or T.[EntityId] In (
				Select	[EntityId]
				From	[AppModel].[ModelEntity]
				Group By [EntityId]
				Having Sum(Case When [ModelId] = @ModelId Then 0 Else 1 End) = 0))

	Delete From [AppModel].[ModelEntity]
	From	[AppModel].[ModelEntity] T
			Left Join @Values S
			On	T.[EntityId] = S.[EntityId]
	Where	S.[EntityId] is Null And
			(@EntityId is Not Null Or @ModelId is Not Null) And
			(@EntityId is Null Or @EntityId = T.[EntityId])  And
			(@ModelId is Null Or @ModelId = T.[ModelId])
	Print FormatMessage ('Delete [AppModel].[ModelEntity] (Entity): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[EntityAlias]
	From	[AppModel].[EntityAlias] T
			Left Join @Values S
			On	T.[EntityId] = S.[EntityId]
	Where	S.[EntityId] is Null And
			T.[EntityId] In (Select [EntityId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[EntityAlias] (Entity): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[EntityDefinition]
	From	[AppModel].[EntityDefinition] T
			Left Join @Values S
			On	T.[EntityId] = S.[EntityId]
	Where	S.[EntityId] is Null And
			T.[EntityId] In (Select [EntityId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[EntityDefinition] (Entity): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[EntityProperty]
	From	[AppModel].[EntityProperty] T
			Left Join @Values S
			On	T.[EntityId] = S.[EntityId]
	Where	S.[EntityId] is Null And
			T.[EntityId] In (Select [EntityId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[EntityProperty] (Entity): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[EntitySubjectArea]
	From	[AppModel].[EntitySubjectArea] T
			Left Join @Values S
			On	T.[EntityId] = S.[EntityId]
	Where	S.[EntityId] is Null And
			T.[EntityId] In (Select [EntityId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[EntitySubjectArea] (Entity): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[EntityAttribute]
	From	[AppModel].[EntityAttribute] T
			Left Join @Values S
			On	T.[EntityId] = S.[EntityId]
	Where	S.[EntityId] is Null And
			T.[EntityId] In (Select [EntityId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[EntityAttribute] (Entity): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[Entity]
	From	[AppModel].[Entity] T
			Left Join @Values S
			On	T.[EntityId] = S.[EntityId]
	Where	S.[EntityId] is Null And
			T.[EntityId] In (Select [EntityId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[Entity]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[EntityId],
				[EntityTitle],
				[EntityDescription],
				[EntityName]
		From	@Values
		Except
		Select	[EntityId],
				[EntityTitle],
				[EntityDescription],
				[EntityName]
		From	[AppModel].[Entity])
	Update [AppModel].[Entity]
	Set		[EntityTitle] = S.[EntityTitle],
			[EntityDescription] = S.[EntityDescription],
			[EntityName] = S.[EntityName]
	From	[AppModel].[Entity] T
			Inner Join [Delta] S
			On	T.[EntityId] = S.[EntityId]
	Print FormatMessage ('Update [AppModel].[Entity]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[Entity] (
			[EntityId],
			[EntityTitle],
			[EntityDescription],
			[EntityName])
	Select	S.[EntityId],
			S.[EntityTitle],
			S.[EntityDescription],
			S.[EntityName]
	From	@Values S
			Left Join [AppModel].[Entity] T
			On	S.[EntityId] = T.[EntityId]
	Where	T.[EntityId] is Null
	Print FormatMessage ('Insert [AppModel].[Entity]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[ModelEntity] (
			[ModelId],
			[EntityId])
	Select	@ModelId As [ModelId],
			S.[EntityId]
	From	@Values S
			Left Join [AppModel].[ModelEntity] T
			On	S.[EntityId] = T.[EntityId] And
				@ModelId = T.[ModelId]
	Where	T.[EntityId] Is Null
	Print FormatMessage ('Insert [AppModel].[ModelEntity]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
