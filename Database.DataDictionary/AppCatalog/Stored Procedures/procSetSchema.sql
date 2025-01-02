CREATE PROCEDURE [AppCatalog].[procSetSchema]
		@CatalogId UniqueIdentifier = Null,
		@SchemaId UniqueIdentifier = Null,
		@Data [AppCatalog].[typeSchema] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DatabaseSchema.
*/

-- Transaction Handling
Declare	@TRN_IsNewTran Bit = 0, -- Indicates that the stored procedure started the transaction. Used to handle nested Transactions
		@RowCount Int = 0 -- @@RowCount is reset just by reading @@RowCount. This is used to persist the value.

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
		From	@Data
				Cross Apply [AppSecurity].[funcCatalogAuthorization](IsNull([CatalogId], @CatalogId), 0)) 
	Throw 601020, 'Catalog Not Authorized', 2;

	If @SchemaId is Not Null And Exists (
		Select	1
		From	@Data D
				Inner Join [AppCatalog].[SchemaHs] T
				On	Coalesce(D.[CatalogId], @CatalogId) = T.[CatalogId] And
				(D.[SchemaId] = T.[SchemaId] Or 
				 D.[SchemaName] = T.[SchemaName])
		Where	IsNull(T.[SchemaId], @SchemaId) <> @SchemaId)
	Throw 602030, '@Data contains other Schemta', 3;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[SchemaId]   UniqueIdentifier Not Null,
		[CatalogId]  UniqueIdentifier Not Null,
		[SchemaName] SysName Not Null,
		Primary Key ([SchemaId]),
		Unique ([CatalogId], [SchemaName]))

	Insert Into @Values
	Select	Coalesce(D.[SchemaId], H.[SchemaId], NewId()) As [SchemaId],
			Coalesce(D.[CatalogId], H.[CatalogId], @CatalogId) As [CatalogId],
			NullIf(Trim(D.[SchemaName]), '') As [SchemaName]
	From	@Data D
			Left Join [AppCatalog].[SchemaHs] H
			On	Coalesce(D.[CatalogId], @CatalogId) = H.[CatalogId] And
				(D.[SchemaId] = H.[SchemaId] Or 
				 D.[SchemaName] = H.[SchemaName])
	Where	(@CatalogId is Null Or @CatalogId = Coalesce(D.[CatalogId], H.[CatalogId])) And
			(@SchemaId is Null Or @SchemaId = Coalesce(D.[SchemaId], H.[SchemaId]))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppCatalog].[ConstraintColumn]
	From	[AppCatalog].[ConstraintColumn] T
			Inner Join [AppCatalog].[ConstraintColumnHs] H
			On	T.[ConstraintColumnId] = H.[ConstraintColumnId]
			Left Join @Values S
			On	H.[SchemaId] = S.[SchemaId]
			Cross Apply [AppSecurity].[funcCatalogAuthorization](H.[CatalogId], 1) 
	Where	S.[SchemaId] is Null And
			(@SchemaId is Not Null Or @CatalogId is Not Null) And
			(@SchemaId is Null Or @SchemaId = H.[SchemaId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[ConstraintColumn] (Schema): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Constraint]
	From	[AppCatalog].[Constraint] T
			Inner Join [AppCatalog].[ConstraintHs] H
			On	T.[ConstraintId] = H.[ConstraintId]
			Left Join @Values S
			On	H.[SchemaId] = S.[SchemaId]
			Cross Apply [AppSecurity].[funcCatalogAuthorization](H.[CatalogId], 1) 
	Where	S.[SchemaId] is Null And
			(@SchemaId is Not Null Or @CatalogId is Not Null) And
			(@SchemaId is Null Or @SchemaId = H.[SchemaId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[Constraint] (Schema): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[RoutineParameter]
	From	[AppCatalog].[RoutineParameter] T
			Inner Join [AppCatalog].[RoutineParameterHs] H
			On	T.[RoutineParameterId] = H.[RoutineParameterId]
			Left Join @Values S
			On	H.[SchemaId] = S.[SchemaId]
			Cross Apply [AppSecurity].[funcCatalogAuthorization](H.[CatalogId], 1) 
	Where	S.[SchemaId] is Null And
			(@SchemaId is Not Null Or @CatalogId is Not Null) And
			(@SchemaId is Null Or @SchemaId = H.[SchemaId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[RoutineParameter] (Schema): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[RoutineColumn]
	From	[AppCatalog].[RoutineColumn] T
			Inner Join [AppCatalog].[RoutineColumnHs] H
			On	T.[RoutineColumnId] = H.[RoutineColumnId]
			Left Join @Values S
			On	H.[SchemaId] = S.[SchemaId]
			Cross Apply [AppSecurity].[funcCatalogAuthorization](H.[CatalogId], 1) 
	Where	S.[SchemaId] is Null And
			(@SchemaId is Not Null Or @CatalogId is Not Null) And
			(@SchemaId is Null Or @SchemaId = H.[SchemaId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[RoutineColumn] (Schema): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Routine]
	From	[AppCatalog].[Routine] T
			Inner Join [AppCatalog].[RoutineHs] H
			On	T.[RoutineId] = H.[RoutineId]
			Left Join @Values S
			On	H.[SchemaId] = S.[SchemaId]
			Cross Apply [AppSecurity].[funcCatalogAuthorization](H.[CatalogId], 1) 
	Where	S.[SchemaId] is Null And
			(@SchemaId is Not Null Or @CatalogId is Not Null) And
			(@SchemaId is Null Or @SchemaId = H.[SchemaId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[Routine] (Schema): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[TableColumn]
	From	[AppCatalog].[TableColumn] T
			Inner Join [AppCatalog].[TableColumnHs] H
			On	T.[TableColumnId] = H.[TableColumnId]
			Left Join @Values S
			On	H.[SchemaId] = S.[SchemaId]
			Cross Apply [AppSecurity].[funcCatalogAuthorization](H.[CatalogId], 1) 
	Where	S.[SchemaId] is Null And
			(@SchemaId is Not Null Or @CatalogId is Not Null) And
			(@SchemaId is Null Or @SchemaId = H.[SchemaId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[TableColumn] (Schema): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Table]
	From	[AppCatalog].[Table] T
			Inner Join [AppCatalog].[TableHs] H
			On	T.[TableId] = H.[TableId]
			Left Join @Values S
			On	H.[SchemaId] = S.[SchemaId]
			Cross Apply [AppSecurity].[funcCatalogAuthorization](H.[CatalogId], 1) 
	Where	S.[SchemaId] is Null And
			(@SchemaId is Not Null Or @CatalogId is Not Null) And
			(@SchemaId is Null Or @SchemaId = H.[SchemaId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[Table] (Schema): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Domain]
	From	[AppCatalog].[Domain] T
			Inner Join [AppCatalog].[DomainHs] H
			On	T.[DomainId] = H.[DomainId]
			Left Join @Values S
			On	H.[SchemaId] = S.[SchemaId]
			Cross Apply [AppSecurity].[funcCatalogAuthorization](H.[CatalogId], 1) 
	Where	S.[SchemaId] is Null And
			(@SchemaId is Not Null Or @CatalogId is Not Null) And
			(@SchemaId is Null Or @SchemaId = H.[SchemaId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[Domain] (Schema): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Schema]
	From	[AppCatalog].[Schema] T
			Inner Join [AppCatalog].[SchemaHs] H
			On	T.[SchemaId] = H.[SchemaId]
			Left Join @Values S
			On	H.[SchemaId] = S.[SchemaId]
			Cross Apply [AppSecurity].[funcCatalogAuthorization](H.[CatalogId], 1) 
	Where	S.[SchemaId] is Null And
			(@SchemaId is Not Null Or @CatalogId is Not Null) And
			(@SchemaId is Null Or @SchemaId = H.[SchemaId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Print FormatMessage ('Delete [AppCatalog].[Schema]: %i, %s', @RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[SchemaId],
				[CatalogId],
				[SchemaName]
		From	@Values
		Except
		Select	[SchemaId],
				[CatalogId],
				[SchemaName]
		From	[AppCatalog].[Schema])
	Update [AppCatalog].[Schema]
	Set		[SchemaName] = S.[SchemaName]
	From	[Delta] S
			Inner Join [AppCatalog].[Schema] T
			On	S.[SchemaId] = T.[SchemaId]
			Cross Apply [AppSecurity].[funcCatalogAuthorization](T.[CatalogId], 1) 
	Print FormatMessage ('Update [AppCatalog].[Schema]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppCatalog].[Schema] (
			[SchemaId],
			[CatalogId],
			[SchemaName])
	Select	S.[SchemaId],
			S.[CatalogId],
			S.[SchemaName]
	From	@Values S
			Left Join [AppCatalog].[Schema] T
			On	S.[SchemaId] = T.[SchemaId]
			Cross Apply [AppSecurity].[funcCatalogAuthorization](S.[CatalogId], 1) 
	Where	T.[SchemaId] is Null
	Print FormatMessage ('Insert [AppCatalog].[Schema]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
