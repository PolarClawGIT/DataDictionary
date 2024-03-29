﻿CREATE PROCEDURE [App_DataDictionary].[procSetDatabaseExtendedProperty]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
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

	-- Validation
	If @ModelId is Null and @CatalogId is Null
	Throw 50000, '@ModelId or @CatalogId must be specified', 1;

	-- Clean the Data
	Declare @Values Table (
		[CatalogId]      UniqueIdentifier Not Null,
		[ExtendedPropertyId] Int Not Null,
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
		Primary Key ([CatalogId], [ExtendedPropertyId]))

	Insert Into @Values
	Select	X.[CatalogId],
			IsNull(A.[ExtendedPropertyId],
				Row_Number() Over (
					Partition By X.[CatalogId]
					Order By D.[ObjName], D.[PropertyName]) +
				(Select	IsNull(Max([ExtendedPropertyId]),0)
				  From	[App_DataDictionary].[DatabaseExtendedProperty]
				  Where	[CatalogId] = X.[CatalogId]))
				As [ExtendedPropertyId],
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
			Inner Join [App_DataDictionary].[DatabaseCatalog_AK] P
			On	D.[DatabaseName] = P.[DatabaseName]
			Left Join [App_DataDictionary].[DatabaseExtendedProperty] A
			On	P.[CatalogId] = A.[CatalogId] And
				IsNull(D.[Level0Name],'') = IsNull(A.[Level0Name],'') And
				IsNull(D.[Level1Name],'') = IsNull(A.[Level1Name],'') And
				IsNull(D.[Level2Name],'') = IsNull(A.[Level2Name],'') And
				D.[ObjName] = A.[ObjName] And
				D.[PropertyName] = A.[PropertyName]
			Cross Apply (
				Select	Coalesce(A.[CatalogId], P.[CatalogId], @CatalogId) As [CatalogId]) X
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
	Delete From [App_DataDictionary].[DatabaseExtendedProperty]
	From	[App_DataDictionary].[DatabaseExtendedProperty] T
			Inner Join [App_DataDictionary].[DatabaseCatalog_AK] P
			On	T.[CatalogId] = P.[CatalogId]
			Left Join @Values S
			On	T.[CatalogId] = S.[CatalogId] And
				T.[ExtendedPropertyId] = S.[ExtendedPropertyId]
	Where	S.[CatalogId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseExtendedProperty]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

;	With [Delta] As (
		Select	[CatalogId],
				[ExtendedPropertyId],
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
				[ExtendedPropertyId],
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
		From	[App_DataDictionary].[DatabaseExtendedProperty])
	Update	[App_DataDictionary].[DatabaseExtendedProperty]
	Set		[CatalogId] = S.[CatalogId],
			[ExtendedPropertyId] = S.[ExtendedPropertyId],
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
	From	[App_DataDictionary].[DatabaseExtendedProperty] T
			Inner Join [Delta] S
			On	T.[CatalogId] = S.[CatalogId] And
				T.[ExtendedPropertyId] = S.[ExtendedPropertyId]
	Print FormatMessage ('Update [App_DataDictionary].[DatabaseExtendedProperty]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DatabaseExtendedProperty] (
			[CatalogId],
			[ExtendedPropertyId],
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
			S.[ExtendedPropertyId],
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
			Left Join [App_DataDictionary].[DatabaseExtendedProperty] T
			On	S.[CatalogId] = T.[CatalogId] And
				S.[ExtendedPropertyId] = T.[ExtendedPropertyId]
	Where	T.[CatalogId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DatabaseExtendedProperty]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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