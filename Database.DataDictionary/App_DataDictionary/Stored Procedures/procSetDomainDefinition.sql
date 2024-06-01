CREATE PROCEDURE [App_DataDictionary].[procSetDomainDefinition]
		@ModelId UniqueIdentifier = Null,
		@DefinitionId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeDomainDefinition] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DomainDefinition.
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
		[DefinitionId]             UniqueIdentifier NOT NULL,
		[DefinitionTitle]          [App_DataDictionary].[typeTitle] Not Null,
		[DefinitionDescription]    [App_DataDictionary].[typeDescription] Null,
		Primary Key ([DefinitionId]))

	Insert Into @Values
	Select	X.[DefinitionId],
			NullIf(Trim(D.[DefinitionTitle]),'') As [DefinitionTitle],
			NullIf(Trim(D.[DefinitionDescription]),'') As [DefinitionDescription]
	From	@Data D
			Cross apply (
				Select	Coalesce(D.[DefinitionId], @DefinitionId, NewId()) As [DefinitionId]) X

	-- Apply Changes
	Delete From [App_DataDictionary].[ModelDefinition]
	From	@Values S
			Left Join [App_DataDictionary].[ModelDefinition] T
			On	S.[DefinitionId] = T.[DefinitionId]
	Where	@ModelId = T.[ModelId] And
			T.[DefinitionId] is Null
	Print FormatMessage ('Delete [App_DataDictionary].[ModelDefinition]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DomainDefinition]
	From	[App_DataDictionary].[DomainDefinition] T
			Left Join @Values S
			On	T.[DefinitionId] = S.[DefinitionId]
	Where	S.[DefinitionId] is Null And
			T.[IsCommon] = 0 And
			T.[DefinitionId] In (
				Select	A.[DefinitionId]
				From	[App_DataDictionary].[DomainDefinition] A
						Left Join [App_DataDictionary].[ModelDefinition] C
						On	A.[DefinitionId] = C.[DefinitionId]
				Where	(@DefinitionId is Null Or @DefinitionId = A.[DefinitionId]) And
						(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DomainDefinition]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[DefinitionId],
				[DefinitionTitle],
				[DefinitionDescription]
		From	@Values
		Except
		Select	[DefinitionId],
				[DefinitionTitle],
				[DefinitionDescription]
		From	[App_DataDictionary].[DomainDefinition])
	Update [App_DataDictionary].[DomainDefinition]
	Set		[DefinitionTitle] = S.[DefinitionTitle],
			[DefinitionDescription] = S.[DefinitionDescription]
	From	[App_DataDictionary].[DomainDefinition] T
			Inner Join [Delta] S
			On	T.[DefinitionId] = S.[DefinitionId]
	Where	T.[IsCommon] = 0 -- Common Definitions cannot be altered by this procedure
	Print FormatMessage ('Update [App_DataDictionary].[DomainDefinition]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DomainDefinition] (
			[DefinitionId],
			[DefinitionTitle],
			[DefinitionDescription])
	Select	S.[DefinitionId],
			S.[DefinitionTitle],
			S.[DefinitionDescription]
	From	@Values S
			Left Join [App_DataDictionary].[DomainDefinition] T
			On	S.[DefinitionId] = T.[DefinitionId]
	Where	T.[DefinitionId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DomainDefinition]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ModelDefinition] (
			[ModelId],
			[DefinitionId])
	Select	@ModelId,
			S.[DefinitionId]
	From	@Values S
			Left Join [App_DataDictionary].[ModelDefinition] T
			On	@ModelId = T.[ModelId] And
				S.[DefinitionId] = T.[DefinitionId]
	Where	T.[DefinitionId] is Null And
			@ModelId is Not Null
	Print FormatMessage ('Insert [App_DataDictionary].[ModelDefinition]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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

	Declare @Data [App_DataDictionary].[typeDomainDefinition]

	Insert Into @Data Values (
		'00000000-0000-0000-0010-000000000020',
		'Business Definition',
		'Definition of the item in Business Terms.')
	Insert Into @Data Values (
		'00000000-0000-0000-0020-000000000020',
		'Technical Definition',
		'Definition of the item in Technical Terms.')

	Exec [App_DataDictionary].[procSetDomainDefinition] @Data = @Data

	update [App_DataDictionary].[DomainDefinition]
	Set		[IsCommon] = 1

	Select	*
	From	[App_DataDictionary].[DomainDefinition]

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