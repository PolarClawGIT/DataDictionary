CREATE PROCEDURE [AppCatalog].[procSetProperty]
		@CatalogId     UniqueIdentifier = Null,
		@PropertyId    UniqueIdentifier = Null,
		@Data          [AppCatalog].[typeProperty] ReadOnly
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

	-- Validation
	If @CatalogId is Not Null And
		Exists (
			Select	1
			From	@Data
			Where	IsNull([CatalogId], @CatalogId) <> @CatalogId)
	Throw 601010, '@Data contains other Catalogs', 1;

	-- Clean the Data
	Declare @Values Table (
		[CatalogId]      UniqueIdentifier Not Null,
		[PropertyId]     UniqueIdentifier Not Null,
		-- Parameters for [fn_listextendedproperty]
		[Level0Type]     SysName Null,
		[Level0Name]     SysName Null,
		[Level1Type]     SysName Null,
		[Level1Name]     SysName Null,
		[Level2Type]     SysName Null,
		[Level2Name]     SysName Null,
		-- Results from [fn_listextendedproperty]
		[ObjType]        SysName Not Null,
		[ObjName]        SysName Not Null,
		[PropertyName]   SysName Not Null,
		[PropertyValue]  NVarChar(Max) Null,
		Primary Key ([PropertyId]),
		Unique ([CatalogId], [Level0Name], [Level1Name], [Level2Name], [PropertyName]))

	Insert Into @Values
	Select	Coalesce(T.[CatalogId], D.[CatalogId], @CatalogId) As [CatalogId],
			Coalesce(T.[PropertyId], D.[PropertyId], NewId()) As [PropertyId],
			-- Parameters for [fn_listextendedproperty]
			D.[Level0Type],
			D.[Level0Name],
			D.[Level1Type],
			D.[Level1Name],
			D.[Level2Type],
			D.[Level2Name],
			-- Results from [fn_listextendedproperty]
			D.[ObjType],
			D.[ObjName],
			D.[PropertyName],
			D.[PropertyValue]
	From	@Data D
			Left Join [AppCatalog].[PropertyHs] T
			On	IsNull(@CatalogId, D.[CatalogId]) = T.[CatalogId] And
				IsNull(D.[Level0Name],'') = IsNull(T.[Level0Name],'') And
				IsNull(D.[Level1Name],'') = IsNull(T.[Level1Name],'') And
				IsNull(D.[Level2Name],'') = IsNull(T.[Level2Name],'') And
				IsNull(D.[PropertyName],'') = IsNull(T.[PropertyName],'')
	Where	(@PropertyId is Null Or @PropertyId = Coalesce(T.[PropertyId], D.[PropertyId]) ) And
			(@CatalogId is Null Or @CatalogId = Coalesce(T.[CatalogId], D.[CatalogId], @CatalogId))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppCatalog].[Property]
	From	[AppCatalog].[Property] T
			Inner Join [AppCatalog].[PropertyHs] H
			On	T.[PropertyId] = H.[PropertyId]
			Left Join @Values S
			On	H.[PropertyId] = S.[PropertyId]
	Where	S.[PropertyId] is Null And
			(@PropertyId is Not Null Or @CatalogId is Not Null) And
			(@PropertyId is Null Or @PropertyId = H.[PropertyId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Print FormatMessage ('Delete [AppCatalog].[Property]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

;	With [Delta] As (
		Select	[CatalogId],
				[PropertyId],
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
		Select	[CatalogId],
				[PropertyId],
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
		From	[AppCatalog].[Property])
	Update	[AppCatalog].[Property]
	Set		[CatalogId] = S.[CatalogId],
			[PropertyId] = S.[PropertyId],
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
	From	[AppCatalog].[Property] T
			Inner Join [Delta] S
			On	T.[CatalogId] = S.[CatalogId] And
				T.[PropertyId] = S.[PropertyId]
	Print FormatMessage ('Update [AppCatalog].[Property]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppCatalog].[Property] (
			[CatalogId],
			[PropertyId],
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
	Select	S.[CatalogId],
			S.[PropertyId],
			S.[Level0Type],
			S.[Level0Name],
			S.[Level1Type],
			S.[Level1Name],
			S.[Level2Type],
			S.[Level2Name],
			S.[ObjType],
			S.[ObjName],
			S.[PropertyName],
			S.[PropertyValue]
	From	@Values S
			Left Join [AppCatalog].[Property] T
			On	S.[CatalogId] = T.[CatalogId] And
				S.[PropertyId] = T.[PropertyId]
	Where	T.[CatalogId] is Null
	Print FormatMessage ('Insert [AppCatalog].[Property]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
	-- Rollback Transaction
	If @TRN_IsNewTran = 1
	  Begin -- If this is the outer transaction, roll it back
		Rollback Transaction
		Print FormatMessage ('Rollback Transaction Issued ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
	  End -- Rollback Transaction
	-- This is a nested transaction, must be rolled back by outer transaction
	Else Print FormatMessage ('Rollback Transaction Pending ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))

	If ERROR_NUMBER() >= 50000 Exec [AppGeneral].[procThrowHelpSubject]
	Else If ERROR_SEVERITY() Not In (0, 11) Throw;
End Catch
GO