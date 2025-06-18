CREATE PROCEDURE [AppModel].[procSetAttributeDefinition]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@Data [AppModel].[typeAttributeDefinition] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on Model AttributeDefinition.
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
		From	@Data D
				Cross Apply [AppSecurity].[funcModelAttributeAuthorization](@ModelId, [AttributeId], 0))
	Throw 601020, 'Model Not Authorized', 2;;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[AttributeId]		UniqueIdentifier Not Null,
		[DefinitionId]		UniqueIdentifier Not Null,
		[DefinitionSummary]	[AppGeneral].[typeDescription] Null,
		[DefinitionText]	[AppModel].[typeRichText] Null,
		Primary Key ([AttributeId], [DefinitionId]))

	Insert Into @Values
	Select	D.[AttributeId],
			D.[DefinitionId],
			NullIf(Trim(D.[DefinitionSummary]),'') As [DefinitionSummary],
			Case
				When D.[DefinitionText] Like '{\rtf1\ansi%'
					Then D.[DefinitionText]
				When D.[DefinitionText] is not null And
					D.[DefinitionText] Not Like '{\rtf1\ansi%'
					Then FormatMessage('{\rtf1\ansi %s}', D.[DefinitionText])
				When NullIf(Trim(D.[DefinitionText]),'') is Null And 
					NullIf(Trim(D.[DefinitionSummary]),'') is not null
					Then FormatMessage('{\rtf1\ansi %s}', Trim(D.[DefinitionSummary]))
				Else Null
				End As [DefinitionText]
	From	@Data D
			Inner Join [AppModel].[DefinitionEnumeration] R
			On	D.[DefinitionId] = R.[DefinitionId]
	Where	(@AttributeId is Null Or @AttributeId = D.[AttributeId]) And
			(@ModelId is Null Or D.[AttributeId] In (
				Select	[AttributeId]
				From	[AppModel].[ModelAttribute]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Apply Changes
	Delete From [AppModel].[AttributeDefinition]
	From	[AppModel].[AttributeDefinition] T
			Left Join @Values V
			On	T.[AttributeId] = V.[AttributeId] And
				T.[DefinitionId] = V.[DefinitionId]
			Cross Apply [AppSecurity].[funcModelAttributeAuthorization](@ModelId, T.[AttributeId], 1)
	Where	V.[AttributeId] is Null And
			(@AttributeId is Not Null Or @ModelId is Not Null) And
			(@AttributeId is Null Or @AttributeId = T.[AttributeId]) And
			(@ModelId is Null Or T.[AttributeId] In (
				Select	[AttributeId]
				From	[AppModel].[ModelAttribute]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppModel].[AttributeDefinition]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[AttributeId],
				[DefinitionId],
				[DefinitionSummary],
				[DefinitionText]
		From	@Values
		Except
		Select	[AttributeId],
				[DefinitionId],
				[DefinitionSummary],
				[DefinitionText]
		From	[AppModel].[AttributeDefinition])
	Update [AppModel].[AttributeDefinition]
	Set		[DefinitionSummary] = S.[DefinitionSummary],
			[DefinitionText] = S.[DefinitionText]
	From	[Delta] S
			Inner Join [AppModel].[AttributeDefinition] T
			On	S.[AttributeId] = T.[AttributeId] And
				S.[DefinitionId] = T.[DefinitionId]
			Cross Apply [AppSecurity].[funcModelAttributeAuthorization](@ModelId, S.[AttributeId], 1)
	Print FormatMessage ('Update [AppModel].[AttributeDefinition]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[AttributeDefinition] (
			[AttributeId],
			[DefinitionId],
			[DefinitionSummary],
			[DefinitionText])
	Select	S.[AttributeId],
			S.[DefinitionId],
			S.[DefinitionSummary],
			S.[DefinitionText]
	From	@Values S
			Left Join [AppModel].[AttributeDefinition] T
			On	S.[AttributeId] = T.[AttributeId] And
				S.[DefinitionId] = T.[DefinitionId]
			Cross Apply [AppSecurity].[funcModelAttributeAuthorization](@ModelId, S.[AttributeId], 1)
	Where	T.[AttributeId] is Null
	Print FormatMessage ('Insert [AppModel].[AttributeDefinition]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[ModelDefinition] (
		[ModelId],
		[DefinitionId])
	Select	@ModelId,
			S.[DefinitionId]
	From	@Values S
			Left Join [AppModel].[ModelDefinition] T
			On	S.[DefinitionId] = T.[DefinitionId] And
				[ModelId] = @ModelId
			Cross Apply [AppSecurity].[funcModelAuthorization](@ModelId, 1)
	Where	T.[DefinitionId] is Null
	Group By S.[DefinitionId]
	Print FormatMessage ('Insert [AppModel].[ModelDefinition]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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

	Declare	@ModelId UniqueIdentifier = Null,
			@AttributeId UniqueIdentifier = Null,
			@Data [AppModel].[typeAttributeDefinition]

	Set	@ModelId = (Select [ModelId] From [AppModel].[Model] Where [ModelTitle] = 'Unit Test')

	Insert Into @Data
	Exec [AppModel].[procGetAttributeDefinition] @ModelId = @ModelId, @AttributeId = @AttributeId

	Select	'@Data', *
	From	@Data

	--Delete From @Data
	--Set @AttributeId = (Select Top 1 [AttributeId] From @Data); Delete From @Data Where [AttributeId] = @AttributeId

	--Set @AttributeId = (Select Top 1 [AttributeId] From @Data); 
	--Insert Into [AppModel].[ModelAttribute] ([ModelId], [AttributeId])
	--Select Top 1 [ModelId], @AttributeId From [AppModel].[Model] Where [ModelId] <> @ModelId
	--Delete From @Data Where [AttributeId] = @AttributeId

	--Insert Into [AppModel].[ModelAttribute] ([ModelId], [AttributeId])
	--Select Top 1 [ModelId], (Select Top 1 [AttributeId] From @Data) From [AppModel].[Model] Where [ModelId] <> @ModelId
	

	Exec [AppModel].[procSetAttributeDefinition]  @ModelId = @ModelId, @AttributeId = @AttributeId, @Data = @Data

	--Exec [AppModel].[procGetAttributeDefinition] @ModelId = @ModelId

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
	--Throw;
End Catch;
*/