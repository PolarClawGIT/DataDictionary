CREATE PROCEDURE [AppCatalog].[procSetTableColumn]
		@CatalogId UniqueIdentifier = Null,
		@TableId UniqueIdentifier = Null,
		@Data [AppCatalog].[typeTableColumn] ReadOnly
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
	If @CatalogId is Not Null And
		Exists (
			Select	1
			From	@Data
			Where	IsNull([CatalogId], @CatalogId) <> @CatalogId)
	Throw 601010, '@Data contains other Catalogs', 1;

	If Exists (
		Select	1
		From	@Data D
				Cross Apply [AppSecurity].[funcCatalogAuthorization](IsNull(D.[CatalogId], @CatalogId), 0))
	Throw 601020, 'Catalog Not Authorized', 2;

	If @TableId is Not Null And Exists (
		Select	1
		From	@Data D
				Inner Join [AppCatalog].[TableColumnHs] T
				On	Coalesce(D.[CatalogId], @CatalogId) = T.[CatalogId] And
					(D.[TableColumnId] = T.[TableColumnId] Or
					 (D.[SchemaName] = T.[SchemaName] And
					  D.[TableName] = T.[TableName] And
					  D.[ColumnName] = T.[ColumnName]))
		Where	IsNull(T.[TableId], @TableId) <> @TableId)
	Throw 602030, '@Data contains other Tables', 3;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[TableColumnId]         UniqueIdentifier Not Null,
		[TableId]               UniqueIdentifier Not Null,
		[ColumnName]            SysName Not Null,
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
		Primary Key ([TableColumnId]),
		Unique ([TableId], [ColumnName]))

	Insert Into @Values
	Select	Coalesce(D.[TableColumnId], H.[TableColumnId], NewId()) As [TableColumnId],
			T.[TableId],
			NullIf(Trim(D.[ColumnName]),'') As [ColumnName],
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
			NullIf(Trim(D.[ComputedDefinition]),'') As [ComputedDefinition],
			NullIf(Trim(D.[GeneratedAlwayType]),'') As [GeneratedAlwayType]
	From	@Data D
			Left Join [AppCatalog].[TableColumnHs] H
			On	Coalesce(D.[CatalogId], @CatalogId) = H.[CatalogId] And
				(D.[TableColumnId] = H.[TableColumnId] Or
				 (D.[SchemaName] = H.[SchemaName] And
				  D.[TableName] = H.[TableName] And
				  D.[ColumnName] = H.[ColumnName]))
			Left Join [AppCatalog].[TableHs] T
			On	Coalesce(D.[CatalogId], @CatalogId) = T.[CatalogId] And
				D.[SchemaName] = T.[SchemaName] And
				D.[TableName] = T.[TableName]
	Where	(@CatalogId is Null Or @CatalogId = Coalesce(D.[CatalogId], H.[CatalogId])) And
			(@TableId is Null Or @TableId = Coalesce(T.[TableId], H.[TableId]))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppCatalog].[ConstraintColumn]
	From	[AppCatalog].[ConstraintColumn] T
			Inner Join [AppCatalog].[ConstraintColumnHs] H
			On	T.[ConstraintColumnId] = H.[ConstraintColumnId]
			Left Join @Values S
			On	H.[TableColumnId] = S.[TableColumnId]
			Cross Apply [AppSecurity].[funcCatalogAuthorization](H.[CatalogId], 1)
	Where	S.[TableId] is Null And
			(@TableId is Not Null Or @CatalogId is Not Null) And
			(@TableId is Null Or @TableId = H.[TableId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Print FormatMessage ('Delete [AppCatalog].[ConstraintColumn] (TableColumn): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[TableColumn]
	From	[AppCatalog].[TableColumn] T
			Inner Join [AppCatalog].[TableColumnHs] H
			On	T.[TableColumnId] = H.[TableColumnId]
			Left Join @Values S
			On	H.[TableColumnId] = S.[TableColumnId]
			Cross Apply [AppSecurity].[funcCatalogAuthorization](H.[CatalogId], 1)
	Where	S.[TableColumnId] is Null And
			(@TableId is Not Null Or @CatalogId is Not Null) And
			(@TableId is Null Or @TableId = H.[TableId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Print FormatMessage ('Delete [AppCatalog].[TableColumn]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[TableColumnId],
				[TableId],
				[ColumnName],
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
		Select	[TableColumnId],
				[TableId],
				[ColumnName],
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
		From	[AppCatalog].[TableColumn])
	Update [AppCatalog].[TableColumn]
	Set		[TableId] = S.[TableId],
			[ColumnName] = S.[ColumnName],
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
	From	[AppCatalog].[TableColumn] T
			Inner Join [Delta] S
			On	T.[TableColumnId] = S.[TableColumnId]
	Where	T.[TableId] In (
				Select	[TableId]
				From	[AppCatalog].[TableHs]
						Cross Apply [AppSecurity].[funcCatalogAuthorization]([CatalogId], 1))
	Print FormatMessage ('Update [AppCatalog].[TableColumn]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppCatalog].[TableColumn] (
			[TableColumnId],
			[TableId],
			[ColumnName],
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
	Select	S.[TableColumnId],
			S.[TableId],
			S.[ColumnName],
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
			Left Join [AppCatalog].[TableColumn] T
			On	S.[TableColumnId] = T.[TableColumnId]
	Where	T.[TableColumnId] is Null And
			S.[TableId] In (
				Select	[TableId]
				From	[AppCatalog].[TableHs]
						Cross Apply [AppSecurity].[funcCatalogAuthorization]([CatalogId], 1))
	Print FormatMessage ('Insert [AppCatalog].[TableColumn]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
