﻿CREATE PROCEDURE [App_DataDictionary].[procSetDomainAttribute]
		@ModelId UniqueIdentifier,
		@Data [App_DataDictionary].[typeDomainAttribute] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DomainAttribute.
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
	Declare @Values [App_DataDictionary].[typeDomainAttribute]
	Insert Into @Values
	Select	IsNull(D.[AttributeId],NewId()) As [AttributeId],
			D.[SubjectAreaId],
			NullIf(Trim(D.[AttributeTitle]),'') As [AttributeTitle],
			NullIf(Trim(D.[AttributeDescription]),'') As [AttributeDescription]
	From	@Data D

	-- Validation
	If Not Exists (Select 1 From [App_DataDictionary].[Model] Where [ModelId] = @ModelId)
	Throw 50000, '[ModelId] could not be found that matched the parameter', 1;

	If Exists (
		Select	[AttributeId]
		From	@Values
		Group By [AttributeId]
		Having Count(*) > 1)
	Throw 50000, '[AttributeId] cannot be duplicate', 3;

	-- Apply Changes
	-- Note: Merge statement can throw errors with FK and UK constraints.
	With [Data] As (
		Select	D.[AttributeId],
				D.[AttributeTitle],
				D.[AttributeDescription]
		From	@Values D
				Inner Join [App_DataDictionary].[DomainAttribute] A
				On	D.[AttributeId] = A.[AttributeId]),
	[Delta] As (
		Select	[AttributeId],
				[AttributeTitle],
				[AttributeDescription]
		From	[Data]
		Except
		Select	[AttributeId],
				[AttributeTitle],
				[AttributeDescription]
		From	[App_DataDictionary].[DomainAttribute])
	Update [App_DataDictionary].[DomainAttribute]
	Set		[AttributeTitle] = S.[AttributeTitle],
			[AttributeDescription] = S.[AttributeDescription]
	From	[Delta] S
			Inner Join [App_DataDictionary].[DomainAttribute] T
			On	S.[AttributeId] = T.[AttributeId];
	Print FormatMessage ('Update [App_DataDictionary].[DomainAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DomainAttribute] (
			[AttributeId],
			[AttributeTitle],
			[AttributeDescription])
	Select	S.[AttributeId],
			S.[AttributeTitle],
			S.[AttributeDescription]
	From	@Values S
			Left Join [App_DataDictionary].[DomainAttribute] T
			On	S.[AttributeId] = T.[AttributeId]
	Where	T.[AttributeId] is Null;
	Print FormatMessage ('Insert [App_DataDictionary].[DomainAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	With [Data] As (
		Select	@ModelId As [ModelId],
				[AttributeId],
				[SubjectAreaId]
		From	@Values)
	Merge [App_DataDictionary].[ModelAttribute] As T
	Using [Data] As S
	On	T.[ModelId] = S.[ModelId] And
		T.[AttributeId] = S.[AttributeId]
	When Matched Then Update Set
		[SubjectAreaId] = S.[SubjectAreaId]
	When Not Matched by Target Then
		Insert ([ModelId], [AttributeId], [SubjectAreaId])
		Values ([ModelId], [AttributeId], [SubjectAreaId])
	When Not Matched by Source And T.[ModelId] = @ModelId Then Delete;
	Print FormatMessage ('Merge [App_DataDictionary].[ApplicationAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
-- Provide System Documentation
EXEC sp_addextendedproperty @name = N'MS_Description',
	@level0type = N'SCHEMA', @level0name = N'App_DataDictionary',
    @level1type = N'PROCEDURE', @level1name = N'procSetDomainAttribute',
	@value = N'Performs Set on DomainAttribute.'