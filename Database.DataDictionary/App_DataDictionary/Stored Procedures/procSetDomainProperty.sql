﻿CREATE PROCEDURE [App_DataDictionary].[procSetDomainProperty]
		@PropertyId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeDomainProperty] ReadOnly
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

	-- Clean the Data
	Declare @Values Table (
		[PropertyId]             UniqueIdentifier NOT NULL,
		[PropertyTitle]          [App_DataDictionary].[typeTitle] Not Null,
		[PropertyDescription]    [App_DataDictionary].[typeDescription] Null,
		[PropertyType]           NVarChar(20) Not Null,
		[PropertyData]           NVarChar(2000) Null,
		Primary Key ([PropertyId]))

	;With [Choice] As (
		Select	[PropertyId],
				String_Agg(NullIf(Trim(L.[value]),''),', ') Within Group (Order By L.[value]) As [ChoiceList]
		From	@Data D
				Cross Apply String_Split(D.[PropertyData],',') L
		Where	[PropertyType] In ('List')
		Group By D.[PropertyId])
	Insert Into @Values
	Select	X.[PropertyId],
			NullIf(Trim(D.[PropertyTitle]),'') As [PropertyTitle],
			NullIf(Trim(D.[PropertyDescription]),'') As [PropertyDescription],
			NullIf(Trim(D.[PropertyType]),'') As [PropertyType],
			IsNull(C.[ChoiceList], D.[PropertyData]) As [PropertyData]
	From	@Data D
			Cross apply (
				Select	Coalesce(D.[PropertyId], @PropertyId, NewId()) As [PropertyId]) X
			Left Join [Choice] C
			On	D.[PropertyId] = C.[PropertyId]

	-- Apply Changes
	Delete From [App_DataDictionary].[DomainProperty]
	From	[App_DataDictionary].[DomainProperty] T
			Left Join @Values S
			On	T.[PropertyId] = S.[PropertyId]
	Where	@PropertyId = T.[PropertyId] or
			(@PropertyId is Null And S.[PropertyId] is Null)
	Print FormatMessage ('Delete [App_DataDictionary].[DomainProperty]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[PropertyId],
				[PropertyTitle],
				[PropertyDescription],
				[PropertyType],
				[PropertyData]
		From	@Values
		Except
		Select	[PropertyId],
				[PropertyTitle],
				[PropertyDescription],
				[PropertyType],
				[PropertyData]
		From	[App_DataDictionary].[DomainProperty])
	Update [App_DataDictionary].[DomainProperty]
	Set		[PropertyTitle] = S.[PropertyTitle],
			[PropertyDescription] = S.[PropertyDescription],
			[PropertyType] = S.[PropertyType],
			[PropertyData] = S.[PropertyData]
	From	[App_DataDictionary].[DomainProperty] T
			Inner Join [Delta] S
			On	T.[PropertyId] = S.[PropertyId]
	Print FormatMessage ('Update [App_DataDictionary].[DomainProperty]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DomainProperty] (
			[PropertyId],
			[PropertyTitle],
			[PropertyDescription],
			[PropertyType],
			[PropertyData])
	Select	S.[PropertyId],
			S.[PropertyTitle],
			S.[PropertyDescription],
			S.[PropertyType],
			S.[PropertyData]
	From	@Values S
			Left Join [App_DataDictionary].[DomainProperty] T
			On	S.[PropertyId] = T.[PropertyId]
	Where	T.[PropertyId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DomainProperty]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
/*
Begin Try;
	Begin Transaction;
	Set NoCount On;

	Declare @Data [App_DataDictionary].[typeDomainProperty]

	Insert Into @Data Values (
		'00000000-0000-0000-0010-000000000010',
		'MS Description',
		'Commonly used by Microsoft tools to store the user defined Description of the element.',
		'MS_Description', 'MS_Description')
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

	Exec [App_DataDictionary].[procSetDomainProperty] @Data = @Data

	Select	*
	From	[App_DataDictionary].[DomainProperty]

	-- By default, throw and error and exit without committing
;	Throw 50000, 'Abort process, comment out this line when ready to actual Commit the transaction',255;
	
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