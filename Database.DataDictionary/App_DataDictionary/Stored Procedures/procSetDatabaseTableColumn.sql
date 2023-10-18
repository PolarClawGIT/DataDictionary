﻿CREATE PROCEDURE [App_DataDictionary].[procSetDatabaseTableColumn]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeDatabaseTableColumn] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DatabaseColumn.
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
	Declare @Values [App_DataDictionary].[typeDatabaseTableColumn]
	Insert Into @Values
	Select	Coalesce(D.[CatalogId], @CatalogId, P.[CatalogId]) As [CatalogId],
			NullIf(Trim(IsNull(P.[SourceDatabaseName], D.[DatabaseName])),'') As [DatabaseName],
			NullIf(Trim(D.[SchemaName]),'') As [SchemaName],
			NullIf(Trim(D.[TableName]),'') As [TableName],
			Null As [TableType], -- Handled by DatabaseTable
			NullIf(Trim([ColumnName]),'') As [ColumnName],
			[OrdinalPosition],
			[IsNullable],
			NullIf(Trim([DataType]),'') As [DataType],
			NullIf(Trim([ColumnDefault]),'') As [ColumnDefault],
			[CharacterMaximumLength],
			[CharacterOctetLength],
			[NumericPrecision],
			[NumericPrecisionRadix],
			[NumericScale],
			[DateTimePrecision],
			NullIf(Trim([CharacterSetCatalog]),'') As [CharacterSetCatalog],
			NullIf(Trim([CharacterSetSchema]),'') As [CharacterSetSchema],
			NullIf(Trim([CharacterSetName]),'') As [CharacterSetName],
			NullIf(Trim([CollationCatalog]),'') As [CollationCatalog],
			NullIf(Trim([CollationSchema]),'') As [CollationSchema],
			NullIf(Trim([CollationName]),'') As [CollationName],
			NullIf(Trim([DomainCatalog]),'') As [DomainCatalog],
			NullIf(Trim([DomainSchema]),'') As [DomainSchema],
			NullIf(Trim([DomainName]),'') As [DomainName],
			[IsIdentity],
			[IsHidden],
			[IsComputed],
			NullIf(Trim([ComputedDefinition]),'') As [ComputedDefinition],
			NullIf(Trim([GeneratedAlwayType]),'') As [GeneratedAlwayType]
	From	@Data D
			Left Join [App_DataDictionary].[DatabaseCatalog] P
			On	Coalesce(D.[CatalogId], @CatalogId) = P.[CatalogId]
	Where	(@CatalogId is Null or D.[CatalogId] = @CatalogId) And
			(@ModelId is Null or @ModelId In (
				Select	[ModelId]
				From	[App_DataDictionary].[ModelCatalog]
				Where	(@CatalogId is Null Or [CatalogId] = @CatalogId)))

	-- Validation
	If @ModelId is Null and @CatalogId is Null
	Throw 50000, '@ModelId or @CatalogId must be specified', 1;

	If Exists (
		Select	[DatabaseName], [SchemaName], [TableName], [ColumnName]
		From	@Values
		Group By [DatabaseName], [SchemaName], [TableName], [ColumnName]
		Having	Count(*) > 1)
	Throw 50000, '[ColumnName] cannot be duplicate within a Table', 2;

	-- Apply Changes
		With [Delta] As (
		Select	[CatalogId],
				[SchemaName],
				[TableName],
				[ColumnName],
				[OrdinalPosition],
				[ColumnDefault],
				[IsNullable],
				[DataType],
				[CharacterMaximumLength],
				[CharacterOctetLength],
				[NumericPrecision],
				[NumericPrecisionRadix],
				[NumericScale],
				[DateTimePrecision],
				[CharacterSetCatalog],
				[CharacterSetSchema],
				[CharacterSetName],
				[CollationCatalog],
				[CollationName],
				[DomainCatalog],
				[DomainSchema],
				[DomainName],
				[IsIdentity],
				[IsHidden],
				[IsComputed],
				[ComputedDefinition],
				[GeneratedAlwayType]
		From	@Values
		Except
		Select	[CatalogId],
				[SchemaName],
				[TableName],
				[ColumnName],
				[OrdinalPosition],
				[ColumnDefault],
				[IsNullable],
				[DataType],
				[CharacterMaximumLength],
				[CharacterOctetLength],
				[NumericPrecision],
				[NumericPrecisionRadix],
				[NumericScale],
				[DateTimePrecision],
				[CharacterSetCatalog],
				[CharacterSetSchema],
				[CharacterSetName],
				[CollationCatalog],
				[CollationName],
				[DomainCatalog],
				[DomainSchema],
				[DomainName],
				[IsIdentity],
				[IsHidden],
				[IsComputed],
				[ComputedDefinition],
				[GeneratedAlwayType]
		From	[App_DataDictionary].[DatabaseTableColumn]),
	[Data] As (
		Select	V.[CatalogId],
				V.[SchemaName],
				V.[TableName],
				V.[ColumnName],
				V.[OrdinalPosition],
				V.[ColumnDefault],
				V.[IsNullable],
				V.[DataType],
				V.[CharacterMaximumLength],
				V.[CharacterOctetLength],
				V.[NumericPrecision],
				V.[NumericPrecisionRadix],
				V.[NumericScale],
				V.[DateTimePrecision],
				V.[CharacterSetCatalog],
				V.[CharacterSetSchema],
				V.[CharacterSetName],
				V.[CollationCatalog],
				V.[CollationName],
				V.[DomainCatalog],
				V.[DomainSchema],
				V.[DomainName],
				V.[IsIdentity],
				V.[IsHidden],
				V.[IsComputed],
				V.[ComputedDefinition],
				V.[GeneratedAlwayType],
				IIF(D.[CatalogId] is Null,0, 1) As [IsDiffrent]
		From	@Values V
				Left Join [Delta] D
				On	V.[CatalogId] = D.[CatalogId] And
					V.[SchemaName] = D.[SchemaName] And
					V.[TableName] = D.[TableName] And
					V.[ColumnName] = D.[ColumnName])
	Merge [App_DataDictionary].[DatabaseTableColumn] As T
	Using [Data] As S
	On	T.[CatalogId] = S.[CatalogId] And
		T.[SchemaName] = S.[SchemaName] And
		T.[TableName] = S.[TableName] And
		T.[ColumnName] = S.[ColumnName]
	When Matched and [IsDiffrent] = 1 Then Update Set
		[OrdinalPosition] = S.[OrdinalPosition],
		[ColumnDefault] = S.[ColumnDefault],
		[IsNullable] = S.[IsNullable],
		[DataType] = S.[DataType],
		[CharacterMaximumLength] = S.[CharacterMaximumLength],
		[CharacterOctetLength] = S.[CharacterOctetLength],
		[NumericPrecision] = S.[NumericPrecision],
		[NumericPrecisionRadix] = S.[NumericPrecisionRadix],
		[NumericScale] = S.[NumericScale],
		[DateTimePrecision] = S.[DateTimePrecision],
		[CharacterSetCatalog] = S.[CharacterSetCatalog],
		[CharacterSetSchema] = S.[CharacterSetSchema],
		[CharacterSetName] = S.[CharacterSetName],
		[CollationCatalog] = S.[CollationCatalog],
		[CollationName] = S.[CollationName],
		[DomainCatalog] = S.[DomainCatalog],
		[DomainSchema] = S.[DomainSchema],
		[DomainName] = S.[DomainName],
		[IsIdentity] = S.[IsIdentity],
		[IsHidden] = S.[IsHidden],
		[IsComputed] = S.[IsComputed],
		[ComputedDefinition] = S.[ComputedDefinition],
		[GeneratedAlwayType] = S.[GeneratedAlwayType]
	When Not Matched by Target Then
		Insert ([CatalogId],
				[SchemaName],
				[TableName],
				[ColumnName],
				[OrdinalPosition],
				[ColumnDefault],
				[IsNullable],
				[DataType],
				[CharacterMaximumLength],
				[CharacterOctetLength],
				[NumericPrecision],
				[NumericPrecisionRadix],
				[NumericScale],
				[DateTimePrecision],
				[CharacterSetCatalog],
				[CharacterSetSchema],
				[CharacterSetName],
				[CollationCatalog],
				[CollationName],
				[DomainCatalog],
				[DomainSchema],
				[DomainName],
				[IsIdentity],
				[IsHidden],
				[IsComputed],
				[ComputedDefinition],
				[GeneratedAlwayType])
		Values ([CatalogId],
				[SchemaName],
				[TableName],
				[ColumnName],
				[OrdinalPosition],
				[ColumnDefault],
				[IsNullable],
				[DataType],
				[CharacterMaximumLength],
				[CharacterOctetLength],
				[NumericPrecision],
				[NumericPrecisionRadix],
				[NumericScale],
				[DateTimePrecision],
				[CharacterSetCatalog],
				[CharacterSetSchema],
				[CharacterSetName],
				[CollationCatalog],
				[CollationName],
				[DomainCatalog],
				[DomainSchema],
				[DomainName],
				[IsIdentity],
				[IsHidden],
				[IsComputed],
				[ComputedDefinition],
				[GeneratedAlwayType])
	When Not Matched by Source And
		(@CatalogId = T.[CatalogId] Or
		 T.[CatalogId] In (
			Select	[CatalogId]
			From	[App_DataDictionary].[ModelCatalog]
			Where	[ModelId] = @ModelId))
		Then Delete;
	Print FormatMessage ('Merge [App_DataDictionary].[DatabaseTableColumn]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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