CREATE PROCEDURE [App_DataDictionary].[procSetDatabaseReference]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeDatabaseReference] ReadOnly
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
		[ObjectId]					uniqueidentifier NOT NULL,
		[ObjectType]				[App_DataDictionary].[typeObjectType] NOT NULL,
		[ReferencedDatabaseName]	sysname NULL,
		[ReferencedSchemaName]		sysname NULL,
		[ReferencedObjectName]		sysname NULL,
		[ReferencedColumnName]		sysname NULL,
		[ReferencedType]			[App_DataDictionary].[typeObjectType] NULL,
		[IsCallerDependent]			bit NULL,
		[IsAmbiguous]				bit NULL,
		[IsSelected]				bit NULL,
		[IsUpdated]					bit NULL,
		[IsSelectAll]				bit NULL,
		[IsAllColumnsFound]			bit NULL,
		[IsInsertAll]				bit NULL,
		[IsIncomplete]				bit NULL,
		Primary Key ([ReferenceId]))

	Insert Into @Values
	Select		X.[ReferenceId],
				O.[ObjectId] As [ObjectId],
				D.[ObjectType],
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
			Left Join [App_DataDictionary].[DatabaseObject] O
			On	IsNull(D.[CatalogId], @CatalogId) = O.[CatalogId] And
				D.[DatabaseName] = O.[DatabaseName] And
				D.[SchemaName] = O.[SchemaName] And
				D.[ObjectName] = O.[ObjectName]
			Left Join [App_DataDictionary].[DatabaseReference] R
			On	O.[ObjectId] = R.[ObjectId] And
				IsNull(D.[ReferencedDatabaseName],'') = IsNull(R.[ReferencedDatabaseName],'') And
				IsNull(D.[ReferencedSchemaName],'') = IsNull(R.[ReferencedSchemaName],'') And
				IsNull(D.[ReferencedObjectName],'') = IsNull(R.[ReferencedObjectName],'') And
				IsNull(D.[ReferencedColumnName],'') = IsNull(R.[ReferencedColumnName],'')
			Cross Apply (
				Select	Coalesce(R.[ReferenceId], D.[ReferenceId], NewId()) As [ReferenceId],
						Coalesce(O.[CatalogId], @CatalogId) As [CatalogId]) X
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
	Delete From [App_DataDictionary].[DatabaseReference]
	From	[App_DataDictionary].[DatabaseReference] T
			Inner Join [App_DataDictionary].[DatabaseObject] P
			On	T.[ObjectId] = P.[ObjectId]
			Left Join @Values S
			On	T.[ReferenceId] = S.[ReferenceId]
	Where	S.[ReferenceId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseReference]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[ReferenceId],
				[ObjectId],
				[ObjectType],
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
				[ObjectId],
				[ObjectType],
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
		From	[App_DataDictionary].[DatabaseReference])
	Update [App_DataDictionary].[DatabaseReference]
	Set		[ObjectId] = S.[ObjectId],
			[ObjectType] = S.[ObjectType],
			[ReferencedDatabaseName] = S.[ReferencedDatabaseName],
			[ReferencedSchemaName] = S.[ReferencedSchemaName],
			[ReferencedObjectName] = S.[ReferencedObjectName],
			[ReferencedColumnName] = S.[ReferencedColumnName],
			[ReferencedType] = S.[ReferencedType],
			[IsCallerDependent] = S.[IsCallerDependent],
			[IsAmbiguous] = S.[IsAmbiguous],
			[IsSelected] = S.[IsSelected],
			[IsUpdated] = S.[IsUpdated],
			[IsSelectAll] = S.[IsSelectAll],
			[IsAllColumnsFound] = S.[IsAllColumnsFound],
			[IsInsertAll] = S.[IsInsertAll],
			[IsIncomplete] = S.[IsIncomplete]
	From	[App_DataDictionary].[DatabaseReference] T
			Inner Join [Delta] S
			On	T.[ReferenceId] = S.[ReferenceId]
	Print FormatMessage ('Update [App_DataDictionary].[DatabaseReference]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DatabaseReference] (
			[ReferenceId],
			[ObjectId],
			[ObjectType],
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
			[IsIncomplete])
	Select	S.[ReferenceId],
			S.[ObjectId],
			S.[ObjectType],
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
			Left Join [App_DataDictionary].[DatabaseReference] T
			On	S.[ReferenceId] = T.[ReferenceId]
	Where	T.[ReferenceId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DatabaseReference]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
