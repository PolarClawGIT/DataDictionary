CREATE PROCEDURE [App_DataDictionary].[procSetDatabaseCatalog]
		@ModelId UniqueIdentifier,
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

	-- Clean the Data
	Declare @Values [App_DataDictionary].[typeDatabaseCatalog]
	Insert Into @Values
	Select	Coalesce(P.[CatalogId], D.[CatalogId], NewId()) As [CatalogId],
			NullIf(Trim(D.[CatalogName]),'') As [CatalogName],
			NullIf(Trim(D.[SourceServerName]),'') As [SourceServerName],
			D.[SysStart]
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
		Select	[CatalogName]
		From	@Values
		Group By [CatalogName]
		Having	Count(*) > 1)
	Throw 50000, '[CatalogName] cannot be duplicate', 2;

	If Exists ( -- Set [SysStart] to Null in parameter data to bypass this check
		Select	D.[CatalogId]
		From	@Values D
				Inner Join [App_DataDictionary].[DatabaseCatalog] A
				On D.[CatalogId] = A.[CatalogId]
		Where	IsNull(D.[SysStart],A.[SysStart]) <> A.[SysStart])
	Throw 50000, '[SysStart] indicates that the Database Row may have changed since the source Row was originally extracted', 3;
	
	-- Cascade Delete
	Declare @Delete Table (
		[CatalogId] UniqueIdentifier Not Null Primary Key)

	Insert Into @Delete
	Select	T.[CatalogId]
	From	[App_DataDictionary].[ApplicationCatalog] T
			Left Join @Values V
			On	V.[CatalogId] = T.[CatalogId]
	Where	V.[CatalogId] is Null And
			T.[ModelId] = @ModelId

	Delete From [App_DataDictionary].[DatabaseColumn]
	Where	[CatalogId] In (Select [CatalogId] From @Delete);

	Delete From [App_DataDictionary].[DatabaseTable]
	Where	[CatalogId] In (Select [CatalogId] From @Delete);

	Delete From [App_DataDictionary].[DatabaseSchema]
	Where	[CatalogId] In (Select [CatalogId] From @Delete);

	Delete From [App_DataDictionary].[ApplicationCatalog]
	Where	[CatalogId] In (Select [CatalogId] From @Delete);

	-- Apply Changes
	With [Delta] As (
		Select	[CatalogId],
				[CatalogName],
				[SourceServerName]
		From	@Values
		Except
		Select	[CatalogId],
				[CatalogName],
				[SourceServerName]
		From	[App_DataDictionary].[DatabaseCatalog]),
	[Data] As (
		Select	V.[CatalogId],
				V.[CatalogName],
				V.[SourceServerName],
				IIF(D.[CatalogId] is Null,1, 0) As [IsDiffrent]
		From	@Values V
				Left Join [Delta] D
				On	V.[CatalogId] = D.[CatalogId])
	Merge [App_DataDictionary].[DatabaseCatalog] As T
	Using [Data] As S
	On	T.[CatalogId] = S.[CatalogId]
	When Matched And S.[IsDiffrent] = 1 Then Update
		Set	[CatalogName] = S.[CatalogName],
			[SourceServerName] = S.[SourceServerName]
	When Not Matched by Target Then
		Insert ([CatalogId], [CatalogName], [SourceServerName])
		Values ([CatalogId], [CatalogName], [SourceServerName])
	When Not Matched by Source Then Delete;

	With [Data] As (
		Select	@ModelId As [ModelId],
				[CatalogId]
		From	@Values)
	Merge [App_DataDictionary].[ApplicationCatalog] T
	Using [Data] D
	On	T.[ModelId] = D.[ModelId] And
		T.[CatalogId] = D.[CatalogId]
	When Not Matched by Target Then
		Insert ([ModelId], [CatalogId])
		Values ([ModelId], [CatalogId])
	When Not Matched by Source And T.[ModelId] = @ModelId Then Delete;

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
-- Provide System Documentation
EXEC sp_addextendedproperty @name = N'MS_Description',
	@level0type = N'SCHEMA', @level0name = N'App_DataDictionary',
    @level1type = N'PROCEDURE', @level1name = N'procSetDatabaseCatalog',
	@value = N'Performs Set on DatabaseCatalog.'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
	@level0type = N'SCHEMA', @level0name = N'App_DataDictionary',
    @level1type = N'PROCEDURE', @level1name = N'procSetDatabaseCatalog',
	@level2type = N'PARAMETER', @level2name = N'@ModelId',
	@value = N'ModelId'
GO