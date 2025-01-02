CREATE PROCEDURE [AppCatalog].[procSetDomain]
		@CatalogId UniqueIdentifier = Null,
		@DomainId UniqueIdentifier = Null,
		@Data [AppCatalog].[typeDomain] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DatabaseDomain.
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

	-- Clean the Data, helps performance
	Declare @Values Table (
		[DomainId]              UniqueIdentifier Not Null,
		[SchemaId]              UniqueIdentifier Not Null,
		[DomainName]            SysName Not Null,
		[DataType]              SysName Null,
		[DomainDefault]         NVarChar(Max) Null,
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
		Primary Key ([DomainId]))

	Insert Into @Values
	Select	Coalesce(D.[DomainId], H.[DomainId], NewId()) As [DomainId],
			S.[SchemaId],
			NullIf(Trim(D.[DomainName]),'') As [DomainName],
			NullIf(Trim(D.[DataType]),'') As [DataType],
			NullIf(Trim(D.[DomainDefault]),'') As [DomainDefault],
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
			NullIf(Trim(D.[CollationName]),'') As [CollationName]
	From	@Data D
			Left Join [AppCatalog].[DomainHs] H
			On	Coalesce(D.[CatalogId], @CatalogId) = H.[CatalogId] And
				(D.[DomainId] = H.[DomainId] Or (
					D.[SchemaName] = H.[SchemaName] And
					D.[DomainName] = H.[DomainName]))
			Left Join [AppCatalog].[SchemaHs] S
			On	Coalesce(D.[CatalogId], @CatalogId) = S.[CatalogId] And
				D.[SchemaName] = S.[SchemaName]
	Where	(@CatalogId is Null Or @CatalogId = Coalesce(D.[CatalogId], H.[CatalogId])) And
			(@DomainId is Null Or @DomainId = Coalesce(D.[DomainId], H.[DomainId]))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppCatalog].[Domain]
	From	[AppCatalog].[Domain] T
			Inner Join [AppCatalog].[DomainHs] H
			On	T.[DomainId] = H.[DomainId]
			Left Join @Values S
			On	H.[DomainId] = S.[DomainId]
			Cross Apply [AppSecurity].[funcCatalogAuthorization](H.[CatalogId], 1)
	Where	S.[DomainId] is Null And
			(@DomainId is Not Null Or @CatalogId is Not Null) And
			(@DomainId is Null Or @DomainId = H.[DomainId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Print FormatMessage ('Delete [AppCatalog].[Domain]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[DomainId],
				[SchemaId],
				[DomainName],
				[DataType],
				[DomainDefault],
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
				[CollationName]
		From	@Values
	Except
		Select	[DomainId],
				[SchemaId],
				[DomainName],
				[DataType],
				[DomainDefault],
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
				[CollationName]
		From	[AppCatalog].[Domain])
	Update [AppCatalog].[Domain]
	Set		[SchemaId] = S.[SchemaId],
			[DomainName] = S.[DomainName],
			[DataType] = S.[DataType],
			[DomainDefault] = S.[DomainDefault],
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
			[CollationName] = S.[CollationName]
	From	[AppCatalog].[Domain] T
			Inner Join [Delta] S
			On	T.[DomainId] = S.[DomainId]
	Where	T.[SchemaId] In (
				Select	[SchemaId]
				From	[AppCatalog].[SchemaHs]
						Cross Apply [AppSecurity].[funcCatalogAuthorization]([CatalogId], 1))
	Print FormatMessage ('Update [AppCatalog].[Domain]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppCatalog].[Domain] (
			[DomainId],
			[SchemaId],
			[DomainName],
			[DataType],
			[DomainDefault],
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
			[CollationName])
	Select	S.[DomainId],
			S.[SchemaId],
			S.[DomainName],
			S.[DataType],
			S.[DomainDefault],
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
			S.[CollationName]
	From	@Values S
			Left Join [AppCatalog].[Domain] T
			On	S.[DomainId] = T.[DomainId]
	Where	T.[DomainId] is Null And
			S.[SchemaId] In (
				Select	[SchemaId]
				From	[AppCatalog].[SchemaHs]
						Cross Apply [AppSecurity].[funcCatalogAuthorization]([CatalogId], 1))
	Print FormatMessage ('Insert [AppCatalog].[Domain]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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