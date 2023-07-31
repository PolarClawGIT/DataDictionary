CREATE PROCEDURE [App_DataDictionary].[procSetDatabaseSchema]
		@ModelId UniqueIdentifier,
		@Data [App_DataDictionary].[typeDatabaseSchema] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DatabaseSchema.
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
	Declare @Values [App_DataDictionary].[typeDatabaseSchema]
	Insert Into @Values
	Select	P.[CatalogId],
			P.[CatalogName],
			NullIf(Trim(D.[SchemaName]),'') As [SchemaName]
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
		Select	[CatalogName], [SchemaName]
		From	@Values
		Group By [CatalogName], [SchemaName]
		Having	Count(*) > 1)
	Throw 50000, '[SchemaName] cannot be duplicate within a Catalog', 2;

	-- Cascade Delete
	Declare @Delete Table (
		[CatalogId] UniqueIdentifier Not Null,
		[SchemaName] SysName Not Null,
		Primary key ([CatalogId], [SchemaName]));

	Insert Into @Delete
	Select	T.[CatalogId],
			T.[SchemaName]
	From	[App_DataDictionary].[DatabaseSchema] T
			Inner Join [App_DataDictionary].[ApplicationCatalog] M
			On	T.[CatalogId] = M.[CatalogId] And
				M.[ModelId] = @ModelId
			Left Join @Values V
			On	T.[CatalogId] = V.[CatalogId] And
				T.[SchemaName] = V.[SchemaName];

	Delete From [App_DataDictionary].[DatabaseTableColumn]
	From	[App_DataDictionary].[DatabaseTableColumn] T
			Inner Join @Delete D
			On	T.[CatalogId] = D.[CatalogId] And
				T.[SchemaName] = D.[SchemaName];

	Delete From [App_DataDictionary].[DatabaseTable]
	From	[App_DataDictionary].[DatabaseTable] T
			Inner Join @Delete D
			On	T.[CatalogId] = D.[CatalogId] And
				T.[SchemaName] = D.[SchemaName];
				
	-- Apply Changes
	With [Delta] As (
		Select	[CatalogId],
				[SchemaName]
		From	@Values
		Except
		Select	[CatalogId],
				[SchemaName]
		From	[App_DataDictionary].[DatabaseSchema]),
	[Data] As (
		Select	V.[CatalogId],
				V.[SchemaName],
				IIF(D.[CatalogId] is Null,1, 0) As [IsDiffrent]
		From	@Values V
				Left Join [Delta] D
				On	V.[CatalogId] = D.[CatalogId] And
					V.[SchemaName] = D.[SchemaName])
	Merge [App_DataDictionary].[DatabaseSchema] As T
	Using [Data] As S
	On	T.[CatalogId] = S.[CatalogId] And
		T.[SchemaName] = S.[SchemaName]
	-- When Matched and [IsDiffrent] = 1 Then Update Set -- Currently not needed
	When Not Matched by Target Then
		Insert ([CatalogId], [SchemaName])
		Values ([CatalogId], [SchemaName])
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
    @level1type = N'PROCEDURE', @level1name = N'procSetDatabaseSchema',
	@value = N'Performs Set on DatabaseSchema.'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
	@level0type = N'SCHEMA', @level0name = N'App_DataDictionary',
    @level1type = N'PROCEDURE', @level1name = N'procSetDatabaseSchema',
	@level2type = N'PARAMETER', @level2name = N'@ModelId',
	@value = N'ModelId'
GO