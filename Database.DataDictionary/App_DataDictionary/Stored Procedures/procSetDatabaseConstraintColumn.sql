CREATE PROCEDURE [App_DataDictionary].[procSetDatabaseConstraintColumn]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeDatabaseConstraintColumn] ReadOnly
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
	If @ModelId is Null and @CatalogId is Null
	Throw 50000, '@ModelId or @CatalogId must be specified', 1;

	-- Clean the Data
	Declare @Values Table (
		[ConstraintColumnId]  UniqueIdentifier Not Null,
		[ConstraintId]        UniqueIdentifier Not Null,
		[ColumnId]            UniqueIdentifier Not Null,
		[OrdinalPosition]     Int Null,
		[ReferencedSchemaName] SysName Null,
		[ReferencedTableName]  SysName Null,
		[ReferencedColumnName] SysName Null,
		Primary Key ([ConstraintColumnId]))

	Insert Into @Values
	Select	X.[ConstraintColumnId],
			X.[ConstraintId],
			R.[ColumnId],
			D.[OrdinalPosition],
			NullIf(Trim([ReferencedSchemaName]),'') As [ReferencedSchemaName],
			NullIf(Trim([ReferencedTableName]),'') As [ReferencedTableName],
			NullIf(Trim([ReferencedColumnName]),'') As [ReferencedColumnName]
	From	@Data D
			Inner Join [App_DataDictionary].[DatabaseConstraint_AK] P
			On	D.[DatabaseName] = P.[DatabaseName] And
				D.[SchemaName] = P.[SchemaName] And
				D.[ConstraintName] = P.[ConstraintName]
			Left Join [App_DataDictionary].[DatabaseConstraintColumn_AK] A
			On	D.[DatabaseName] = A.[DatabaseName] And
				D.[SchemaName] = A.[SchemaName] And
				D.[ConstraintName] = A.[ConstraintName] And
				D.[TableName] = A.[TableName] And
				D.[ColumnName] = A.[ColumnName]
			Inner Join [App_DataDictionary].[DatabaseTableColumn_AK] R
			On	D.[DatabaseName] = R.[DatabaseName] And
				D.[SchemaName] = R.[SchemaName] And
				D.[TableName] = R.[TableName] And
				D.[ColumnName] = R.[ColumnName]
			Cross Apply (
				Select	Coalesce(A.[ConstraintColumnId], D.[ConstraintColumnId], NewId()) As [ConstraintColumnId],
						Coalesce(A.[ConstraintId], P.[ConstraintId]) As [ConstraintId],
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
	Delete From [App_DataDictionary].[DatabaseConstraintColumn]
	From	[App_DataDictionary].[DatabaseConstraintColumn] T
			Inner Join [App_DataDictionary].[DatabaseConstraint_AK] P
			On	T.[ConstraintId] = P.[ConstraintId]
			Left Join @Values S
			On	T.[ConstraintColumnId] = S.[ConstraintColumnId]
	Where	S.[ConstraintColumnId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseConstraintColumn]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
				[ColumnId],
				[OrdinalPosition],
				[ReferencedSchemaName],
				[ReferencedTableName],
				[ReferencedColumnName]
		From	[App_DataDictionary].[DatabaseConstraintColumn])
	Update [App_DataDictionary].[DatabaseConstraintColumn]
	Set		[ConstraintId] = S.[ConstraintId],
			[ColumnId] = S.[ColumnId],
			[OrdinalPosition] = S.[OrdinalPosition],
			[ReferencedSchemaName] = S.[ReferencedSchemaName],
			[ReferencedTableName] = S.[ReferencedTableName],
			[ReferencedColumnName] = S.[ReferencedColumnName]
	From	[App_DataDictionary].[DatabaseConstraintColumn] T
			Inner Join [Delta] S
			On	T.[ConstraintColumnId] = S.[ConstraintColumnId]
	Print FormatMessage ('Update [App_DataDictionary].[DatabaseConstraintColumn]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DatabaseConstraintColumn] (
			[ConstraintColumnId],
			[ConstraintId],
			[ColumnId],
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
			Left Join [App_DataDictionary].[DatabaseConstraintColumn] T
			On	S.[ConstraintColumnId] = T.[ConstraintColumnId]
	Where	T.[ConstraintColumnId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DatabaseConstraintColumn]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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