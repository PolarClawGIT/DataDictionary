CREATE PROCEDURE [AppCatalog].[procSetConstraint]
		@CatalogId UniqueIdentifier = Null,
		@ConstraintId UniqueIdentifier = Null,
		@Data [AppCatalog].[typeConstraint] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DatabaseConstraint.
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
		[ConstraintId]		UniqueIdentifier Not Null,
		[SchemaId]			UniqueIdentifier Not Null,
		[TableId]			UniqueIdentifier Not Null,
		[ConstraintName]	SysName Not Null,
		[ConstraintType]	[App_DataDictionary].[typeObjectType] Null,
		Primary Key ([ConstraintId]),
		Unique  ([SchemaId], [ConstraintName]))

	Insert Into @Values
	Select	Coalesce(D.[ConstraintId], H.[ConstraintId], NewId()) As [ConstraintId],
			T.[SchemaId],
			T.[TableId],
			NullIf(Trim(D.[ConstraintName]),'') As [ConstraintName],
			NullIf(Trim(D.[ConstraintType]),'') As [ConstraintType]
	From	@Data D
			Left Join [AppCatalog].[ConstraintHs] H
			On	Coalesce(D.[CatalogId], @CatalogId) = H.[CatalogId] And
				(D.[ConstraintId] = H.[ConstraintId] Or
				 (D.[SchemaName] = H.[SchemaName] And
				  D.[ConstraintName] = H.[ConstraintName]))
			Left Join [AppCatalog].[TableHs] T
			On	Coalesce(D.[CatalogId], @CatalogId) = T.[CatalogId] And
				D.[SchemaName] = T.[SchemaName] And
				D.[TableName] = T.[TableName]
	Where	(@CatalogId is Null Or @CatalogId = Coalesce(D.[CatalogId], H.[CatalogId])) And
			(@ConstraintId is Null Or @ConstraintId = Coalesce(D.[ConstraintId], H.[ConstraintId]))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Apply Changes
	Delete From [AppCatalog].[ConstraintColumn]
	From	[AppCatalog].[ConstraintColumn] T
			Inner Join [AppCatalog].[ConstraintColumnHS] H
			On	T.[ConstraintColumnId] = H.[ConstraintColumnId]
			Left Join @Values S
			On	H.[ConstraintId] = S.[ConstraintId]
	Where	S.[ConstraintId] is Null And
			(@ConstraintId is Not Null Or @CatalogId is Not Null) And
			(@ConstraintId is Null Or @ConstraintId = H.[ConstraintId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Print FormatMessage ('Delete [AppCatalog].[ConstraintColumn] (Constraint): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[Constraint]
	From	[AppCatalog].[Constraint] T
			Inner Join [AppCatalog].[ConstraintHs] H
			On	T.[ConstraintId] = H.[ConstraintId]
			Left Join @Values S
			On	H.[ConstraintId] = S.[ConstraintId]
	Where	S.[ConstraintId] is Null And
			(@ConstraintId is Not Null Or @CatalogId is Not Null) And
			(@ConstraintId is Null Or @ConstraintId = H.[ConstraintId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Print FormatMessage ('Delete [AppCatalog].[Constraint]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[ConstraintId],
				[SchemaId],
				[TableId],
				[ConstraintName],
				[ConstraintType]
		From	@Values
		Except
		Select	[ConstraintId],
				[SchemaId],
				[TableId],
				[ConstraintName],
				[ConstraintType]
		From	[AppCatalog].[Constraint])
	Update	[AppCatalog].[Constraint]
	Set		[SchemaId] = S.[SchemaId],
			[TableId] = S.[TableId],
			[ConstraintName] = S.[ConstraintName],
			[ConstraintType] = S.[ConstraintType]
	From	[AppCatalog].[Constraint] T
			Inner Join [Delta] S
			On	T.[ConstraintId] = S.[ConstraintId]
	Print FormatMessage ('Update [AppCatalog].[Constraint]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppCatalog].[Constraint] (
			[ConstraintId],	
			[SchemaId],
			[TableId],
			[ConstraintName],
			[ConstraintType])
	Select	S.[ConstraintId],	
			S.[SchemaId],
			S.[TableId],
			S.[ConstraintName],
			S.[ConstraintType]
	From	@Values S
			Left Join [AppCatalog].[Constraint] T
			On	S.[ConstraintId] = T.[ConstraintId]
	Where	T.[ConstraintId] is Null
	Print FormatMessage ('Insert [AppCatalog].[Constraint]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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