CREATE PROCEDURE [AppCatalog].[procSetRoutineParameter]
		@CatalogId UniqueIdentifier = Null,
		@RoutineId UniqueIdentifier = Null,
		@Data [AppCatalog].[typeRoutineParameter] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DatabaseRoutineParameter.
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
		[RoutineParameterId]     UniqueIdentifier Not Null,
		[RoutineId]              UniqueIdentifier Not Null,
		[ParameterName]          SysName Not Null,
		[OrdinalPosition]        Int Not Null,
		[DataType]               SysName Null,
		[CharacterMaximumLength] Int Null,
		[CharacterOctetLength]   Int Null,
		[NumericPrecision]       TinyInt Null,
		[NumericPrecisionRadix]  SmallInt Null,
		[NumericScale]           Int Null,
		[DateTimePrecision]      SmallInt Null,
		[CharacterSetCatalog]    SysName Null,
		[CharacterSetSchema]     SysName Null,
		[CharacterSetName]       SysName Null,
		[CollationCatalog]       SysName Null,
		[CollationSchema]        SysName Null,
		[CollationName]          SysName Null,
		[DomainCatalog]          SysName Null,
		[DomainSchema]           SysName Null,
		[DomainName]             SysName Null,
		Primary Key ([RoutineParameterId]))

	Insert Into @Values
	Select	Coalesce(D.[RoutineParameterId], H.[RoutineParameterId], NewId()) As [RoutineParameterId],
			R.[RoutineId],
			NullIf(Trim(D.[ParameterName]),'') As [ParameterName],
			D.[OrdinalPosition],
			NullIf(Trim(D.[DataType] ),'') As [DataType],
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
			NullIf(Trim(D.[DomainName]),'') As [DomainName]
	From	@Data D
			Left Join [AppCatalog].[RoutineParameterHs] H
			On	Coalesce(D.[CatalogId], @CatalogId) = H.[CatalogId] And
				(D.[RoutineParameterId] = H.[RoutineParameterId] Or
				 (D.[SchemaName] = H.[SchemaName] And
				  D.[RoutineName] = H.[RoutineName] And
				  D.[ParameterName] = H.[ParameterName]))
			Left Join [AppCatalog].[RoutineHs] R
			On	Coalesce(D.[CatalogId], @CatalogId) = R.[CatalogId] And
				D.[SchemaName] = R.[SchemaName] And
				D.[RoutineName] = R.[RoutineName]
	Where	(@CatalogId is Null Or @CatalogId = Coalesce(D.[CatalogId], H.[CatalogId])) And
			(@RoutineId is Null Or @RoutineId = Coalesce(R.[RoutineId], H.[RoutineId]))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppCatalog].[RoutineParameter]
	From	[AppCatalog].[RoutineParameter] T
			Inner Join [AppCatalog].[RoutineParameterHs] H
			On	T.[RoutineParameterId] = H.[RoutineParameterId]
			Left Join @Values S
			On	H.[RoutineParameterId] = S.[RoutineParameterId]
	Where	S.[RoutineParameterId] is Null And
			(@RoutineId is Not Null Or @CatalogId is Not Null) And
			(@RoutineId is Null Or @RoutineId = H.[RoutineId]) And
			(@CatalogId is Null Or @CatalogId = H.[CatalogId])
	Print FormatMessage ('Delete [AppCatalog].[RoutineParameter]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[RoutineParameterId],
				[RoutineId],
				[ParameterName],
				[OrdinalPosition],
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
				[CollationSchema],
				[CollationName],
				[DomainCatalog],
				[DomainSchema],
				[DomainName]
		From	@Values
		Except
		Select	[RoutineParameterId],
				[RoutineId],
				[ParameterName],
				[OrdinalPosition],
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
				[CollationSchema],
				[CollationName],
				[DomainCatalog],
				[DomainSchema],
				[DomainName]
		From	[AppCatalog].[RoutineParameter])
	Update [AppCatalog].[RoutineParameter]
	Set		[RoutineId] = S.[RoutineId],
			[ParameterName] = S.[ParameterName],
			[OrdinalPosition] = S.[OrdinalPosition],
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
			[CollationSchema] = S.[CollationSchema],
			[CollationName] = S.[CollationName],
			[DomainCatalog] = S.[DomainCatalog],
			[DomainSchema] = S.[DomainSchema],
			[DomainName] = S.[DomainName]
	From	[AppCatalog].[RoutineParameter] T
			Inner Join [Delta] S
			On	T.[RoutineParameterId] = S.[RoutineParameterId]
	Print FormatMessage ('Update [AppCatalog].[RoutineParameter]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppCatalog].[RoutineParameter] (
			[RoutineParameterId],
			[RoutineId],
			[ParameterName],
			[OrdinalPosition],
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
			[CollationSchema],
			[CollationName],
			[DomainCatalog],
			[DomainSchema],
			[DomainName])
	Select	S.[RoutineParameterId],
			S.[RoutineId],
			S.[ParameterName],
			S.[OrdinalPosition],
			S.[DataType],
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
			S.[DomainName]
	From	@Values S
			Left Join [AppCatalog].[RoutineParameter] T
			On	S.[RoutineParameterId] = T.[RoutineParameterId]
	Where	T.[RoutineParameterId] is Null
	Print FormatMessage ('Insert [AppCatalog].[RoutineParameter]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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