CREATE PROCEDURE [AppModel].[procSetEntityProperty]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null,
		@Data [AppModel].[udttEntityProperty] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on Model EntityProperty.
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
		[PropertyId]		UniqueIdentifier Not Null,
		[PropertyValue]		[AppModel].[uddtPropertyValue] Null,
		Primary Key ([EntityId], [PropertyId]))

	Insert Into @Values
	Select	D.[EntityId],
			D.[PropertyId],
			D.[PropertyValue]
	From	@Data D
			Inner Join [AppModel].[PropertyEnumeration] R
			On	D.[PropertyId] = R.[PropertyId]
	Where	(@EntityId is Null Or @EntityId = D.[EntityId]) And
			(@ModelId is Null Or D.[EntityId] In (
				Select	[EntityId]
				From	[AppModel].[ModelEntity]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Apply Changes
	Delete From [AppModel].[EntityProperty]
	From	[AppModel].[EntityProperty] T
			Left Join @Values V
			On	T.[EntityId] = V.[EntityId] And
				T.[PropertyId] = V.[PropertyId]
			Cross Apply [AppSecurity].[funcModelEntityAuthorization](@ModelId, T.[EntityId], 1)
	Where	V.[EntityId] is Null And
			(@EntityId is Not Null Or @ModelId is Not Null) And
			(@EntityId is Null Or @EntityId = T.[EntityId]) And
			(@ModelId is Null Or T.[EntityId] In (
				Select	[EntityId]
				From	[AppModel].[ModelEntity]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppModel].[EntityProperty]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[EntityId],
				[PropertyId],
				[PropertyValue]
		From	@Values
		Except
		Select	[EntityId],
				[PropertyId],
				[PropertyValue]
		From	[AppModel].[EntityProperty])
	Update [AppModel].[EntityProperty]
	Set		[PropertyValue] = S.[PropertyValue]
	From	[Delta] S
			Inner Join [AppModel].[EntityProperty] T
			On	S.[EntityId] = T.[EntityId] And
				S.[PropertyId] = T.[PropertyId]
			Cross Apply [AppSecurity].[funcModelEntityAuthorization](@ModelId, S.[EntityId], 1)
	Print FormatMessage ('Update [AppModel].[EntityProperty]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[EntityProperty] (
			[EntityId],
			[PropertyId],
			[PropertyValue])
	Select	S.[EntityId],
			S.[PropertyId],
			S.[PropertyValue]
	From	@Values S
			Left Join [AppModel].[EntityProperty] T
			On	S.[EntityId] = T.[EntityId] And
				S.[PropertyId] = T.[PropertyId]
			Cross Apply [AppSecurity].[funcModelEntityAuthorization](@ModelId, S.[EntityId], 1)
	Where	T.[EntityId] is Null
	Print FormatMessage ('Insert [AppModel].[EntityProperty]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[ModelProperty] (
		[ModelId],
		[PropertyId])
	Select	@ModelId,
			S.[PropertyId]
	From	@Values S
			Left Join [AppModel].[ModelProperty] T
			On	S.[PropertyId] = T.[PropertyId] And
				[ModelId] = @ModelId
			Cross Apply [AppSecurity].[funcModelAuthorization](@ModelId, 1)
	Where	T.[PropertyId] is Null
	Group By S.[PropertyId]
	Print FormatMessage ('Insert [AppModel].[ModelProperty]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));


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