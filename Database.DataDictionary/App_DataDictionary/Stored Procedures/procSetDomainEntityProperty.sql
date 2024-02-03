CREATE PROCEDURE [App_DataDictionary].[procSetDomainEntityProperty]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeDomainEntityProperty] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DomainEntityProperty.
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
	If @ModelId is Null and @EntityId is Null
	Throw 50000, '@ModelId or @EntityId must be specified', 1;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[EntityId]		UniqueIdentifier Not Null,
		[PropertyId]		UniqueIdentifier NOT Null,
		[PropertyValue]		NVarChar(4000) Null,
		[DefinitionText]    NVarChar(Max) Null,
		Primary Key ([EntityId],[PropertyId]))

	Insert Into @Values
	Select	[EntityId],
			[PropertyId],
			[PropertyValue],
			[DefinitionText]
	From	@Values
	Select	D.[EntityId],
			D.[PropertyId],
			NullIf(Trim(D.[PropertyValue]),'') As [PropertyValue],
			NullIf(Trim(D.[DefinitionText]),'') As [DefinitionText]
	From	@Data D

	-- Apply Changes
	Delete From [App_DataDictionary].[DomainEntityProperty]
	From	[App_DataDictionary].[DomainEntityProperty] T
			Left Join @Values S
			On	T.[EntityId] = S.[EntityId] And
				T.[PropertyId] = S.[PropertyId]
	Where	S.[EntityId] is Null And
			T.[EntityId] In (
			Select	A.[EntityId]
			From	[App_DataDictionary].[DomainEntity] A
					Left Join [App_DataDictionary].[ModelEntity] C
					On	A.[EntityId] = C.[EntityId]
			Where	(@EntityId is Null Or @EntityId = A.[EntityId]) And
					(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DomainEntityProperty]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[EntityId],
				[PropertyId],
				[PropertyValue],
				[DefinitionText]
		From	@Values
		Except 
		Select	[EntityId],
				[PropertyId],
				[PropertyValue],
				[DefinitionText]
		From	[App_DataDictionary].[DomainEntityProperty])
	Update [App_DataDictionary].[DomainEntityProperty]
	Set		[PropertyValue] = S.[PropertyValue],
			[DefinitionText] = S.[DefinitionText]
	From	[App_DataDictionary].[DomainEntityProperty] T
			Inner Join [Delta] S
			On	T.[EntityId] = S.[EntityId] And
				T.[PropertyId] = S.[PropertyId]
	Print FormatMessage ('Update [App_DataDictionary].[DomainEntityProperty]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into  [App_DataDictionary].[DomainEntityProperty] (
			[EntityId],
			[PropertyId],
			[PropertyValue],
			[DefinitionText])
	Select	S.[EntityId],
			S.[PropertyId],
			S.[PropertyValue],
			S.[DefinitionText]
	From	@Values S
			Left Join [App_DataDictionary].[DomainEntityProperty] T
			On	S.[EntityId] = T.[EntityId] And
				S.[PropertyId] = T.[PropertyId]
	Where	T.[EntityId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DomainEntityProperty]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));
	
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
