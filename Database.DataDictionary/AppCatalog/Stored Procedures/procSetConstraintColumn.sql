CREATE PROCEDURE [AppCatalog].[procSetConstraintColumn]
		@CatalogId UniqueIdentifier = Null,
		@ConstraintId UniqueIdentifier = Null,
		@Data [AppCatalog].[typeConstraintColumn] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DatabaseConstraintColumn.
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

	-- Clean the Data
	Declare @Values Table (
		[ConstraintColumnId]  UniqueIdentifier Not Null,
		[ConstraintId]        UniqueIdentifier Not Null,
		[ColumnId]            UniqueIdentifier Not Null,
		[OrdinalPosition]     Int Null,
		[ReferencedSchemaName] SysName Null,
		[ReferencedTableName]  SysName Null,
		[ReferencedColumnName] SysName Null,
		Primary Key ([ConstraintColumnId]),
		Unique ([ConstraintId], [ColumnId]))

	Insert Into @Values
	Select	Coalesce(D.[ConstraintColumnId], H.[ConstraintColumnId], NewId()) As [ConstraintColumnId],
			C.[ConstraintId],
			T.[TableColumnId],
			D.[OrdinalPosition],
			NullIf(Trim(D.[ReferencedSchemaName]),'') As [ReferencedSchemaName],
			NullIf(Trim(D.[ReferencedTableName]),'') As [ReferencedTableName],
			NullIf(Trim(D.[ReferencedColumnName]),'') As [ReferencedColumnName]
	From	@Data D
			Left Join [AppCatalog].[ConstraintColumnHs] H
			On	Coalesce(D.[CatalogId], @CatalogId) = H.[CatalogId] And
				(D.[ConstraintColumnId] = H.[ConstraintColumnId] Or
				 (D.[SchemaName] = H.[SchemaName] And
				  D.[ConstraintName] = H.[ConstraintName] And
				  D.[ColumnName] = H.[ColumnName]))
			Left Join [AppCatalog].[ConstraintHs] C
			On	Coalesce(D.[CatalogId], @CatalogId) = H.[CatalogId] And
				D.[SchemaName] = C.[SchemaName] And
				D.[ConstraintName] = C.[ConstraintName]
			Left Join [AppCatalog].[TableColumnHs] T
			On	Coalesce(D.[CatalogId], @CatalogId) = T.[CatalogId] And
				D.[SchemaName] = T.[SchemaName] And
				D.[TableName] = T.[TableName] And
				D.[ColumnName] = T.[ColumnName]
	Where	(@CatalogId is Null Or @CatalogId = Coalesce(D.[CatalogId], H.[CatalogId])) And
			(@ConstraintId is Null Or @ConstraintId = C.[ConstraintId])
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog]

	-- Apply Changes
	Delete From [AppCatalog].[ConstraintColumn]
	From	[AppCatalog].[ConstraintColumn] T
			Inner Join [AppCatalog].[ConstraintColumnHs] H
			On	T.[ConstraintColumnId] = H.[ConstraintColumnId]
			Left Join @Values S
			On	H.[ConstraintColumnId] = S.[ConstraintColumnId]
	Where	S.[ConstraintColumnId] is Null And
			(@ConstraintId is Not Null Or @CatalogId is Not Null) And
			(@ConstraintId is Null Or @ConstraintId = H.[ConstraintId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Print FormatMessage ('Delete [AppCatalog].[ConstraintColumn]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[ConstraintColumnId],
				[ConstraintId],
				[ColumnId],
				[OrdinalPosition],
				[ReferencedSchemaName],
				[ReferencedTableName],
				[ReferencedColumnName]
		From	@Values
		Except
		Select	[ConstraintColumnId],
				[ConstraintId],
				[TableColumnId],
				[OrdinalPosition],
				[ReferencedSchemaName],
				[ReferencedTableName],
				[ReferencedColumnName]
		From	[AppCatalog].[ConstraintColumn])
	Update [AppCatalog].[ConstraintColumn]
	Set		[ConstraintId] = S.[ConstraintId],
			[TableColumnId] = S.[ColumnId],
			[OrdinalPosition] = S.[OrdinalPosition],
			[ReferencedSchemaName] = S.[ReferencedSchemaName],
			[ReferencedTableName] = S.[ReferencedTableName],
			[ReferencedColumnName] = S.[ReferencedColumnName]
	From	[AppCatalog].[ConstraintColumn] T
			Inner Join [Delta] S
			On	T.[ConstraintColumnId] = S.[ConstraintColumnId]
	Print FormatMessage ('Update [AppCatalog].[ConstraintColumn]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppCatalog].[ConstraintColumn] (
			[ConstraintColumnId],
			[ConstraintId],
			[TableColumnId],
			[OrdinalPosition],
			[ReferencedSchemaName],
			[ReferencedTableName],
			[ReferencedColumnName])
	Select	S.[ConstraintColumnId],
			S.[ConstraintId],
			S.[ColumnId],
			S.[OrdinalPosition],
			S.[ReferencedSchemaName],
			S.[ReferencedTableName],
			S.[ReferencedColumnName]
	From	@Values S
			Left Join [AppCatalog].[ConstraintColumn] T
			On	S.[ConstraintColumnId] = T.[ConstraintColumnId]
	Where	T.[ConstraintColumnId] is Null
	Print FormatMessage ('Insert [AppCatalog].[ConstraintColumn]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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