CREATE PROCEDURE [AppCatalog].[procSetCatalog]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@Data [AppCatalog].[typeCatalog] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DatabaseCatalog.
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
	Throw 50000, 'CatalogId in @Data contains Catalogs that do not match @CatalogId', 1;

	If Exists (
		Select	1
		From	@Data
		Group By IsNull([CatalogId], @CatalogId)
		Having Count(*) > 1)
	Throw 50000, 'Duplicate CatalogId in @Data are not allowed', 11;

	If @ModelId is not null And Exists (
		Select	1
		From	@Data
		Group By [DatabaseName]
		Having Count(*) > 1)
	Throw 50000, 'Duplicate Database Name are not allowed for a Model', 12;

	-- Clean the Data, helps performance
	Declare @Values Table ( -- Needs to match the target data structure
		[CatalogId] UniqueIdentifier Not Null,
		[CatalogTitle] [App_DataDictionary].[typeTitle] Not Null,
		[CatalogDescription] [App_DataDictionary].[typeDescription] Null,
		[ServerName] SysName Not Null,
		[DatabaseName] SysName Not Null,
		[SourceDate] DateTime Not Null,
		Primary Key ([CatalogId]))

	Insert Into @Values
	Select	Coalesce(D.[CatalogId], @CatalogId, NewId()),
			NullIf(Trim(IsNull(D.[CatalogTitle], D.[DatabaseName])),'') As [CatalogTitle],
			NullIf(Trim(D.[CatalogDescription]), '') As [CatalogDescription],
			NullIf(Trim(D.[ServerName]), '') As [SourceServerName],
			NullIf(Trim(D.[DatabaseName]), '') As [SourceDatabaseName],
			IsNull(D.[SourceDate], GetDate()) As [SourceDate]
	From	@Data D
	Where	(@CatalogId is Null Or @CatalogId = Coalesce(D.[CatalogId], @CatalogId))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog]

	-- Deal with Ownership, Sets up Row Level Security
	Insert Into [AppSecurity].[SecurableOwner] (
			[PrincipalId],
			[SecurableId])
	Select	S.[PrincipalId],
			V.[CatalogId]
	From	@Values V
			Cross Apply [AppSecurity].[funcAuthorization](V.[CatalogId]) S
	Where	S.[IsCatalogOwner] = 1 And
			S.[IsCatalogAdmin] = 0 And
			S.[HasOwner] = 0 And
			S.[PrincipalId] is not null
	Set @RowCount = @@RowCount
	If @RowCount > 0 Print FormatMessage ('Insert [AppSecurity].[SecurityOwner]: %i, %s', @RowCount, Convert(VarChar,GetDate()));

	-- Apply Changes (To Delete, the @CatalogId must be specified)
	Delete From [App_DataDictionary].[ModelCatalog]
	From	[App_DataDictionary].[ModelCatalog] T
			Left Join @Values S
			On	T.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			T.[ModelId] = @ModelId -- @ModelId must be specfied
	Print FormatMessage ('Delete [App_DataDictionary].[ModelCatalog] (Catalog): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Reference]
	From	[AppCatalog].[Reference] T
			Inner Join [AppCatalog].[ReferenceHs] H
			On	T.[ReferenceId] = H.[ReferenceId]
			Left Join @Values S
			On	H.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			H.[CatalogId] = @CatalogId
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[Reference] (Catalog): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Property]
	From	[AppCatalog].[Property] T
			Inner Join [AppCatalog].[PropertyHs] H
			On	T.[PropertyId] = H.[PropertyId]
			Left Join @Values S
			On	H.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null  And
			T.[CatalogId] = @CatalogId
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[Property] (Catalog): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[ConstraintColumn]
	From	[AppCatalog].[ConstraintColumn] T
			Inner Join [AppCatalog].[ConstraintColumnHS] H
			On	T.[ConstraintColumnId] = H.[ConstraintColumnId]
			Left Join @Values S
			On	H.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			H.[CatalogId] = @CatalogId
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[ConstraintColumn] (Catalog): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Constraint]
	From	[AppCatalog].[Constraint] T
			Inner Join [AppCatalog].[ConstraintHs] H
			On	T.[ConstraintId] = H.[ConstraintId]
			Left Join @Values S
			On	H.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			H.[CatalogId] = @CatalogId
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[Constraint] (Catalog): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[RoutineParameter]
	From	[AppCatalog].[RoutineParameter] T
			Inner Join [AppCatalog].[RoutineParameterHs] H
			On	T.[ParameterId] = H.[ParameterId]
			Left Join @Values S
			On	H.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			H.[CatalogId] = @CatalogId
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[RoutineParameter] (Catalog): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[RoutineColumn]
	From	[AppCatalog].[RoutineColumn] T
			Inner Join [AppCatalog].[RoutineColumnHs] H
			On	T.[ColumnId] = H.[ColumnId]
			Left Join @Values S
			On	H.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			H.[CatalogId] = @CatalogId
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[RoutineColumn] (Catalog): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Routine]
	From	[AppCatalog].[Routine] T
			Inner Join [AppCatalog].[RoutineHs] H
			On	T.[RoutineId] = H.[RoutineId]
			Left Join @Values S
			On	H.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			H.[CatalogId] = @CatalogId
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[Routine] (Catalog): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[TableColumn]
	From	[AppCatalog].[TableColumn] T
			Inner Join [AppCatalog].[TableColumnHs] H
			On	T.[ColumnId] = H.[ColumnId]
			Left Join @Values S
			On	H.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			H.[CatalogId] = @CatalogId
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[TableColumn] (Catalog): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Table]
	From	[AppCatalog].[Table] T
			Inner Join [AppCatalog].[TableHs] H
			On	T.[TableId] = H.[TableId]
			Left Join @Values S
			On	H.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			H.[CatalogId] = @CatalogId
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[Table] (Catalog): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Domain]
	From	[AppCatalog].[Domain] T
			Inner Join [AppCatalog].[DomainHs] H
			On	T.[DomainId] = H.[DomainId]
			Left Join @Values S
			On	H.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			H.[CatalogId] = @CatalogId
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[Domain] (Catalog): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Schema]
	From	[AppCatalog].[Schema] T
			Inner Join [AppCatalog].[SchemaHs] H
			On	T.[SchemaId] = H.[SchemaId]
			Left Join @Values S
			On	H.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			H.[CatalogId] = @CatalogId
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[Schema] (Catalog): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Catalog]
	From	[AppCatalog].[Catalog] T
			Left Join @Values S
			On	T.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			T.[CatalogId] = @CatalogId
	Print FormatMessage ('Delete [AppCatalog].[Catalog]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[CatalogId],
				[CatalogTitle],
				[CatalogDescription],
				[ServerName],
				[DatabaseName],
				[SourceDate]
		From	@Values
		Except
		Select	[CatalogId],
				[CatalogTitle],
				[CatalogDescription],
				[ServerName],
				[DatabaseName],
				[SourceDate]
		From	[AppCatalog].[Catalog])
	Update [AppCatalog].[Catalog]
	Set		[CatalogTitle] = S.[CatalogTitle],
			[CatalogDescription] = S.[CatalogDescription],
			[ServerName] = S.[ServerName],
			[DatabaseName] = S.[DatabaseName],
			[SourceDate] = S.[SourceDate]
	From	[AppCatalog].[Catalog] T
			Inner Join [Delta] S
			On	T.[CatalogId] = S.[CatalogId]
	Print FormatMessage ('Update [AppCatalog].[Catalog]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppCatalog].[Catalog] (
			[CatalogId],
			[CatalogTitle],
			[CatalogDescription],
			[ServerName],
			[DatabaseName],
			[SourceDate])
	Select	S.[CatalogId],
			S.[CatalogTitle],
			S.[CatalogDescription],
			S.[ServerName],
			S.[DatabaseName],
			S.[SourceDate]
	From	@Values S
			Left Join [AppCatalog].[Catalog] T
			On	S.[CatalogId] = T.[CatalogId]
	Where	T.[CatalogId] is Null
	Print FormatMessage ('Insert [AppCatalog].[Catalog]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ModelCatalog] ([ModelId], [CatalogId])
	Select	@ModelId As [ModelId],
			S.[CatalogId]
	from	@Values S
			Left Join [App_DataDictionary].[ModelCatalog] T
			On	S.[CatalogId] = T.[CatalogId] And
				@ModelId = T.[ModelId]
	Where	T.[ModelId] is Null And
			@ModelId is Not Null
	Print FormatMessage ('Insert [App_DataDictionary].[ModelCatalog]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
