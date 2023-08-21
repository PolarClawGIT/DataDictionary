﻿CREATE PROCEDURE [App_DataDictionary].[procSetDatabaseRoutine]
		@ModelId UniqueIdentifier,
		@Data [App_DataDictionary].[typeDatabaseRoutine] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DatabaseRoutine.
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
	Declare @Values [App_DataDictionary].[typeDatabaseRoutine]
	Insert Into @Values
	Select	P.[CatalogId] As [CatalogId],
			P.[CatalogName] As [CatalogName],
			NullIf(Trim(D.[SchemaName]),'') As [SchemaName],
			NullIf(Trim(D.[RoutineName]),'') As [RoutineName],
			NullIf(Trim(D.[RoutineType]),'') As [RoutineType]
	From	@Data D
			Left Join [App_DataDictionary].[ModelCatalog] C
			On	C.[ModelId] = @ModelId
			Left Join [App_DataDictionary].[DatabaseCatalog] P
			On	C.[CatalogId] = P.[CatalogId] And
				D.[CatalogName] = P.[CatalogName]

	-- Validation
	If Not Exists (Select 1 From [App_DataDictionary].[Model] Where [ModelId] = @ModelId)
	Throw 50000, '[ModelId] could not be found that matched the parameter', 1;

	If Exists (
		Select	[CatalogName], [SchemaName], [RoutineName]
		From	@Values
		Group By [CatalogName], [SchemaName], [RoutineName]
		Having	Count(*) > 1)
	Throw 50000, '[RoutineName] cannot be duplicate', 2;

	-- Cascade Delete
	Declare @Delete [App_DataDictionary].[typeDatabaseCatalogObject] 

	Insert Into @Delete ([CatalogId], [SchemaName], [ObjectName])
	Select	T.[CatalogId],
			T.[SchemaName],
			T.[RoutineName]
	From	[App_DataDictionary].[DatabaseRoutine] T
			Inner Join [App_DataDictionary].[ModelCatalog] M
			On	T.[CatalogId] = M.[CatalogId] And
				M.[ModelId] = @ModelId
			Left Join @Values V
			On	T.[CatalogId] = V.[CatalogId] And
				T.[SchemaName] = V.[SchemaName] And
				T.[RoutineName] = V.[RoutineName]
	Where	V.[CatalogId] is Null;

	if Exists (Select 1 From @Delete)
	Exec [App_DataDictionary].[procDeleteDatabaseCatalogObject] @ModelId, @Delete;

	-- Apply Changes
	With [Delta] As (
		Select	[CatalogId],
				[SchemaName],
				[RoutineName],
				[RoutineType]
		From	@Values
		Except
		Select	[CatalogId],
				[SchemaName],
				[RoutineName],
				[RoutineType]
		From	[App_DataDictionary].[DatabaseRoutine]),
	[Data] As (
		Select	V.[CatalogId],
				V.[SchemaName],
				V.[RoutineName],
				V.[RoutineType],
				IIF(D.[CatalogId] is Null,1, 0) As [IsDiffrent]
		From	@Values V
				Left Join [Delta] D
				On	V.[CatalogId] = D.[CatalogId] And
					V.[SchemaName] = D.[SchemaName] And
					V.[RoutineName] = D.[RoutineName])
	Merge [App_DataDictionary].[DatabaseRoutine] As T
	Using [Data] As S
	On	T.[CatalogId] = S.[CatalogId] And
		T.[SchemaName] = S.[SchemaName] And
		T.[RoutineName] = S.[RoutineName]
	When Matched and [IsDiffrent] = 1 Then Update Set
		[CatalogId] = S.[CatalogId],
		[SchemaName] = S.[SchemaName],
		[RoutineName] = S.[RoutineName],
		[RoutineType] = S.[RoutineType]
	When Not Matched by Target Then
		Insert ([CatalogId], [SchemaName], [RoutineName], [RoutineType])
		Values ([CatalogId], [SchemaName], [RoutineName], [RoutineType]);

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