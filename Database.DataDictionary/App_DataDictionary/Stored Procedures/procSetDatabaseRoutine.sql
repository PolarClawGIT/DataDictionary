CREATE PROCEDURE [App_DataDictionary].[procSetDatabaseRoutine]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeDatabaseRoutine] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DatabaseRoutine.
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
	If @ModelId is Null and @CatalogId is Null
	Throw 50000, '@ModelId or @CatalogId must be specified', 1;

	-- Clean the Data
	Declare @Values Table (
		[RoutineId]          UniqueIdentifier Not Null,
		[SchemaId]           UniqueIdentifier Not Null,
		[RoutineName]        SysName Not Null,
		[RoutineType]        [App_DataDictionary].[typeObjectType] Null,
		Primary Key ([RoutineId]))

	Insert Into @Values
	Select	X.[RoutineId],  
			X.[SchemaId],
			NullIf(Trim(D.[RoutineName]),'') As [RoutineName],
			NullIf(Trim(D.[RoutineType]),'') As [RoutineType]
	From	@Data D
			Inner Join [App_DataDictionary].[DatabaseSchema_AK] P
			On	D.[DatabaseName] = P.[DatabaseName] And
				D.[SchemaName] = P.[SchemaName]
			Left Join [App_DataDictionary].[DatabaseRoutine_AK] A
			On	D.[DatabaseName] = A.[DatabaseName] And
				D.[SchemaName] = A.[SchemaName] And
				D.[RoutineName] = A.[RoutineName]
			Cross Apply (
				Select	Coalesce(A.[RoutineId], D.[RoutineId], NewId()) As [RoutineId],
						Coalesce(A.[SchemaId], P.[SchemaId]) As [SchemaId],
						Coalesce(A.[CatalogId], P.[CatalogId], @CatalogId) As [CatalogId]) X
	Where	@CatalogId is Null or
			X.[CatalogId] = @CatalogId or
			X.[CatalogId] In (
			Select	A.[CatalogId]
			From	[App_DataDictionary].[DatabaseCatalog] A
					Left Join [App_DataDictionary].[ModelCatalog] C
					On	A.[CatalogId] = C.[CatalogId]
			Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
					(@ModelId is Null Or @ModelId = C.[ModelId]))

	-- Apply Changes
	Delete From [App_DataDictionary].[DatabaseRoutineDependency]
	From	[App_DataDictionary].[DatabaseRoutineDependency] T
			Inner Join [App_DataDictionary].[DatabaseRoutine_AK] P
			On	T.[RoutineId] = P.[RoutineId]
			Left Join @Values S
			On	T.[RoutineId] = S.[RoutineId]
	Where	S.[RoutineId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseRoutineDependency] (Routine): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseRoutineParameter]
	From	[App_DataDictionary].[DatabaseRoutineParameter] T
			Inner Join [App_DataDictionary].[DatabaseRoutine_AK] P
			On	T.[RoutineId] = P.[RoutineId]
			Left Join @Values S
			On	T.[RoutineId] = S.[RoutineId]
	Where	S.[RoutineId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseRoutineParameter] (Routine): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseRoutine]
	From	[App_DataDictionary].[DatabaseRoutine] T
			Inner Join [App_DataDictionary].[DatabaseSchema_AK] P
			On	T.[SchemaId] = P.[SchemaId]
			Left Join @Values S
			On	T.[RoutineId] = S.[RoutineId]
	Where	S.[RoutineId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseRoutine]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[RoutineId],
				[SchemaId],
				[RoutineName],
				[RoutineType]
		From	@Values
		Except
		Select	[RoutineId],
				[SchemaId],
				[RoutineName],
				[RoutineType]
		From	[App_DataDictionary].[DatabaseRoutine])
	Update [App_DataDictionary].[DatabaseRoutine]
	Set		[SchemaId] = S.[SchemaId],
			[RoutineName] = S.[RoutineName],
			[RoutineType] = S.[RoutineType]
	From	[App_DataDictionary].[DatabaseRoutine] T
			Inner Join [Delta] S
			On	T.[RoutineId] = S.[RoutineId]
	Print FormatMessage ('Update [App_DataDictionary].[DatabaseRoutine]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DatabaseRoutine] (
		[RoutineId],
		[SchemaId],
		[RoutineName],
		[RoutineType])
	Select	S.[RoutineId],
			S.[SchemaId],
			S.[RoutineName],
			S.[RoutineType]
	From	@Values S
			Left Join [App_DataDictionary].[DatabaseRoutine] T
			On	S.[RoutineId] = T.[RoutineId]
	Where	T.[RoutineId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DatabaseRoutine]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
	-- Debug Data
	Print FormatMessage ('*** Error Report: %s ***', Object_Name(@@ProcID))
	Print FormatMessage (' Message- %s', ERROR_MESSAGE())
	Print FormatMessage (' Number- %i', ERROR_NUMBER())
	Print FormatMessage (' Severity- %i', ERROR_SEVERITY())
	Print FormatMessage (' State- %i', ERROR_STATE())
	Print FormatMessage (' Procedure- %s', ERROR_PROCEDURE())
	Print FormatMessage (' Line- %i', ERROR_LINE())
	Print FormatMessage (' @@TranCount - %i', @@TranCount)
	Print FormatMessage (' @@NestLevel - %i', @@NestLevel)
	Print FormatMessage (' Original_Login - %s', Original_Login())
	Print FormatMessage (' Current_User - %s', Current_User)
	Print FormatMessage (' XAct_State - %i', XAct_State())
	Print '*** Debug Report ***'

	Print FormatMessage ('*** End Report: %s ***', Object_Name(@@ProcID))

	-- Rollback Transaction
	If @TRN_IsNewTran = 1
	  Begin -- If this is the outer transaction, roll it back
		Rollback Transaction
		Print FormatMessage ('Rollback Transaction Issued ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
	  End -- Rollback Transaction
	-- This is a nested transaction, must be rolled back by outer transaction
	Else Print FormatMessage ('Rollback Transaction Pending ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))

	If ERROR_SEVERITY() Not In (0, 11) Throw -- Re-throw the Error
End Catch
GO