CREATE PROCEDURE [AppModel].[procSetProperty]
		@ModelId UniqueIdentifier = Null,
		@PropertyId UniqueIdentifier = Null,
		@Data [AppModel].[udttProperty] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DomainProperty.
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
	If Exists (
		Select	1
		From	[AppSecurity].[funcCatalogAuthorization](@ModelId, 0))
	Throw 601020, 'Model Not Authorized', 2;

	-- Clean the Data
	Declare @Values Table (
		[PropertyId]             UniqueIdentifier NOT NULL,
		[PropertyTitle]          [AppGeneral].[uddtTitle] Not Null,
		[PropertyDescription]    [AppGeneral].[uddtDescription] Null,
		[IsCommon]               Bit Not Null,
		[DataType]               NVarChar(20) Not Null,
		[PropertyData]           NVarChar(2000) Null,
		Primary Key ([PropertyId]))

	;With [Choice] As (
		Select	[PropertyId],
				String_Agg(NullIf(Trim(L.[value]),''),', ') Within Group (Order By L.[value]) As [ChoiceList]
		From	@Data D
				Cross Apply String_Split(D.[PropertyData],',') L
		Where	[DataType] In ('List')
		Group By D.[PropertyId])
	Insert Into @Values
	Select	Coalesce(D.[PropertyId], @PropertyId, NewId()),
			NullIf(Trim(D.[PropertyTitle]),'') As [PropertyTitle],
			NullIf(Trim(D.[PropertyDescription]),'') As [PropertyDescription],
			IsNull(D.[IsCommon],0) As [IsCommon],
			NullIf(Trim(D.[DataType]),'') As [PropertyType],
			IsNull(C.[ChoiceList], D.[PropertyData]) As [PropertyData]
	From	@Data D
			Left Join [Choice] C
			On	D.[PropertyId] = C.[PropertyId]
	Where	(@PropertyId is Null Or @PropertyId = Coalesce(D.[PropertyId], @PropertyId))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppModel].[ModelProperty]
	From	[AppModel].[ModelProperty] T
			Inner Join [AppModel].[ModelPropertyHs] H
			On	T.[PropertyId] = H.[PropertyId] And
				T.[ModelId] = H.[ModelId]
			Left Join @Values S
			On	H.[PropertyId] = S.[PropertyId]
			Cross Apply [AppSecurity].[funcModelAuthorization](@ModelId, 1)
	Where	S.[PropertyId] is Null And
			(@PropertyId is Not Null Or @ModelId is Not Null) And
			(@PropertyId is Null Or H.[PropertyId] = @PropertyId) And
			(@ModelId is Null Or H.[ModelId] = @ModelId)
	Print FormatMessage ('Delete [AppModel].[ModelProperty]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[PropertyEnumeration]
	From	[AppModel].[PropertyEnumeration] T
			Left Join @Values S
			On	T.[PropertyId] = S.[PropertyId]
			Cross Apply [AppSecurity].[funcModelAuthorization](Null, 1)
	Where	S.[PropertyId] is Null And
			T.[PropertyId] = @PropertyId 
	Print FormatMessage ('Delete [AppModel].[PropertyEnumeration]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[PropertyId],
				[PropertyTitle],
				[PropertyDescription],
				[IsCommon],
				[DataType],
				[PropertyData]
		From	@Values
		Except
		Select	[PropertyId],
				[PropertyTitle],
				[PropertyDescription],
				[IsCommon],
				[DataType],
				[PropertyData]
		From	[AppModel].[PropertyEnumeration])
	Update [AppModel].[PropertyEnumeration]
	Set		[PropertyTitle] = S.[PropertyTitle],
			[PropertyDescription] = S.[PropertyDescription],
			[DataType] = S.[DataType],
			[PropertyData] = S.[PropertyData],
			[IsCommon] = S.[IsCommon]
	From	[AppModel].[PropertyEnumeration] T
			Inner Join [Delta] S
			On	T.[PropertyId] = S.[PropertyId]
			Cross Apply [AppSecurity].[funcModelAuthorization](Null, 1)
	Print FormatMessage ('Update [AppModel].[PropertyEnumeration]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[PropertyEnumeration] (
			[PropertyId],
			[PropertyTitle],
			[PropertyDescription],
			[IsCommon],
			[DataType],
			[PropertyData])
	Select	S.[PropertyId],
			S.[PropertyTitle],
			S.[PropertyDescription],
			S.[IsCommon],
			S.[DataType],
			S.[PropertyData]
	From	@Values S
			Left Join [AppModel].[PropertyEnumeration] T
			On	S.[PropertyId] = T.[PropertyId]
			Cross Apply [AppSecurity].[funcModelAuthorization](Null, 1)
	Where	T.[PropertyId] is Null
	Print FormatMessage ('Insert [AppModel].[PropertyEnumeration]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[ModelProperty] (
			[ModelId],
			[PropertyId])
	Select	@ModelId,
			S.[PropertyId]
	From	@Values S
			Left Join [AppModel].[ModelProperty] T
			On	@ModelId = T.[ModelId] And
				S.[PropertyId] = T.[PropertyId]
			Cross Apply [AppSecurity].[funcModelAuthorization](@ModelId, 1)
	Where	T.[PropertyId] is Null And
			@ModelId is Not Null
	Print FormatMessage ('Insert [AppModel].[ModelProperty]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
/*
Begin Try;
	Begin Transaction;
	Set NoCount On;

	Declare @Data [AppModel].[typeDomainProperty]

	Insert Into @Data Values (
		'00000000-0000-0000-0010-000000000010',
		'MS SQL Description',
		'Commonly used by Microsoft tools to store the user defined Description of the element.',
		'MS_ExtendedProperty', 'MS_Description')
	Insert Into @Data Values (
		'00000000-0000-0000-0020-000000000010',
		'.Net Summary',
		'Summary Documentation block for .Net Framework Code',
		'String', Null)

	Insert Into @Data Values (
		'00000000-0000-0000-0030-000000000010',
		'.Net System Type',
		'The types for .Net Framework Code. (This is an incomplete sub-set of the most commonly used types based on the list compatible with TSQL)',
		'List','Boolean, Byte, Byte[], Char[], DateTime, DateTimeOffset, Decimal, Double, Guid, Int16, Int32, Int64, Single, String, TimeSpan')

	Insert Into @Data Values (
		'00000000-0000-0000-0040-000000000010',
		'MS SQL Type',
		'The types for MS SQL',
		'List','bigint, binary, bit, char, date, datetime, datetime2, datetimeoffset, decimal, float, geography, geometry, hierarchyid, image, int, money, nchar, ntext, numeric, nvarchar, real, smalldatetime, smallint, smallmoney, sql_variant, sysname, text, time, timestamp, tinyint, uniqueidentifier, varbinary, varchar, xml')

	Insert Into @Data Values (
		'00000000-0000-0000-0050-000000000010',
		'Length',
		'The Maxium Length of the value. Used with varaible length types.',
		'Integer',Null)

	Exec [AppModel].[procSetDomainProperty] @Data = @Data

	update [AppModel].[DomainProperty]
	Set		[IsCommon] = 1

	Select	*
	From	[AppModel].[DomainProperty]

	-- By default, throw and error and exit without committing
--;	Throw 50000, 'Abort process, comment out this line when ready to actual Commit the transaction',255;
	
	Commit Transaction;
	Print 'Commit Issued';
End Try
Begin Catch
	Print FormatMessage ('*** Error Report: %s ***', Object_Name(@@ProcID));
	Print FormatMessage (' Message- %s', ERROR_MESSAGE());
	Print FormatMessage (' Number- %i', ERROR_NUMBER());
	Print FormatMessage (' Severity- %i', ERROR_SEVERITY());
	Print FormatMessage (' State- %i', ERROR_STATE());
	Print FormatMessage (' Procedure- %s', ERROR_PROCEDURE());
	Print FormatMessage (' Line- %i', ERROR_LINE());
	Print FormatMessage (' @@TranCount - %i', @@TranCount);
	Print FormatMessage (' @@NestLevel - %i', @@NestLevel);
	Print FormatMessage (' Original_Login - %s', Original_Login());
	Print FormatMessage (' Current_User - %s', Current_User);
	Print FormatMessage (' XAct_State - %i', XAct_State());
	Print '--- Debug Data ---';

	-- Rollback Transaction
	Print 'Rollback Issued';
	Rollback Transaction;
	Throw;
End Catch;
*/