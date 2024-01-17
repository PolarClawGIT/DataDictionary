CREATE PROCEDURE [App_DataDictionary].[procSetDatabaseRoutineParameter]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeDatabaseRoutineParameter] ReadOnly
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
	If @ModelId is Null and @CatalogId is Null
	Throw 50000, '@ModelId or @CatalogId must be specified', 1;

	IF Exists (
		Select	1
		From	@Data D
				Left Join [App_DataDictionary].[DatabaseRoutine_AK] P
				On	Coalesce(D.[CatalogId], @CatalogId) = P.[CatalogId] And
					NullIf(Trim(D.[DatabaseName]),'') = P.[DatabaseName] And
					NullIf(Trim(D.[SchemaName]),'') = P.[SchemaName] And
					NullIf(Trim(D.[RoutineName]),'') = P.[RoutineName]
		Where	P.[CatalogId] is Null)
	Throw 50000, '[DatabaseName], [SchemaName] or [RoutineName] does not match existing data', 2;

	-- Clean the Data
	Declare @Values Table (
		[ParameterId]            UniqueIdentifier Not Null,
		[RoutineId]              UniqueIdentifier Not Null,
		[ParameterName]          SysName Not Null,
		[ScopeId]                Int Not Null,
		[OrdinalPosition]        Int Not Null,
		[DataType]               SysName Null,
		[CharacterMaximumLength]  Int Null,
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
		Primary Key ([ParameterId]))

	;With [Scope] As (
		Select	S.[ScopeId],
				F.[ScopeName]
		From	[App_DataDictionary].[ApplicationScope] S
				Cross Apply [App_DataDictionary].[funcGetScopeName](S.[ScopeId]) F)
	Insert Into @Values
	Select	X.[ParameterId],
			X.[RoutineId],
			NullIf(Trim(D.[ParameterName]),'') As [ParameterName],
			S.[ScopeId],     
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
			Inner Join [App_DataDictionary].[DatabaseRoutine_AK] P
			On	D.[DatabaseName] = P.[DatabaseName] And
				D.[SchemaName] = P.[SchemaName] And
				D.[RoutineName] = P.[RoutineName]
			Left Join [App_DataDictionary].[DatabaseRoutineParameter_AK] A
			On	D.[DatabaseName] = A.[DatabaseName] And
				D.[SchemaName] = A.[SchemaName] And
				D.[RoutineName] = A.[RoutineName] And
				D.[ParameterName] = A.[ParameterName]
			Left Join [Scope] S
			On	D.[ScopeName] = S.[ScopeName]
			Cross Apply (
				Select	Coalesce(A.[ParameterId], D.[ParameterId], NewId()) As [ParameterId],
						Coalesce(A.[RoutineId], P.[RoutineId]) As [RoutineId],
						Coalesce(A.[SchemaId], P.[SchemaId]) As [SchemaId],
						Coalesce(A.[CatalogId], P.[CatalogId], @CatalogId) As [CatalogId]) X
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
	Delete From [App_DataDictionary].[DatabaseRoutineParameter]
	From	[App_DataDictionary].[DatabaseRoutineParameter] T
			Inner Join [App_DataDictionary].[DatabaseRoutine_AK] P
			On	T.[RoutineId] = P.[RoutineId]
			Left Join @Values S
			On	T.[ParameterId] = S.[ParameterId]
	Where	S.[ParameterId] is Null And
			P.[CatalogId] In (
				Select	A.[CatalogId]
				From	[App_DataDictionary].[DatabaseCatalog] A
						Left Join [App_DataDictionary].[ModelCatalog] C
						On	A.[CatalogId] = C.[CatalogId]
				Where	(@CatalogId is Null Or @CatalogId = A.[CatalogId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseRoutineParameter]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[ParameterId],
				[RoutineId],
				[ParameterName],
				[ScopeId], 
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
		Select	[ParameterId],
				[RoutineId],
				[ParameterName],
				[ScopeId], 
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
		From	[App_DataDictionary].[DatabaseRoutineParameter])
	Update [App_DataDictionary].[DatabaseRoutineParameter]
	Set		[RoutineId] = S.[RoutineId],
			[ParameterName] = S.[ParameterName],
			[ScopeId] = S.[ScopeId], 
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
	From	[App_DataDictionary].[DatabaseRoutineParameter] T
			Inner Join [Delta] S
			On	T.[ParameterId] = S.[ParameterId]
	Print FormatMessage ('Update [App_DataDictionary].[DatabaseRoutineParameter]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DatabaseRoutineParameter] (
			[ParameterId],
			[RoutineId],
			[ParameterName],
			[ScopeId], 
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
	Select	S.[ParameterId],
			S.[RoutineId],
			S.[ParameterName],
			S.[ScopeId], 
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
			Left Join [App_DataDictionary].[DatabaseRoutineParameter] T
			On	S.[ParameterId] = T.[ParameterId]
	Where	T.[ParameterId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DatabaseRoutineParameter]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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