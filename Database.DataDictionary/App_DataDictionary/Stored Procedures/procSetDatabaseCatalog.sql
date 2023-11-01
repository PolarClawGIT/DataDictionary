CREATE PROCEDURE [App_DataDictionary].[procSetDatabaseCatalog]
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

	-- Clean the Data
	Declare @Values [App_DataDictionary].[typeDatabaseCatalog]
	Insert Into @Values
	Select	Coalesce(D.[CatalogId], @CatalogId, NewId()) As [CatalogId],
			NullIf(Trim(D.[CatalogTitle]),'') As [CatalogTitle],
			NullIf(Trim(D.[CatalogDescription]),'') As [CatalogDescription],
			NullIf(Trim(D.[SourceServerName]),'') As [SourceServerName],
			NullIf(Trim(D.[SourceDatabaseName]),'') As [SourceDatabaseName],
			D.[SourceDate]
	From	@Data D
	Where	(@CatalogId is Null or Coalesce(D.[CatalogId], @CatalogId)  = @CatalogId)

	-- Validation
	If @ModelId is Null and @CatalogId is Null
	Throw 50000, '@ModelId or @CatalogId must be specified', 1;

	If Exists (
		Select	[SourceDatabaseName]
		From	(Select	[SourceDatabaseName],
						[CatalogId]
				From	@Values
				Union
				Select	[SourceDatabaseName],
						[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog]) G
		Group By [SourceDatabaseName]
		Having	Count(*) > 1)
	Throw 50000, '[SourceDatabaseName] cannot be duplicate', 2;

	-- Cascade Delete
	Declare @Delete [App_DataDictionary].[typeDatabaseCatalogObject] 

	Insert Into @Delete ([CatalogId])
	Select	T.[CatalogId]
	From	[App_DataDictionary].[DatabaseCatalog] T
			Left Join @Values V
			On	T.[CatalogId] = V.[CatalogId]
			Left Join [App_DataDictionary].[ModelCatalog] M
			On	T.[CatalogId] = M.[CatalogId]
	Where	V.[CatalogId] is Null And
			(@CatalogId is Null or T.[CatalogId] = @CatalogId) And
			(@ModelId is Null or M.[ModelId] = @ModelId)

	if Exists (Select 1 From @Delete)
	Exec [App_DataDictionary].[procDeleteDatabaseCatalogObject] @Delete;

	-- Apply Changes
	Delete From [App_DataDictionary].[ModelCatalog]
	From	[App_DataDictionary].[ModelCatalog] M
			Left Join @Values S
			On	M.[CatalogId] = S.[CatalogId] And
				M.[ModelId] = @ModelId
	Where	S.[CatalogId] is Null And
			(@CatalogId is Null or M.[CatalogId] = @CatalogId) And
			(@ModelId is Null or M.[ModelId] = @ModelId)
	Print FormatMessage ('Delete [App_DataDictionary].[ModelCatalog]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
		From	[App_DataDictionary].[DatabaseCatalog]),
	[Data] As (
		Select	V.[CatalogId],
				V.[CatalogTitle],
				V.[CatalogDescription],
				V.[SourceServerName],
				V.[SourceDatabaseName],
				V.[SourceDate],
				IIF(D.[CatalogId] is Null,0, 1) As [IsDiffrent]
		From	@Values V
				Left Join [Delta] D
				On	V.[CatalogId] = D.[CatalogId])
	Merge [App_DataDictionary].[DatabaseCatalog] As T
	Using [Data] As S
	On	T.[CatalogId] = S.[CatalogId]
	When Matched And S.[IsDiffrent] = 1 Then Update
		Set	[CatalogTitle] = S.[CatalogTitle],
			[CatalogDescription] = S.[CatalogDescription],
			[SourceServerName] = S.[SourceServerName],
			[SourceDatabaseName] = S.[SourceDatabaseName],
			[SourceDate] = S.[SourceDate]
	When Not Matched by Target Then
		Insert ([CatalogId],
				[CatalogTitle],
				[CatalogDescription],
				[SourceServerName],
				[SourceDatabaseName],
				[SourceDate])
		Values ([CatalogId],
				[CatalogTitle],
				[CatalogDescription],
				[SourceServerName],
				[SourceDatabaseName],
				[SourceDate])
	When Not Matched by Source And
		-- Only way to get rid of a Catalog is to remove it by Catalog Id.
		T.[CatalogId] = @CatalogId And
		T.[CatalogId] Not in (
			Select	[CatalogId]
			From	[App_DataDictionary].[ModelCatalog])
		Then Delete;
	Print FormatMessage ('Merge [App_DataDictionary].[DatabaseCatalog]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
