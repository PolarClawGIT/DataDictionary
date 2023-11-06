﻿CREATE PROCEDURE [App_DataDictionary].[procSetDatabaseConstraintColumn]
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

	IF Exists (
		Select	1
		From	@Data D
				Left Join [App_DataDictionary].[DatabaseConstraint_AK] P
				On	Coalesce(D.[CatalogId], @CatalogId) = P.[CatalogId] And
					NullIf(Trim(D.[DatabaseName]),'') = P.[DatabaseName] And
					NullIf(Trim(D.[SchemaName]),'') = P.[SchemaName] And
					NullIf(Trim(D.[ConstraintName]),'') = P.[ConstraintName]
		Where	P.[CatalogId] is Null)
	Throw 50000, '[DatabaseName], [SchemaName] or [ConstraintName] does not match existing data', 2;

	IF Exists (
		Select	1
		From	@Data D
				Left Join [App_DataDictionary].[DatabaseTableColumn_AK] R
				On	NullIf(Trim(D.[DatabaseName]),'') = R.[DatabaseName] And
					NullIf(Trim(D.[SchemaName]),'') = R.[SchemaName] And
					NullIf(Trim(D.[TableName]),'') = R.[TableName] And
					NullIf(Trim(D.[ColumnName]),'') = R.[ColumnName]
		Where	R.[CatalogId] is Null)
	Throw 50000, '[DatabaseName], [SchemaName], [TableName] or [ColumnName] does not match existing data', 3;

	-- Clean the Data
	Declare @Values Table (
		[ConstraintColumnId]  UniqueIdentifier Not Null,
		[ConstraintId]        UniqueIdentifier Not Null,
		[ParentColumnId]      UniqueIdentifier Not Null,
		[OrdinalPosition]     Int Null,
		[ReferenceSchemaName] SysName Null,
		[ReferenceTableName]  SysName Null,
		[ReferenceColumnName] SysName Null,
		Primary Key ([ConstraintColumnId]))

	Insert Into @Values
	Select	Coalesce(A.[ConstraintColumnId], NewId()) As[ConstraintColumnId],
			P.[ConstraintId],
			R.[ColumnId] As [ParentColumnId],
			D.[OrdinalPosition],
			NullIf(Trim([ReferenceSchemaName]),'') As [ReferenceSchemaName],
			NullIf(Trim([ReferenceTableName]),'') As [ReferenceTableName],
			NullIf(Trim([ReferenceColumnName]),'') As [ReferenceColumnName]
	From	@Data D
			Left Join [App_DataDictionary].[DatabaseConstraint_AK] P
			On	Coalesce(D.[CatalogId], @CatalogId) = P.[CatalogId] And
				NullIf(Trim(D.[DatabaseName]),'') = P.[DatabaseName] And
				NullIf(Trim(D.[SchemaName]),'') = P.[SchemaName] And
				NullIf(Trim(D.[ConstraintName]),'') = P.[ConstraintName]
			Left Join [App_DataDictionary].[DatabaseConstraintColumn_AK] A
			On	P.[CatalogId] = A.[CatalogId] And
				P.[SchemaId] = A.[SchemaId] And
				P.[ConstraintId] = A.[ConstraintId] And
				NullIf(Trim(D.[ColumnName]),'') = A.[ColumnName]
			Left Join [App_DataDictionary].[DatabaseTableColumn_AK] R
			On	NullIf(Trim(D.[DatabaseName]),'') = R.[DatabaseName] And
				NullIf(Trim(D.[SchemaName]),'') = R.[SchemaName] And
				NullIf(Trim(D.[TableName]),'') = R.[TableName] And
				NullIf(Trim(D.[ColumnName]),'') = R.[ColumnName]
	Where	P.[CatalogId] is Null Or
			P.[CatalogId] In (
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
				[ParentColumnId],
				[OrdinalPosition],
				[ReferenceSchemaName],
				[ReferenceTableName],
				[ReferenceColumnName]
		From	@Values
		Except
		Select	[ConstraintColumnId],
				[ConstraintId],
				[ParentColumnId],
				[OrdinalPosition],
				[ReferenceSchemaName],
				[ReferenceTableName],
				[ReferenceColumnName]
		From	[App_DataDictionary].[DatabaseConstraintColumn])
	Update [App_DataDictionary].[DatabaseConstraintColumn]
	Set		[ConstraintId] = S.[ConstraintId],
			[ParentColumnId] = S.[ParentColumnId],
			[OrdinalPosition] = S.[OrdinalPosition],
			[ReferenceSchemaName] = S.[ReferenceSchemaName],
			[ReferenceTableName] = S.[ReferenceTableName],
			[ReferenceColumnName] = S.[ReferenceColumnName]
	From	[App_DataDictionary].[DatabaseConstraintColumn] T
			Inner Join [Delta] S
			On	T.[ConstraintColumnId] = S.[ConstraintColumnId]
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseConstraintColumn]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DatabaseConstraintColumn] (
			[ConstraintColumnId],
			[ConstraintId],
			[ParentColumnId],
			[OrdinalPosition],
			[ReferenceSchemaName],
			[ReferenceTableName],
			[ReferenceColumnName])
	Select	S.[ConstraintColumnId],
			S.[ConstraintId],
			S.[ParentColumnId],
			S.[OrdinalPosition],
			S.[ReferenceSchemaName],
			S.[ReferenceTableName],
			S.[ReferenceColumnName]
	From	@Values S
			Left Join [App_DataDictionary].[DatabaseConstraintColumn] T
			On	S.[ConstraintColumnId] = T.[ConstraintColumnId]
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseConstraintColumn]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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