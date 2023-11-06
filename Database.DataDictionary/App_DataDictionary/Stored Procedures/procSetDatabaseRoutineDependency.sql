CREATE PROCEDURE [App_DataDictionary].[procSetDatabaseRoutineDependency]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
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

	-- Validation
	If @ModelId is Null and @CatalogId is Null
	Throw 50000, '@ModelId or @CatalogId must be specified', 1;

	IF Exists (
		Select	1
		From	@Data D
				Left Join [App_DataDictionary].[DatabaseRoutine_AK] P
				On	Coalesce(D.[CatalogId], @CatalogId) = P.[CatalogId] And
					NullIf(Trim(D.[DatabaseName]),'') = P.[DatabaseName] And
					NullIf(Trim(D.[SchemaName]),'') = P.[SchemaName] And
					NullIf(Trim(D.[RoutineName]),'') = P.[RoutineName]
		Where	P.[CatalogId] is Null)
	Throw 50000, '[DatabaseName], [SchemaName] or [RoutineName] does not match existing data', 2;

	-- Clean the Data
	Declare @Values Table (
			[DependencyId]        UniqueIdentifier Not Null,
			[RoutineId]           UniqueIdentifier Not Null,
			-- Source has Reference objects as null-able and may not be up to date.
			[ReferenceSchemaName] SysName Null,
			[ReferenceObjectName] SysName Null,
			[ReferenceObjectType] NVarChar(60) Null,
			[ReferenceColumnName] SysName Null,
			[IsCallerDependent]   Bit Null,
			[IsAmbiguous]         Bit Null,
			[IsSelected]          Bit Null,
			[IsUpdated]           Bit Null,
			[IsSelectAll]         Bit Null,
			[IsAllColumnsFound]   Bit Null,
			[IsInsertAll]         BIT Null,
			[IsIncomplete]        BIT NULL,
			Primary Key ([DependencyId]))

	Insert Into @Values
	Select	Coalesce(A.[DependencyId], NewId()) As [DependencyId],
			P.[RoutineId],       
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
			Left Join [App_DataDictionary].[DatabaseRoutine_AK] P
			On	Coalesce(D.[CatalogId], @CatalogId) = P.[CatalogId] And
				NullIf(Trim(D.[DatabaseName]),'') = P.[DatabaseName] And
				NullIf(Trim(D.[SchemaName]),'') = P.[SchemaName] And
				NullIf(Trim(D.[RoutineName]),'') = P.[RoutineName]
			Left Join [App_DataDictionary].[DatabaseRoutineDependency] A
			On	P.[RoutineId] = A.[RoutineId] and
				IsNull(Trim(D.[ReferenceSchemaName]),'') = IsNull(A.[ReferenceSchemaName],'') And
				IsNull(Trim(D.[ReferenceObjectName]),'') = IsNull(A.[ReferenceObjectName],'') And
				IsNull(Trim(D.[ReferenceColumnName]),'') = IsNull(A.[ReferenceColumnName],'')
	Where	P.[CatalogId] is Null Or
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))


	-- Apply Changes
	Delete From [App_DataDictionary].[DatabaseRoutineDependency]
	From	[App_DataDictionary].[DatabaseRoutineDependency] T
			Inner Join [App_DataDictionary].[DatabaseRoutine_AK] P
			On	T.[RoutineId] = P.[RoutineId]
			Left Join @Values S
			On	T.[DependencyId] = S.[DependencyId]
	Where	S.[DependencyId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseRoutineDependency]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[DependencyId],
				[RoutineId],
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
		Select	[DependencyId],
				[RoutineId],
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
		From	[App_DataDictionary].[DatabaseRoutineDependency])
	Update [App_DataDictionary].[DatabaseRoutineDependency]
	Set		[RoutineId] = S.[RoutineId],
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
	From	[App_DataDictionary].[DatabaseRoutineDependency] T
			Inner Join [Delta] S
			On	T.[DependencyId] = S.[DependencyId]
	Print FormatMessage ('Update [App_DataDictionary].[DatabaseRoutineDependency]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DatabaseRoutineDependency] (
			[DependencyId],
			[RoutineId],
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
	Select	S.[DependencyId],
			S.[RoutineId],
			S.[ReferenceSchemaName],
			S.[ReferenceObjectName],
			S.[ReferenceObjectType],
			S.[ReferenceColumnName],
			S.[IsCallerDependent],
			S.[IsAmbiguous],
			S.[IsSelected],
			S.[IsUpdated],
			S.[IsSelectAll],
			S.[IsAllColumnsFound],
			S.[IsInsertAll],
			S.[IsIncomplete]
	From	@Values S
			Left Join [App_DataDictionary].[DatabaseRoutineDependency] T
			On	S.[DependencyId] = T.[DependencyId]
	Where	T.[DependencyId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DatabaseRoutineDependency]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
