CREATE PROCEDURE [App_DataDictionary].[procDeleteDatabaseCatalogObject]
		@ModelId UniqueIdentifier,
		@Data [App_DataDictionary].[typeDatabaseCatalogObject] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Cascaded Delete on DatabaseCatalog Objects.
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

	Delete From [App_DataDictionary].[DatabaseConstraintColumn]
	From	@Data S
			Inner Join [App_DataDictionary].[ModelCatalog] C
			On	S.[CatalogId] = C.[CatalogId] And
				C.[ModelId] = @ModelId
			Inner Join [App_DataDictionary].[DatabaseConstraintColumn] T
			On	S.[CatalogId] = T.[CatalogId] And
				(IsNull(S.[SchemaName],T.[SchemaName]) = T.[SchemaName] And
				 (IsNull(S.[ObjectName],T.[ConstraintName]) = T.[ConstraintName] Or
				  IsNull(S.[ObjectName],T.[TableName]) = T.[TableName])) Or
				(IsNull(S.[SchemaName],T.[ReferenceSchemaName]) = T.[ReferenceSchemaName] And
				 IsNull(S.[ObjectName],T.[ReferenceTableName]) = T.[ReferenceTableName])
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseConstraintColumn]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseConstraint]
	From	@Data S
			Inner Join [App_DataDictionary].[ModelCatalog] C
			On	S.[CatalogId] = C.[CatalogId] And
				C.[ModelId] = @ModelId
			Inner Join [App_DataDictionary].[DatabaseConstraint] T
			On	S.[CatalogId] = T.[CatalogId] And
				IsNull(S.[SchemaName],T.[SchemaName]) = T.[SchemaName] And
				(IsNull(S.[ObjectName],T.[ConstraintName]) = T.[ConstraintName] Or
				 IsNull(S.[ObjectName],T.[TableName]) = T.[TableName])
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseConstraint]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseDomain]
	From	@Data S
			Inner Join [App_DataDictionary].[ModelCatalog] C
			On	S.[CatalogId] = C.[CatalogId] And
				C.[ModelId] = @ModelId
			Inner Join [App_DataDictionary].[DatabaseDomain] T
			On	S.[CatalogId] = T.[CatalogId] And
				IsNull(S.[SchemaName],T.[SchemaName]) = T.[SchemaName] And
				IsNull(S.[ObjectName],T.[DomainName]) = T.[DomainName]
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseDomain]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseRoutineDependency]
	From	@Data S
			Inner Join [App_DataDictionary].[ModelCatalog] C
			On	S.[CatalogId] = C.[CatalogId] And
				C.[ModelId] = @ModelId
			Inner Join [App_DataDictionary].[DatabaseRoutineDependency] T
			On	S.[CatalogId] = T.[CatalogId] And
				IsNull(S.[SchemaName],T.[SchemaName]) = T.[SchemaName] And
				IsNull(S.[ObjectName],T.[RoutineName]) = T.[RoutineName]
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseRoutineDependency]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseRoutineParameter]
	From	@Data S
			Inner Join [App_DataDictionary].[ModelCatalog] C
			On	S.[CatalogId] = C.[CatalogId] And
				C.[ModelId] = @ModelId
			Inner Join [App_DataDictionary].[DatabaseRoutineParameter] T
			On	S.[CatalogId] = T.[CatalogId] And
				IsNull(S.[SchemaName],T.[SchemaName]) = T.[SchemaName] And
				IsNull(S.[ObjectName],T.[RoutineName]) = T.[RoutineName]
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseRoutineParameter]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseRoutine]
	From	@Data S
			Inner Join [App_DataDictionary].[ModelCatalog] C
			On	S.[CatalogId] = C.[CatalogId] And
				C.[ModelId] = @ModelId
			Inner Join [App_DataDictionary].[DatabaseRoutine] T
			On	S.[CatalogId] = T.[CatalogId] And
				IsNull(S.[SchemaName],T.[SchemaName]) = T.[SchemaName] And
				IsNull(S.[ObjectName],T.[RoutineName]) = T.[RoutineName]
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseRoutine]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseTableColumn]
	From	@Data S
			Inner Join [App_DataDictionary].[ModelCatalog] C
			On	S.[CatalogId] = C.[CatalogId] And
				C.[ModelId] = @ModelId
			Inner Join [App_DataDictionary].[DatabaseTableColumn] T
			On	S.[CatalogId] = T.[CatalogId] And
				IsNull(S.[SchemaName],T.[SchemaName]) = T.[SchemaName] And
				IsNull(S.[ObjectName],T.[TableName]) = T.[TableName]
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseTableColumn]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseTable]
	From	@Data S
			Inner Join [App_DataDictionary].[ModelCatalog] C
			On	S.[CatalogId] = C.[CatalogId] And
				C.[ModelId] = @ModelId
			Inner Join [App_DataDictionary].[DatabaseTable] T
			On	S.[CatalogId] = T.[CatalogId] And
				IsNull(S.[SchemaName],T.[SchemaName]) = T.[SchemaName] And
				IsNull(S.[ObjectName],T.[TableName]) = T.[TableName]
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseTable]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseExtendedProperty]
	From	@Data S
			Inner Join [App_DataDictionary].[ModelCatalog] C
			On	S.[CatalogId] = C.[CatalogId] And
				C.[ModelId] = @ModelId
			Inner Join [App_DataDictionary].[DatabaseExtendedProperty] T
			On	S.[CatalogId] = T.[CatalogId] And
				Coalesce(S.[SchemaName],T.[Level0Name],'') = Coalesce(T.[Level0Name],'') And
				Coalesce(S.[ObjectName],T.[Level1Type],'') = Coalesce(T.[Level1Type],'') And
				Coalesce(S.[ObjectName],T.[Level2Name],'') = Coalesce(T.[Level2Name],'')
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseExtendedProperty]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DatabaseSchema]
	From	@Data S
			Inner Join [App_DataDictionary].[ModelCatalog] C
			On	S.[CatalogId] = C.[CatalogId] And
				C.[ModelId] = @ModelId
			Inner Join [App_DataDictionary].[DatabaseSchema] T
			On	S.[CatalogId] = T.[CatalogId] And
				IsNull(S.[SchemaName],T.[SchemaName]) = T.[SchemaName] And
				S.[ObjectName] is Null
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseSchema]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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