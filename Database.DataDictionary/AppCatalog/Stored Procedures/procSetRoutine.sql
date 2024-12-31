CREATE PROCEDURE [AppCatalog].[procSetRoutine]
		@CatalogId UniqueIdentifier = Null,
		@RoutineId UniqueIdentifier = Null,
		@Data [AppCatalog].[typeRoutine] ReadOnly
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
	If @CatalogId is Not Null And
		Exists (
			Select	1
			From	@Data
			Where	IsNull([CatalogId], @CatalogId) <> @CatalogId)
	Throw 601010, '@Data contains other Catalogs', 1;

	If Exists (
		Select	1
		From	@Data D
				Cross Apply [AppSecurity].[funcCatalogAuthorization](IsNull(D.[CatalogId], @CatalogId), 0))
	Throw 601020, 'Catalog Not Authorized', 2;

	-- Clean the Data
	Declare @Values Table (
		[RoutineId]          UniqueIdentifier Not Null,
		[SchemaId]           UniqueIdentifier Not Null,
		[RoutineName]        SysName Not Null,
		[RoutineType]        [App_DataDictionary].[typeObjectType] Null,
		Primary Key ([RoutineId]),
		Unique ([SchemaId], [RoutineName]))

	Insert Into @Values
	Select	Coalesce(D.[RoutineId], H.[RoutineId], NewId()) As [RoutineId],
			S.[SchemaId],
			NullIf(Trim(D.[RoutineName]),'') As [RoutineName],
			NullIf(Trim(D.[RoutineType]),'') As [RoutineType]
	From	@Data D
			Left Join [AppCatalog].[RoutineHs] H
			On	Coalesce(D.[CatalogId], @CatalogId) = H.[CatalogId] And
				(D.[RoutineId] = H.[RoutineId] Or
				 (D.[SchemaName] = H.[SchemaName] And
				  D.[RoutineName] = H.[RoutineName]))
			Left Join [AppCatalog].[SchemaHs] S
			On	Coalesce(D.[CatalogId], @CatalogId) = S.[CatalogId] And
				D.[SchemaName] = S.[SchemaName]
	Where	(@CatalogId is Null Or @CatalogId = Coalesce(D.[CatalogId], H.[CatalogId])) And
			(@RoutineId is Null Or @RoutineId = Coalesce(D.[RoutineId], H.[RoutineId]))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppCatalog].[RoutineParameter]
	From	[AppCatalog].[RoutineParameter] T
			Inner Join [AppCatalog].[RoutineParameterHs] H
			On	T.[RoutineParameterId] = H.[RoutineParameterId]
			Left Join @Values S
			On	H.[RoutineId] = S.[RoutineId]
			Cross Apply [AppSecurity].[funcCatalogAuthorization](H.[CatalogId], 1)
	Where	S.[SchemaId] is Null And
			(@RoutineId is Not Null Or @CatalogId is Not Null) And
			(@RoutineId is Null Or @RoutineId = H.[RoutineId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Print FormatMessage ('Delete [AppCatalog].[RoutineParameter] (Routine): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[RoutineColumn]
	From	[AppCatalog].[RoutineColumn] T
			Inner Join [AppCatalog].[RoutineColumnHs] H
			On	T.[RoutineColumnId] = H.[RoutineColumnId]
			Left Join @Values S
			On	H.[RoutineId] = S.[RoutineId]
			Cross Apply [AppSecurity].[funcCatalogAuthorization](H.[CatalogId], 1)
	Where	S.[SchemaId] is Null And
			(@RoutineId is Not Null Or @CatalogId is Not Null) And
			(@RoutineId is Null Or @RoutineId = H.[RoutineId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Print FormatMessage ('Delete [AppCatalog].[RoutineColumn] (Routine): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Routine]
	From	[AppCatalog].[Routine] T
			Inner Join [AppCatalog].[RoutineHs] H
			On	T.[RoutineId] = H.[RoutineId]
			Left Join @Values S
			On	H.[RoutineId] = S.[RoutineId]
			Cross Apply [AppSecurity].[funcCatalogAuthorization](H.[CatalogId], 1)
	Where	S.[SchemaId] is Null And
			(@RoutineId is Not Null Or @CatalogId is Not Null) And
			(@RoutineId is Null Or @RoutineId = H.[RoutineId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Print FormatMessage ('Delete [AppCatalog].[Routine] (Routine): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

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
		From	[AppCatalog].[Routine])
	Update [AppCatalog].[Routine]
	Set		[SchemaId] = S.[SchemaId],
			[RoutineName] = S.[RoutineName],
			[RoutineType] = S.[RoutineType]
	From	[AppCatalog].[Routine] T
			Inner Join [Delta] S
			On	T.[RoutineId] = S.[RoutineId]
	Where	T.[SchemaId] In (
				Select	[SchemaId]
				From	[AppCatalog].[SchemaHs]
						Cross Apply [AppSecurity].[funcCatalogAuthorization]([CatalogId], 1))
	Print FormatMessage ('Update [App_DataDictionary].[DatabaseRoutine]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppCatalog].[Routine] (
		[RoutineId],
		[SchemaId],
		[RoutineName],
		[RoutineType])
	Select	S.[RoutineId],
			S.[SchemaId],
			S.[RoutineName],
			S.[RoutineType]
	From	@Values S
			Left Join [AppCatalog].[Routine] T
			On	S.[RoutineId] = T.[RoutineId]
	Where	T.[RoutineId] is Null And
			S.[SchemaId] In (
				Select	[SchemaId]
				From	[AppCatalog].[SchemaHs]
						Cross Apply [AppSecurity].[funcCatalogAuthorization]([CatalogId], 1))
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