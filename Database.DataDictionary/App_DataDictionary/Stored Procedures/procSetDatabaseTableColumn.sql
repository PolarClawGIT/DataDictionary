CREATE PROCEDURE [App_DataDictionary].[procSetDatabaseTableColumn]
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

	-- Validation
	If @ModelId is Null and @CatalogId is Null
	Throw 50000, '@ModelId or @CatalogId must be specified', 1;

	IF Exists (
		Select	1
		From	@Data D
				Left Join [App_DataDictionary].[DatabaseTable_AK] P
				On	Coalesce(D.[CatalogId], @CatalogId) = P.[CatalogId] And
					NullIf(Trim(D.[DatabaseName]),'') = P.[DatabaseName] And
					NullIf(Trim(D.[SchemaName]),'') = P.[SchemaName] And
					NullIf(Trim(D.[TableName]),'') = P.[TableName]
		Where	P.[CatalogId] is Null)
	Throw 50000, '[DatabaseName], [SchemaName] or [TableName] does not match existing data', 2;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[ColumnId]              UniqueIdentifier Not Null,
		[TableId]               UniqueIdentifier Not Null,
		[ColumnName]            SysName Not Null,
		[ScopeId]               Int Not Null,
		[OrdinalPosition]       Int Not Null,
		[IsNullable]            Bit Null,
		[DataType]              SysName Null,
		[ColumnDefault]         NVarChar(Max) Null,
		[CharacterMaximumLength] Int Null,
		[CharacterOctetLength]  Int Null,
		[NumericPrecision]      TinyInt Null,
		[NumericPrecisionRadix] SmallInt Null,
		[NumericScale]          Int Null,
		[DateTimePrecision]     SmallInt Null,
		[CharacterSetCatalog]   SysName Null,
		[CharacterSetSchema]    SysName Null,
		[CharacterSetName]      SysName Null,
		[CollationCatalog]      SysName Null,
		[CollationSchema]       SysName Null,
		[CollationName]         SysName Null,
		[DomainCatalog]         SysName Null,
		[DomainSchema]          SysName Null,
		[DomainName]            SysName Null,
		[IsIdentity]            Bit Null,
		[IsHidden]              Bit Null,
		[IsComputed]            Bit Null,
		[ComputedDefinition]    NVarChar(Max) Null,
		[GeneratedAlwayType]    NVarChar(60) Null,
		Primary Key ([ColumnId]))

	;With [Scope] As (
		Select	S.[ScopeId],
				F.[ScopeName]
		From	[App_DataDictionary].[ApplicationScope] S
				Cross Apply [App_DataDictionary].[funcGetScopeName](S.[ScopeId]) F)
	Insert Into @Values
	Select	Coalesce(A.[ColumnId], D.[ColumnId], NewId()) As [ColumnId],
			P.[TableId],
			NullIf(Trim(D.[ColumnName]),'') As [ColumnName],
			S.[ScopeId],
			D.[OrdinalPosition],
			D.[IsNullable],
			NullIf(Trim(D.[DataType]),'') As [DataType],
			NullIf(Trim(D.[ColumnDefault]),'') As [ColumnDefault],
			D.[CharacterMaximumLength],
			D.[CharacterOctetLength],
			D.[NumericPrecision],
			D.[NumericPrecisionRadix],
			D.[NumericScale],
			D.[DateTimePrecision],
			NullIf(Trim(D.[CharacterSetCatalog]),'') As [CharacterSetCatalog],
			NullIf(Trim(D.[CharacterSetSchema]),'') As [CharacterSetSchema],
			NullIf(Trim(D.[CharacterSetName]),'') As [CharacterSetName],
			NullIf(Trim(D.[CollationCatalog]),'') As [CollationCatalog],
			NullIf(Trim(D.[CollationSchema]),'') As [CollationSchema],
			NullIf(Trim(D.[CollationName]),'') As [CollationName],
			NullIf(Trim(D.[DomainCatalog]),'') As [DomainCatalog],
			NullIf(Trim(D.[DomainSchema]),'') As [DomainSchema],
			NullIf(Trim(D.[DomainName]),'') As [DomainName],
			D.[IsIdentity],
			D.[IsHidden],
			D.[IsComputed],
			NullIf(Trim([ComputedDefinition]),'') As [ComputedDefinition],
			NullIf(Trim([GeneratedAlwayType]),'') As [GeneratedAlwayType]
	From	@Data D
			Left Join [App_DataDictionary].[DatabaseTable_AK] P
			On	Coalesce(D.[CatalogId], @CatalogId) = P.[CatalogId] And
				NullIf(Trim(D.[DatabaseName]),'') = P.[DatabaseName] And
				NullIf(Trim(D.[SchemaName]),'') = P.[SchemaName] And
				NullIf(Trim(D.[TableName]),'') = P.[TableName]
			Left Join [Scope] S
			On	D.[ScopeName] = S.[ScopeName]
			Left Join [App_DataDictionary].[DatabaseTableColumn_AK] A
			On	P.[CatalogId] = A.[CatalogId] And
				P.[SchemaId] = A.[SchemaId] and
				P.[TableId] = A.[TableId] and
				NullIf(Trim(D.[ColumnName]),'') = A.[ColumnName]
	Where	P.[CatalogId] is Null Or
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))

	-- Apply Changes
	Delete From [App_DataDictionary].[DatabaseTableColumn]
	From	[App_DataDictionary].[DatabaseTableColumn] T
			Inner Join [App_DataDictionary].[DatabaseTable_AK] P
			On	T.[TableId] = P.[TableId]
			Left Join @Values S
			On	T.[ColumnId] = S.[ColumnId]
	Where	S.[ColumnId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseTableColumn]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[ColumnId],
				[TableId],
				[ColumnName],
				[ScopeId],
				[OrdinalPosition],
				[IsNullable],
				[DataType],
				[ColumnDefault],
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
				[CollationSchema],
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
		Select	[ColumnId],
				[TableId],
				[ColumnName],
				[ScopeId],
				[OrdinalPosition],
				[IsNullable],
				[DataType],
				[ColumnDefault],
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
				[CollationSchema],
				[CollationName],
				[DomainCatalog],
				[DomainSchema],
				[DomainName],
				[IsIdentity],
				[IsHidden],
				[IsComputed],
				[ComputedDefinition],
				[GeneratedAlwayType]
		From	[App_DataDictionary].[DatabaseTableColumn])
	Update [App_DataDictionary].[DatabaseTableColumn]
	Set		[TableId] = S.[TableId],
			[ColumnName] = S.[ColumnName],
			[ScopeId] = S.[ScopeId],
			[OrdinalPosition] = S.[OrdinalPosition],
			[IsNullable] = S.[IsNullable],
			[DataType] = S.[DataType],
			[ColumnDefault] = S.[ColumnDefault],
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
			[CollationSchema] = S.[CollationSchema],
			[CollationName] = S.[CollationName],
			[DomainCatalog] = S.[DomainCatalog],
			[DomainSchema] = S.[DomainSchema],
			[DomainName] = S.[DomainName],
			[IsIdentity] = S.[IsIdentity],
			[IsHidden] = S.[IsHidden],
			[IsComputed] = S.[IsComputed],
			[ComputedDefinition] = S.[ComputedDefinition],
			[GeneratedAlwayType] = S.[GeneratedAlwayType]
	From	[App_DataDictionary].[DatabaseTableColumn] T
			Inner Join [Delta] S
			On	T.[ColumnId] = S.[ColumnId]
	Print FormatMessage ('Update [App_DataDictionary].[DatabaseTableColumn]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DatabaseTableColumn] (
			[ColumnId],
			[TableId],
			[ColumnName],
			[ScopeId],
			[OrdinalPosition],
			[IsNullable],
			[DataType],
			[ColumnDefault],
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
			[CollationSchema],
			[CollationName],
			[DomainCatalog],
			[DomainSchema],
			[DomainName],
			[IsIdentity],
			[IsHidden],
			[IsComputed],
			[ComputedDefinition],
			[GeneratedAlwayType])
	Select	S.[ColumnId],
			S.[TableId],
			S.[ColumnName],
			S.[ScopeId],
			S.[OrdinalPosition],
			S.[IsNullable],
			S.[DataType],
			S.[ColumnDefault],
			S.[CharacterMaximumLength],
			S.[CharacterOctetLength],
			S.[NumericPrecision],
			S.[NumericPrecisionRadix],
			S.[NumericScale],
			S.[DateTimePrecision],
			S.[CharacterSetCatalog],
			S.[CharacterSetSchema],
			S.[CharacterSetName],
			S.[CollationCatalog],
			S.[CollationSchema],
			S.[CollationName],
			S.[DomainCatalog],
			S.[DomainSchema],
			S.[DomainName],
			S.[IsIdentity],
			S.[IsHidden],
			S.[IsComputed],
			S.[ComputedDefinition],
			S.[GeneratedAlwayType]
	From	@Values S
			Left Join [App_DataDictionary].[DatabaseTableColumn] T
			On	S.[ColumnId] = T.[ColumnId]
	Where	T.[ColumnId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DatabaseTableColumn]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
