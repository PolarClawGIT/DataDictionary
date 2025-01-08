CREATE PROCEDURE [AppModel].[procSetEntityAttribute]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null,
		@Data [AppModel].[typeEntityAttribute] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on Model EntityAttribute.
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
		[EntityAttributeId]    UniqueIdentifier Not Null,
		[EntityId]			   UniqueIdentifier Not Null,
		[AttributeAlias]       [App_DataDictionary].[typeTitle] Null,
		[AttributeName]        [AppModel].[typeQualifiedName] Null,
		[OrdinalPosition]      Int Not Null,
		[IsNullable]		   Bit Null,
		[IsPrimaryKey]		   Bit Null,
		Primary Key ([EntityAttributeId]),
		Unique ([EntityId], [AttributeAlias]))

	Insert Into @Values
	Select	Coalesce(H.[EntityAttributeId], NewId()) As [EntityAttributeId],
			D.[EntityId],
			NullIf(Trim(D.[AttributeAlias]),'') As [AttributeAlias],
			NullIf(Trim(D.[AttributeName]),'') As [AttributeName],
			D.[OrdinalPosition],
			IsNull(D.[IsNullable],0) As [IsNullable],
			IsNull(D.[IsPrimaryKey],0) As [IsPrimaryKey]
	From	@Data D
			Left Join [AppModel].[EntityAttributeHs] H
			On	D.[EntityId] = H.[EntityId] And
				D.[AttributeAlias] = H.[AttributeAlias]
			Cross Apply (
				Select	[QualifiedName] As [AttributeName]
				From	[AppModel].[funcParseName](D.[AttributeName])
				Where	[IsBase] = 1) N
	Where	(@EntityId is Null Or @EntityId = D.[EntityId]) And
			(@ModelId is Null Or D.[EntityId] In (
				Select	[EntityId]
				From	[AppModel].[ModelEntity]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppModel].[EntityAttribute]
	From	[AppModel].[EntityAttribute] T
			Left Join @Values V
			On	T.[EntityAttributeId] = V.[EntityAttributeId]
			Cross Apply [AppSecurity].[funcModelEntityAuthorization](@ModelId, T.[EntityId], 1)
	Where	V.[EntityAttributeId] is Null And
			(@EntityId is Not Null Or @ModelId is Not Null) And
			(@EntityId is Null Or @EntityId = T.[EntityId])  And
			(@ModelId is Null Or T.[EntityId] In (
				Select	[EntityId]
				From	[AppModel].[ModelEntity]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppModel].[EntityAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[EntityAttributeId],
				[EntityId],
				[AttributeAlias],
				[AttributeName],
				[OrdinalPosition],
				[IsNullable],
				[IsPrimaryKey]
		From	@Values
		Except
		Select	[EntityAttributeId],
				[EntityId],
				[AttributeAlias],
				[AttributeName],
				[OrdinalPosition],
				[IsNullable],
				[IsPrimaryKey]
		From	[AppModel].[EntityAttribute])
	Update [AppModel].[EntityAttribute]
	Set		[AttributeAlias] = S.[AttributeAlias],
			[AttributeName] = S.[AttributeName],
			[OrdinalPosition] = S.[OrdinalPosition],
			[IsNullable] = S.[IsNullable],
			[IsPrimaryKey] = S.[IsPrimaryKey]
	From	[AppModel].[EntityAttribute] T
			Inner Join [Delta] S
			On	T.[EntityAttributeId] = S.[EntityAttributeId]
			Cross Apply [AppSecurity].[funcModelEntityAuthorization](@ModelId, S.[EntityId], 1)
	Print FormatMessage ('Update [AppModel].[EntityAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[EntityAttribute] (
			[EntityAttributeId],
			[EntityId],
			[AttributeAlias],
			[AttributeName],
			[OrdinalPosition],
			[IsNullable],
			[IsPrimaryKey])
	Select	S.[EntityAttributeId],
			S.[EntityId],
			S.[AttributeAlias],
			S.[AttributeName],
			S.[OrdinalPosition],
			S.[IsNullable],
			S.[IsPrimaryKey]
	From	@Values S
			Left Join [AppModel].[EntityAttribute] T
			On	S.[EntityAttributeId] = T.[EntityAttributeId]
			Cross Apply [AppSecurity].[funcModelEntityAuthorization](@ModelId, S.[EntityId], 1)
	Where	T.[EntityAttributeId] is Null
	Print FormatMessage ('Insert [AppModel].[Entity]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));


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
