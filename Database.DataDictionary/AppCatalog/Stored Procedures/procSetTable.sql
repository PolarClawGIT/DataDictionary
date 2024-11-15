CREATE PROCEDURE [AppCatalog].[procSetTable]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@Data [AppCatalog].[typeTable] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DatabaseTable.
*/
; Throw 50000, 'TODO: Fix for Temporal Data', 1;
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

	-- Clean the Data, helps performance
	Declare @Values Table (
		[TableId]   UniqueIdentifier Not Null,
		[SchemaId]  UniqueIdentifier Not Null,
		[TableName] SysName Not Null,
		[TableType] [App_DataDictionary].[typeObjectType] Null,
		Primary Key ([TableId]))

	Insert Into @Values
	Select	X.[TableId],
			X.[SchemaId],
			NullIf(Trim(D.[TableName]),'') As [TableName],
			NullIf(Trim(D.[TableType]),'') As [TableType]
	From	@Data D
			Inner Join [App_DataDictionary].[DatabaseSchema_AK] P
			On	D.[DatabaseName] = P.[DatabaseName] And
				D.[SchemaName] = P.[SchemaName]
			Left Join [AppCatalog].[TableHs] A
			On	D.[DatabaseName] = A.[DatabaseName] And
				D.[SchemaName] = A.[SchemaName] And
				D.[TableName] = A.[TableName]
			Cross Apply (
				Select	Coalesce(A.[TableId], D.[TableId], NewId()) As [TableId],
						Coalesce(A.[SchemaId], P.[SchemaId]) As [SchemaId],
						Coalesce(A.[CatalogId], P.[CatalogId], @CatalogId) As [CatalogId]) X
	Where	@CatalogId is Null or
			X.[CatalogId] = @CatalogId or
			X.[CatalogId] In (
			Select	A.[CatalogId]
			From	[AppCatalog].[Catalog] A
					Left Join [App_DataDictionary].[ModelCatalog] C
					On	A.[CatalogId] = C.[CatalogId]
			Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
					(@ModelId is Null Or @ModelId = C.[ModelId]))

	-- Apply Changes
	Delete From [AppCatalog].[ConstraintColumn]
	From	[AppCatalog].[ConstraintColumn] T
			Inner Join [AppCatalog].[ConstraintHs] P
			On	T.[ConstraintId] = P.[ConstraintId]
			Left Join @Values S
			On	P.[TableId] = S.[TableId]
	Where	S.[TableId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[AppCatalog].[Catalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseConstraintColumn] (table): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Constraint]
	From	[AppCatalog].[Constraint] T
			Inner Join [App_DataDictionary].[DatabaseSchema_AK] P
			On	T.[SchemaId] = P.[SchemaId]
			Left Join @Values S
			On	T.[TableId] = S.[TableId]
	Where	S.[TableId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[AppCatalog].[Catalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseConstraint] (table): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[TableColumn]
	From	[AppCatalog].[TableColumn] T
			Inner Join [AppCatalog].[TableHs] P
			On	T.[TableId] = P.[TableId]
			Left Join @Values S
			On	P.[TableId] = S.[TableId]
	Where	S.[TableId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[AppCatalog].[Catalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseTableColumn] (table): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Table]
	From	[AppCatalog].[Table] T
			Inner Join [App_DataDictionary].[DatabaseSchema_AK] P
			On	T.[SchemaId] = P.[SchemaId]
			Left Join @Values S
			On	T.[TableId] = S.[TableId]
	Where	S.[TableId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[AppCatalog].[Catalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseTable]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[TableId],
				[SchemaId],
				[TableName],
				[TableType]
		From	@Values
		Except
		Select	[TableId],
				[SchemaId],
				[TableName],
				[TableType]
		From	[AppCatalog].[Table])
	Update [AppCatalog].[Table]
	Set		[SchemaId] = S.[SchemaId],
			[TableName] = S.[TableName],
			[TableType] = S.[TableType]
	From	[AppCatalog].[Table] T
			Inner Join [Delta] S
			On	T.[TableId] = S.[TableId]
	Print FormatMessage ('Update [App_DataDictionary].[DatabaseTable]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppCatalog].[Table] (
		[TableId],
		[SchemaId],
		[TableName],
		[TableType])
	Select	S.[TableId],
			S.[SchemaId],
			S.[TableName],
			S.[TableType]
	From	@Values S
			Left Join [AppCatalog].[Table] T
			On	S.[TableId] = T.[TableId]
	Where	T.[TableId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DatabaseTable]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
	Print FormatMessage (' @ModelId- %s',Convert(NVarChar(50),@ModelId))

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

