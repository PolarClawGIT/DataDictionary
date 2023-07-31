CREATE PROCEDURE [App_DataDictionary].[procSetDatabaseTable]
		@ModelId UniqueIdentifier,
		@Data [App_DataDictionary].[typeDatabaseTable] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DatabaseTable.
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
	Declare @Values [App_DataDictionary].[typeDatabaseTable]
	Insert Into @Values
	Select	P.[CatalogId] As [CatalogId],
			P.[CatalogName] As [CatalogName],
			NullIf(Trim(D.[SchemaName]),'') As [SchemaName],
			NullIf(Trim(D.[TableName]),'') As [TableName],
			NullIf(Trim(D.[TableType]),'') As [TableType]
	From	@Data D
			Left Join [App_DataDictionary].[ApplicationCatalog] C
			On	C.[ModelId] = @ModelId
			Left Join [App_DataDictionary].[DatabaseCatalog] P
			On	C.[CatalogId] = P.[CatalogId] And
				D.[CatalogName] = P.[CatalogName]

	-- Validation
	If Not Exists (Select 1 From [App_DataDictionary].[ApplicationModel] Where [ModelId] = @ModelId)
	Throw 50000, '[ModelId] could not be found that matched the parameter', 1;

	If Exists (
		Select	[CatalogName], [SchemaName], [TableName]
		From	@Values
		Group By [CatalogName], [SchemaName], [TableName]
		Having	Count(*) > 1)
	Throw 50000, '[TableName] cannot be duplicate within a Schema', 2;

	-- Cascade Delete
	Declare @Delete Table (
		[CatalogId] UniqueIdentifier Not Null,
		[SchemaName] SysName Not Null,
		[TableName] SysName Not Null,
		Primary key ([CatalogId], [SchemaName], [TableName]));

	Insert Into @Delete
	Select	T.[CatalogId],
			T.[SchemaName],
			T.[TableName]
	From	[App_DataDictionary].[DatabaseTable] T
			Inner Join [App_DataDictionary].[ApplicationCatalog] M
			On	T.[CatalogId] = M.[CatalogId] And
				M.[ModelId] = @ModelId
			Left Join @Values V
			On	T.[CatalogId] = V.[CatalogId] And
				T.[SchemaName] = V.[SchemaName] And
				T.[TableName] = V.[TableName]

	Delete From [App_DataDictionary].[DatabaseColumn]
	From	[App_DataDictionary].[DatabaseColumn] T
			Inner Join @Delete D
			On	T.[CatalogId] = D.[CatalogId] And
				T.[SchemaName] = D.[SchemaName] And
				T.[TableName] = D.[TableName];

	-- Apply Changes
	With [Delta] As (
		Select	[CatalogId],
				[SchemaName],
				[TableName],
				[TableType]
		From	@Values
		Except
		Select	[CatalogId],
				[SchemaName],
				[TableName],
				[TableType]
		From	[App_DataDictionary].[DatabaseTable]),
	[Data] As (
		Select	V.[CatalogId],
				V.[SchemaName],
				V.[TableName],
				V.[TableType],
				IIF(D.[CatalogId] is Null,1, 0) As [IsDiffrent]
		From	@Values V
				Left Join [Delta] D
				On	V.[CatalogId] = D.[CatalogId] And
					V.[SchemaName] = D.[SchemaName] And
					V.[TableName] = D.[TableName])
	Merge [App_DataDictionary].[DatabaseTable] As T
	Using [Data] As S
	On	T.[CatalogId] = S.[CatalogId] And
		T.[SchemaName] = S.[SchemaName] And
		T.[TableName] = S.[TableName]
	When Matched and [IsDiffrent] = 1 Then Update Set
		[TableType] = S.[TableType]
	When Not Matched by Target Then
		Insert ([CatalogId], [SchemaName], [TableName], [TableType])
		Values ([CatalogId], [SchemaName], [TableName], [TableType])
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
	Print FormatMessage (' @ModelId- %s',Convert(NVarChar(50),@ModelId))

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
-- Provide System Documentation
EXEC sp_addextendedproperty @name = N'MS_Description',
	@level0type = N'SCHEMA', @level0name = N'App_DataDictionary',
    @level1type = N'PROCEDURE', @level1name = N'procSetDatabaseTable',
	@value = N'Performs Set on DatabaseTable.'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
	@level0type = N'SCHEMA', @level0name = N'App_DataDictionary',
    @level1type = N'PROCEDURE', @level1name = N'procSetDatabaseTable',
	@level2type = N'PARAMETER', @level2name = N'@ModelId',
	@value = N'ModelId'
GO