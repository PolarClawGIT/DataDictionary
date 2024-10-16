﻿CREATE PROCEDURE [App_General].[procSetApplicationHelp]
		@HelpId UniqueIdentifier = null,
		@Data [App_General].[typeApplicationHelp] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ApplicationHelp.
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
			[HelpId] UniqueIdentifier Not Null,
			[HelpSubject] NVarChar(100) Not Null,
			[HelpToolTip] NVarChar(500) Null,
			[HelpText] NVarChar(Max) Not Null,
			[NameSpace] NVarChar(1023) Null,
			Primary Key ([HelpId]))

	Insert Into @Values
	Select	Coalesce(D.[HelpId], @HelpId, NewId()) As [HelpId],
			NullIf(Trim(D.[HelpSubject]),'') As [HelpSubject],
			NullIf(Trim(D.[HelpToolTip]),'') As [HelpToolTip],
			NullIf(Trim(D.[HelpText]),'') As [HelpText],
			NullIf(Trim(D.[NameSpace]),'') As [NameSpace]
	From	@Data D
	Where	(@HelpId is Null or @HelpId = D.[HelpId])

	-- Apply Changes
	Delete From [App_General].[ApplicationHelp]
	From	[App_General].[ApplicationHelp] T
			Left Join @Values S
			On	T.[HelpId] = S.[HelpId]
	Where	S.[HelpId] is Null And
			T.[HelpId] In (
				Select	[HelpId]
				From	[App_General].[ApplicationHelp]
				Where	(@HelpId is Null Or @HelpId = [HelpId]))
	Print FormatMessage ('Delete [App_General].[ApplicationHelp]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[HelpId],
				[HelpSubject],
				[HelpToolTip],
				[HelpText],
				[NameSpace]
		From	@Values
		Except
		Select	[HelpId],
				[HelpSubject],
				[HelpToolTip],
				[HelpText],
				[NameSpace]
		From	[App_General].[ApplicationHelp])
	Update [App_General].[ApplicationHelp]
	Set		[HelpSubject] = S.[HelpSubject],
			[HelpToolTip] = S.[HelpToolTip],
			[HelpText] = S.[HelpText],
			[NameSpace] = S.[NameSpace]
	From	[App_General].[ApplicationHelp] T
			Inner Join [Delta] S
			On	T.[HelpId] = S.[HelpId]
	Print FormatMessage ('Update [App_General].[ApplicationHelp]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_General].[ApplicationHelp] (
			[HelpId],
			[HelpSubject],
			[HelpToolTip],
			[HelpText],
			[NameSpace])
	Select	S.[HelpId],
			S.[HelpSubject],
			S.[HelpToolTip],
			S.[HelpText],
			S.[NameSpace]
	From	@Values S
			Left Join [App_General].[ApplicationHelp] T
			On	S.[HelpId] = T.[HelpId]
	Where	T.[HelpId] is Null
	Print FormatMessage ('Insert [App_General].[ApplicationHelp]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
