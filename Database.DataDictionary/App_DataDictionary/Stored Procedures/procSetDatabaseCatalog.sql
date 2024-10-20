﻿CREATE PROCEDURE [App_DataDictionary].[procSetDatabaseCatalog]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeDatabaseCatalog] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DatabaseCatalog.
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

	-- Clean the Data, helps performance
	Declare @Values Table ( -- Needs to match the target data structure
		[CatalogId] UniqueIdentifier Not Null,
		[CatalogTitle] [App_DataDictionary].[typeTitle] Not Null,
		[CatalogDescription] [App_DataDictionary].[typeDescription] Null,
		[SourceServerName] SysName Not Null,
		[SourceDatabaseName] SysName Not Null,
		[SourceDate] DateTime Not Null,
		Primary Key ([CatalogId]))
	
	Insert Into @Values
	Select	X.[CatalogId],
			NullIf(Trim(IsNull(D.[CatalogTitle], D.[SourceDatabaseName])),'') As [CatalogTitle],
			NullIf(Trim(D.[CatalogDescription]), '') As [CatalogDescription],
			NullIf(Trim(D.[SourceServerName]), '') As [SourceServerName],
			NullIf(Trim(D.[SourceDatabaseName]), '') As [SourceDatabaseName],
			IsNull(D.[SourceDate],GetDate()) As [SourceDate]
	From	@Data D
			Left Join [App_DataDictionary].[DatabaseCatalog_AK] A
			On	D.[SourceDatabaseName] = A.[DatabaseName]
			Cross apply (
				Select	Coalesce(A.[CatalogId], D.[CatalogId], @CatalogId, NewId()) As [CatalogId]) X
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
			On	P.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseConstraintColumn] (Catalog): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseConstraint]
	From	[App_DataDictionary].[DatabaseConstraint] T
			Inner Join [App_DataDictionary].[DatabaseSchema_AK] P
			On	T.[SchemaId] = P.[SchemaId]
			Left Join @Values S
			On	P.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseConstraint] (Catalog): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseDomain]
	From	[App_DataDictionary].[DatabaseDomain] T
			Inner Join [App_DataDictionary].[DatabaseSchema_AK] P
			On	T.[SchemaId] = P.[SchemaId]
			Left Join @Values S
			On	P.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseDomain] (Catalog): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseRoutineDependency]
	From	[App_DataDictionary].[DatabaseRoutineDependency] T
			Inner Join [App_DataDictionary].[DatabaseRoutine_AK] P
			On	T.[RoutineId] = P.[RoutineId]
			Left Join @Values S
			On	P.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseRoutineDependency] (Catalog): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseRoutineParameter]
	From	[App_DataDictionary].[DatabaseRoutineParameter] T
			Inner Join [App_DataDictionary].[DatabaseRoutine_AK] P
			On	T.[RoutineId] = P.[RoutineId]
			Left Join @Values S
			On	P.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseRoutineParameter] (Catalog): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseRoutine]
	From	[App_DataDictionary].[DatabaseRoutine] T
			Inner Join [App_DataDictionary].[DatabaseSchema_AK] P
			On	T.[SchemaId] = P.[SchemaId]
			Left Join @Values S
			On	P.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseRoutine] (Catalog): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseTableColumn]
	From	[App_DataDictionary].[DatabaseTableColumn] T
			Inner Join [App_DataDictionary].[DatabaseTable_AK] P
			On	T.[TableId] = P.[TableId]
			Left Join @Values S
			On	P.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseTableColumn] (Catalog): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseTable]
	From	[App_DataDictionary].[DatabaseTable] T
			Inner Join [App_DataDictionary].[DatabaseSchema_AK] P
			On	T.[SchemaId] = P.[SchemaId]
			Left Join @Values S
			On	P.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseTable] (Catalog): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseSchema]
	From	[App_DataDictionary].[DatabaseSchema] T
			Inner Join [App_DataDictionary].[DatabaseCatalog_AK] P
			On	T.[CatalogId] = P.[CatalogId]
			Left Join @Values S
			On	P.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			T.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseSchema] (Catalog): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseExtendedProperty]
	From	[App_DataDictionary].[DatabaseExtendedProperty] T
			Inner Join [App_DataDictionary].[DatabaseCatalog_AK] P
			On	T.[CatalogId] = P.[CatalogId]
			Left Join @Values S
			On	P.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			T.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseExtendedProperty] (Catalog): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[ModelCatalog]
	From	[App_DataDictionary].[ModelCatalog] T
			Inner Join [App_DataDictionary].[DatabaseCatalog_AK] P
			On	T.[CatalogId] = P.[CatalogId]
			Left Join @Values S
			On	P.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			T.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[ModelCatalog] (Catalog): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseCatalog]
	From	[App_DataDictionary].[DatabaseCatalog] T
			Left Join @Values S
			On	T.[CatalogId] = S.[CatalogId]
	Where	S.[CatalogId] is Null And
			T.[CatalogId] In (
			Select	A.[CatalogId]
			From	[App_DataDictionary].[DatabaseCatalog] A
					Left Join [App_DataDictionary].[ModelCatalog] C
					On	A.[CatalogId] = C.[CatalogId]
			Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
					(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseCatalog]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[CatalogId],
				[CatalogTitle],
				[CatalogDescription],
				[SourceServerName],
				[SourceDatabaseName],
				[SourceDate]
		From	@Values
		Except
		Select	[CatalogId],
				[CatalogTitle],
				[CatalogDescription],
				[SourceServerName],
				[SourceDatabaseName],
				[SourceDate]
		From	[App_DataDictionary].[DatabaseCatalog])
	Update [App_DataDictionary].[DatabaseCatalog]
	Set		[CatalogTitle] = S.[CatalogTitle],
			[CatalogDescription] = S.[CatalogDescription],
			[SourceServerName] = S.[SourceServerName],
			[SourceDatabaseName] = S.[SourceDatabaseName],
			[SourceDate] = S.[SourceDate]
	From	[App_DataDictionary].[DatabaseCatalog] T
			Inner Join [Delta] S
			On	T.[CatalogId] = S.[CatalogId]
	Print FormatMessage ('Update [App_DataDictionary].[DatabaseCatalog]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DatabaseCatalog] (
			[CatalogId],
			[CatalogTitle],
			[CatalogDescription],
			[SourceServerName],
			[SourceDatabaseName],
			[SourceDate])
	Select	S.[CatalogId],
			S.[CatalogTitle],
			S.[CatalogDescription],
			S.[SourceServerName],
			S.[SourceDatabaseName],
			S.[SourceDate]
	From	@Values S
			Left Join [App_DataDictionary].[DatabaseCatalog] T
			On	S.[CatalogId] = T.[CatalogId]
	Where	T.[CatalogId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DatabaseCatalog]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
