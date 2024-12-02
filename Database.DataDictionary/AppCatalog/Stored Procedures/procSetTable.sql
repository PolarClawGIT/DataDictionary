CREATE PROCEDURE [AppCatalog].[procSetTable]
		@CatalogId UniqueIdentifier = Null,
		@TableId UniqueIdentifier = Null,
		@Data [AppCatalog].[typeTable] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DatabaseTable.
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

	-- Clean the Data, helps performance
	Declare @Values Table (
		[TableId]   UniqueIdentifier Not Null,
		[SchemaId]  UniqueIdentifier Not Null,
		[TableName] SysName Not Null,
		[TableType] [App_DataDictionary].[typeObjectType] Null,
		Primary Key ([TableId]),
		Unique ([SchemaId], [TableName]))

	Insert Into @Values
	Select	Coalesce(D.[TableId], H.[TableId], NewId()) As [TableId],
			S.[SchemaId],
			NullIf(Trim(D.[TableName]),'') As [TableName],
			NullIf(Trim(D.[TableType]),'') As [TableType]
	From	@Data D
			Left Join [AppCatalog].[TableHs] H
			On	Coalesce(D.[CatalogId], @CatalogId) = H.[CatalogId] And
				(D.[TableId] = H.[TableId] Or
				 (D.[SchemaName] = H.[SchemaName] And
				  D.[TableName] = H.[TableName]))
			Left Join [AppCatalog].[SchemaHs] S
			On	Coalesce(D.[CatalogId], @CatalogId) = S.[CatalogId] And
				D.[SchemaName] = S.[SchemaName]
	Where	(@CatalogId is Null Or @CatalogId = Coalesce(D.[CatalogId], H.[CatalogId])) And
			(@TableId is Null Or @TableId = Coalesce(D.[TableId], H.[TableId]))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppCatalog].[ConstraintColumn]
	From	[AppCatalog].[ConstraintColumn] T
			Inner Join [AppCatalog].[ConstraintColumnHS] H
			On	T.[ConstraintColumnId] = H.[ConstraintColumnId]
			Left Join @Values S
			On	H.[TableId] = S.[TableId]
	Where	S.[TableId] is Null And
			(@TableId is Not Null Or @CatalogId is Not Null) And
			(@TableId is Null Or @TableId = H.[TableId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Print FormatMessage ('Delete [AppCatalog].[ConstraintColumn] (Table): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Constraint]
	From	[AppCatalog].[Constraint] T
			Inner Join [AppCatalog].[ConstraintHS] H
			On	T.[ConstraintId] = H.[ConstraintId]
			Left Join @Values S
			On	H.[TableId] = S.[TableId]
	Where	S.[TableId] is Null And
			(@TableId is Not Null Or @CatalogId is Not Null) And
			(@TableId is Null Or @TableId = H.[TableId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Print FormatMessage ('Delete [AppCatalog].[Constraint] (Table): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[TableColumn]
	From	[AppCatalog].[TableColumn] T
			Inner Join [AppCatalog].[TableColumnHs] H
			On	T.[TableColumnId] = H.[TableColumnId]
			Left Join @Values S
			On	H.[TableId] = S.[TableId]
	Where	S.[SchemaId] is Null And
			(@TableId is Not Null Or @CatalogId is Not Null) And
			(@TableId is Null Or @TableId = H.[TableId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Print FormatMessage ('Delete [AppCatalog].[TableColumn]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Table]
	From	[AppCatalog].[Table] T
			Inner Join [AppCatalog].[TableHs] H
			On	T.[TableId] = H.[TableId]
			Left Join @Values S
			On	H.[TableId] = S.[TableId]
	Where	S.[SchemaId] is Null And
			(@TableId is Not Null Or @CatalogId is Not Null) And
			(@TableId is Null Or @TableId = H.[TableId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Print FormatMessage ('Delete [AppCatalog].[Table]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

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

