CREATE PROCEDURE [App_DataDictionary].[procSetDatabaseRoutineDependency]
		@ModelId UniqueIdentifier,
		@Data [App_DataDictionary].[typeDatabaseRoutineDependency] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DatabaseRoutineDependency.
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

	-- Clean the Data
	Declare @Values [App_DataDictionary].[typeDatabaseRoutineDependency]
	Insert Into @Values
	Select	P.[CatalogId] As [CatalogId],
			P.[CatalogName] As [CatalogName],
			NullIf(Trim(D.[SchemaName]),'') As [SchemaName],
			NullIf(Trim(D.[RoutineName]),'') As [RoutineName],
			NullIf(Trim(D.[ReferenceSchemaName]),'') As [ReferenceSchemaName],
			NullIf(Trim(D.[ReferenceObjectName]),'') As [ReferenceObjectName],
			NullIf(Trim(D.[ReferenceObjectType]),'') As [ReferenceObjectType],
			NullIf(Trim(D.[ReferenceColumnName]),'') As [ReferenceColumnName],
			D.[IsCallerDependent],
			D.[IsAmbiguous],
			D.[IsSelected],
			D.[IsUpdated],
			D.[IsSelectAll],
			D.[IsAllColumnsFound],
			D.[IsInsertAll],
			D.[IsIncomplete]
	From	@Data D
			Left Join [App_DataDictionary].[ApplicationCatalog] C
			On	C.[ModelId] = @ModelId
			Left Join [App_DataDictionary].[DatabaseCatalog] P
			On	C.[CatalogId] = P.[CatalogId] And
				D.[CatalogName] = P.[CatalogName]

	-- Validation
	If Not Exists (Select 1 From [App_DataDictionary].[ApplicationModel] Where [ModelId] = @ModelId)
	Throw 50000, '[ModelId] could not be found that matched the parameter', 1;

	-- Apply Changes
	With [Delta] As (
		Select	[CatalogId],
				[SchemaName],
				[RoutineName],
				[ReferenceSchemaName],
				[ReferenceObjectName],
				[ReferenceObjectType],
				[ReferenceColumnName],
				[IsCallerDependent],
				[IsAmbiguous],
				[IsSelected],
				[IsUpdated],
				[IsSelectAll],
				[IsAllColumnsFound],
				[IsInsertAll],
				[IsIncomplete]
		From	@Values
		Except
		Select	[CatalogId],
				[SchemaName],
				[RoutineName],
				[ReferenceSchemaName],
				[ReferenceObjectName],
				[ReferenceObjectType],
				[ReferenceColumnName],
				[IsCallerDependent],
				[IsAmbiguous],
				[IsSelected],
				[IsUpdated],
				[IsSelectAll],
				[IsAllColumnsFound],
				[IsInsertAll],
				[IsIncomplete]
		From	[App_DataDictionary].[DatabaseRoutineDependency]),
	[Data] As (
		Select	V.[CatalogId],
				V.[SchemaName],
				V.[RoutineName],
				V.[ReferenceSchemaName],
				V.[ReferenceObjectName],
				V.[ReferenceObjectType],
				V.[ReferenceColumnName],
				V.[IsCallerDependent],
				V.[IsAmbiguous],
				V.[IsSelected],
				V.[IsUpdated],
				V.[IsSelectAll],
				V.[IsAllColumnsFound],
				V.[IsInsertAll],
				V.[IsIncomplete],
				IIF(D.[CatalogId] is Null,1, 0) As [IsDiffrent]
		From	@Values V
				Left Join [Delta] D
				On	V.[CatalogId] = D.[CatalogId] And
					V.[SchemaName] = D.[SchemaName] And
					V.[RoutineName] = D.[RoutineName] And
					IsNull(V.[ReferenceSchemaName],'') = IsNull(D.[ReferenceSchemaName],'') And
					IsNull(V.[ReferenceObjectName],'') = IsNull(D.[ReferenceObjectName],'') And
					IsNull(V.[ReferenceColumnName],'') = IsNull(D.[ReferenceColumnName],''))
	Merge [App_DataDictionary].[DatabaseRoutineDependency] As T
	Using [Data] As S
	On	T.[CatalogId] = S.[CatalogId] And
		T.[SchemaName] = S.[SchemaName] And
		T.[RoutineName] = S.[RoutineName] And
		IsNull(T.[ReferenceSchemaName],'') = IsNull(S.[ReferenceSchemaName],'') And
		IsNull(T.[ReferenceObjectName],'') = IsNull(S.[ReferenceObjectName],'') And
		IsNull(T.[ReferenceColumnName],'') = IsNull(S.[ReferenceColumnName],'')
	When Matched and [IsDiffrent] = 1 Then Update Set
		[CatalogId] = S.[CatalogId],
		[SchemaName] = S.[SchemaName],
		[RoutineName] = S.[RoutineName],
		[ReferenceSchemaName] = S.[ReferenceSchemaName],
		[ReferenceObjectName] = S.[ReferenceObjectName],
		[ReferenceObjectType] = S.[ReferenceObjectType],
		[ReferenceColumnName] = S.[ReferenceColumnName],
		[IsCallerDependent] = S.[IsCallerDependent],
		[IsAmbiguous] = S.[IsAmbiguous],
		[IsSelected] = S.[IsSelected],
		[IsUpdated] = S.[IsUpdated],
		[IsSelectAll] = S.[IsSelectAll],
		[IsAllColumnsFound] = S.[IsAllColumnsFound],
		[IsInsertAll] = S.[IsInsertAll],
		[IsIncomplete] = S.[IsIncomplete]
	When Not Matched by Target Then
		Insert ([CatalogId],
				[SchemaName],
				[RoutineName],
				[ReferenceSchemaName],
				[ReferenceObjectName],
				[ReferenceObjectType],
				[ReferenceColumnName],
				[IsCallerDependent],
				[IsAmbiguous],
				[IsSelected],
				[IsUpdated],
				[IsSelectAll],
				[IsAllColumnsFound],
				[IsInsertAll],
				[IsIncomplete])
		Values ([CatalogId],
				[SchemaName],
				[RoutineName],
				[ReferenceSchemaName],
				[ReferenceObjectName],
				[ReferenceObjectType],
				[ReferenceColumnName],
				[IsCallerDependent],
				[IsAmbiguous],
				[IsSelected],
				[IsUpdated],
				[IsSelectAll],
				[IsAllColumnsFound],
				[IsInsertAll],
				[IsIncomplete])
	When Not Matched by Source And (T.[CatalogId] In (
		Select	[CatalogId]
		From	[App_DataDictionary].[ApplicationCatalog]
		Where	[ModelId] = @ModelId))
		Then Delete;

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
