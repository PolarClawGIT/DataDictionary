CREATE PROCEDURE [AppCatalog].[procSetReference]
		@CatalogId UniqueIdentifier = Null,
		@ReferenceId UniqueIdentifier = Null,
		@Data [AppCatalog].[typeReference] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DatabaseReference.
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

	-- Clean the Data, helps performance
	Declare @Values Table (
		[ReferenceId]				uniqueidentifier NOT NULL,
		[CatalogId]				    uniqueidentifier NOT NULL,
		[SchemaName]                SysName NULL,
		[ObjectName]                SysName Null,
		[ReferencedDatabaseName]	SysName NULL,
		[ReferencedSchemaName]		SysName NULL,
		[ReferencedObjectName]		SysName NULL,
		[ReferencedColumnName]		SysName NULL,
		[ReferencedType]			[App_DataDictionary].[typeObjectType] NULL,
		[IsCallerDependent]			Bit NULL,
		[IsAmbiguous]				Bit NULL,
		[IsSelected]				Bit NULL,
		[IsUpdated]					Bit NULL,
		[IsSelectAll]				Bit NULL,
		[IsAllColumnsFound]			Bit NULL,
		[IsInsertAll]				Bit NULL,
		[IsIncomplete]				Bit NULL,
		Primary Key ([ReferenceId]),
		Unique ([CatalogId], [SchemaName], [ObjectName], [ReferencedDatabaseName], [ReferencedSchemaName], [ReferencedObjectName], [ReferencedColumnName]))

	Insert Into @Values
	Select		Coalesce(D.[ReferenceId], H.[ReferenceId], NewId()) As [ReferenceId],
				Coalesce(D.[CatalogId], H.[CatalogId], @CatalogId) As [CatalogId],
				D.[SchemaName],
				D.[ObjectName],
				D.[ReferencedDatabaseName],
				D.[ReferencedSchemaName],
				D.[ReferencedObjectName],
				D.[ReferencedColumnName],
				D.[ReferencedType],
				D.[IsCallerDependent],
				D.[IsAmbiguous],
				D.[IsSelected],
				D.[IsUpdated],
				D.[IsSelectAll],
				D.[IsAllColumnsFound],
				D.[IsInsertAll],
				D.[IsIncomplete]
	From	@Data D
			Left Join [AppCatalog].[ReferenceHs] H
			On	Coalesce(D.[CatalogId], @CatalogId) = H.[CatalogId] And
				(D.[ReferenceId] = H.[ReferenceId] Or 
				 (D.[SchemaName] = H.[SchemaName] And
				  D.[ObjectName] = H.[ObjectName] And
				  IsNull(D.[ReferencedDatabaseName], '') = IsNull(H.[ReferencedDatabaseName], '') And
				  IsNull(D.[ReferencedSchemaName], '') = IsNull(H.[ReferencedSchemaName], '') And
				  IsNull(D.[ReferencedObjectName], '') = IsNull(H.[ReferencedObjectName], '') And
				  IsNull(D.[ReferencedColumnName], '') = IsNull(H.[ReferencedColumnName], '')))
	Where	(@CatalogId is Null Or @CatalogId = Coalesce(D.[CatalogId], H.[CatalogId])) And
			(@ReferenceId is Null Or @ReferenceId = Coalesce(D.[ReferenceId], H.[ReferenceId]))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
				
	-- Apply Changes
	Delete From [AppCatalog].[Reference]
	From	[AppCatalog].[Reference] T
			Inner Join [AppCatalog].[ReferenceHs] H
			On	T.[ReferenceId] = H.[ReferenceId]
			Left Join @Values S
			On	H.[ReferenceId] = S.[ReferenceId]
	Where	S.[ReferenceId] is Null And
			(@ReferenceId is Not Null Or @CatalogId is Not Null) And
			(@ReferenceId is Null Or @ReferenceId = H.[ReferenceId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Print FormatMessage ('Delete [AppCatalog].[Reference]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[ReferenceId],
				[SchemaName],
				[ObjectName],
				[ReferencedDatabaseName],
				[ReferencedSchemaName],
				[ReferencedObjectName],
				[ReferencedColumnName],
				[ReferencedType],
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
		Select	[ReferenceId],
				[SchemaName],
				[ObjectName],
				[ReferencedDatabaseName],
				[ReferencedSchemaName],
				[ReferencedObjectName],
				[ReferencedColumnName],
				[ReferencedType],
				[IsCallerDependent],
				[IsAmbiguous],
				[IsSelected],
				[IsModified],
				[IsSelectAll],
				[IsAllColumnsFound],
				[IsInsertAll],
				[IsIncomplete]
		From	[AppCatalog].[Reference])
	Update [AppCatalog].[Reference]
	Set		[SchemaName] = S.[SchemaName],
			[ObjectName] = S.[ObjectName],
			[ReferencedDatabaseName] = S.[ReferencedDatabaseName],
			[ReferencedSchemaName] = S.[ReferencedSchemaName],
			[ReferencedObjectName] = S.[ReferencedObjectName],
			[ReferencedColumnName] = S.[ReferencedColumnName],
			[ReferencedType] = S.[ReferencedType],
			[IsCallerDependent] = S.[IsCallerDependent],
			[IsAmbiguous] = S.[IsAmbiguous],
			[IsSelected] = S.[IsSelected],
			[IsModified] = S.[IsUpdated],
			[IsSelectAll] = S.[IsSelectAll],
			[IsAllColumnsFound] = S.[IsAllColumnsFound],
			[IsInsertAll] = S.[IsInsertAll],
			[IsIncomplete] = S.[IsIncomplete]
	From	[AppCatalog].[Reference] T
			Inner Join [Delta] S
			On	T.[ReferenceId] = S.[ReferenceId]
	Print FormatMessage ('Update [AppCatalog].[Reference]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppCatalog].[Reference] (
			[ReferenceId],
			[CatalogId],
			[SchemaName],
			[ObjectName],
			[ReferencedDatabaseName],
			[ReferencedSchemaName],
			[ReferencedObjectName],
			[ReferencedColumnName],
			[ReferencedType],
			[IsCallerDependent],
			[IsAmbiguous],
			[IsSelected],
			[IsModified],
			[IsSelectAll],
			[IsAllColumnsFound],
			[IsInsertAll],
			[IsIncomplete])
	Select	S.[ReferenceId],
			S.[CatalogId],
			S.[SchemaName],
			S.[ObjectName],
			S.[ReferencedDatabaseName],
			S.[ReferencedSchemaName],
			S.[ReferencedObjectName],
			S.[ReferencedColumnName],
			S.[ReferencedType],
			S.[IsCallerDependent],
			S.[IsAmbiguous],
			S.[IsSelected],
			S.[IsUpdated],
			S.[IsSelectAll],
			S.[IsAllColumnsFound],
			S.[IsInsertAll],
			S.[IsIncomplete]
	From	@Values S
			Left Join [AppCatalog].[Reference] T
			On	S.[ReferenceId] = T.[ReferenceId]
	Where	T.[ReferenceId] is Null
	Print FormatMessage ('Insert [AppCatalog].[Reference]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
