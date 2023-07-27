CREATE PROCEDURE [App_DataDictionary].[procSetDatabaseExtendedProperty]
		@ModelId UniqueIdentifier,
		@Data    [App_DataDictionary].[typeDatabaseExtendedProperty] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DatabaseExtendedProperty.
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
	Declare @Values  [App_DataDictionary].[typeDatabaseExtendedProperty]
	Insert Into @Values
	Select	IsNull(O.[PropertyId],newid()) As [PropertyId],
			P.[CatalogId],
			P.[CatalogName] As [CatalogName],
			NullIf(Trim(D.[Level0Type]),'') As [Level0Type],
			NullIf(Trim(D.[Level0Name]),'') As [Level0Name],
			NullIf(Trim(D.[Level1Type]),'') As [Level1Type],
			NullIf(Trim(D.[Level1Name]),'') As [Level1Name],
			NullIf(Trim(D.[Level2Type]),'') As [Level2Type],
			NullIf(Trim(D.[Level2Name]),'') As [Level2Name],
			NullIf(Trim(D.[ObjType]),'') As [ObjType],
			NullIf(Trim(D.[ObjName]),'') As [ObjName],
			NullIf(Trim(D.[PropertyName]),'') As [PropertyName],
			NullIf(D.[PropertyValue],'') As [PropertyValue]
	From	@Data D
			Left Join [App_DataDictionary].[ApplicationCatalog] C
			On	C.[ModelId] = @ModelId
			Left Join [App_DataDictionary].[DatabaseCatalog] P
			On	C.[CatalogId] = P.[CatalogId] And
				D.[CatalogName] = P.[CatalogName]
			Left Join [App_DataDictionary].[DatabaseExtendedProperty] O
			On	P.[CatalogId] = O.[CatalogId] And
				IsNull(D.[Level0Name],'') = IsNull(O.[Level0Name],'') And
				IsNull(D.[Level1Name],'') = IsNull(O.[Level1Name],'') And
				IsNull(D.[Level2Name],'') = IsNull(O.[Level2Name],'') And
				IsNull(D.[PropertyName],'') = IsNull(O.[PropertyName],'')

	-- Validation
	If Not Exists (Select 1 From [App_DataDictionary].[ApplicationModel] Where [ModelId] = @ModelId)
	Throw 50000, '[ModelId] could not be found that matched the parameter', 1;

	-- Apply Changes
	With [Delta] As (
		Select	[PropertyId],
				[Level0Type],
				[Level0Name],
				[Level1Type],
				[Level1Name],
				[Level2Type],
				[Level2Name],
				[ObjType],
				[ObjName],
				[PropertyName],
				[PropertyValue]
		From	@Values
		Except
		Select	[PropertyId],
				[Level0Type],
				[Level0Name],
				[Level1Type],
				[Level1Name],
				[Level2Type],
				[Level2Name],
				[ObjType],
				[ObjName],
				[PropertyName],
				[PropertyValue]
		From	[App_DataDictionary].[DatabaseExtendedProperty]),
	[Data] As (
		Select	V.[PropertyId],
				V.[CatalogId],
				V.[Level0Type],
				V.[Level0Name],
				V.[Level1Type],
				V.[Level1Name],
				V.[Level2Type],
				V.[Level2Name],
				V.[ObjType],
				V.[ObjName],
				V.[PropertyName],
				V.[PropertyValue],
				IIF(D.[PropertyId] is Null,1, 0) As [IsDiffrent]
		From	@Values V
				Left Join [Delta] D
				On	V.[PropertyId] = D.[PropertyId])
	Merge [App_DataDictionary].[DatabaseExtendedProperty] T
	Using [Data] S
	On	T.[PropertyId] = S.[PropertyId]
	When Matched and [IsDiffrent] = 1 Then Update Set
		[Level0Type] = S.[Level0Type],
		[Level0Name] = S.[Level0Name],
		[Level1Type] = S.[Level1Type],
		[Level1Name] = S.[Level1Name],
		[Level2Type] = S.[Level2Type],
		[Level2Name] = S.[Level2Name],
		[ObjType] = S.[ObjType],
		[ObjName] = S.[ObjName],
		[PropertyName] = S.[PropertyName],
		[PropertyValue] = S.[PropertyValue]
	When Not Matched by Target Then
		Insert ([PropertyId],
				[CatalogId],
				[Level0Type],
				[Level0Name],
				[Level1Type],
				[Level1Name],
				[Level2Type],
				[Level2Name],
				[ObjType],
				[ObjName],
				[PropertyName],
				[PropertyValue])
		Values ([PropertyId],
				[CatalogId],
				[Level0Type],
				[Level0Name],
				[Level1Type],
				[Level1Name],
				[Level2Type],
				[Level2Name],
				[ObjType],
				[ObjName],
				[PropertyName],
				[PropertyValue])
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
	Print FormatMessage (' @ModelId- %s',Convert(NVarChar,@ModelId))

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