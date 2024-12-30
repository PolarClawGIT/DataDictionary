CREATE PROCEDURE [AppGeneral].[procSetHelpSubject]
		@HelpId UniqueIdentifier = null,
		@Data [AppGeneral].[typeHelpSubject] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on HelpSubject.
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

	If Exists (
		Select	1
		From	@Data D
		Where	[AppSecurity].[funcHelpSubjectAuthorization]([HelpId]) = 0)
	Throw 601020, 'Help Subject Not Authorized', 2;

	If @HelpId is Not Null And Exists (
		Select	1
		From	@Data D
		Where	IsNull([HelpId], @HelpId) <> @HelpId)
	Throw 602030, '@Data contains other Help Subjects', 3;

	-- Clean the Data
	Declare @Values Table (
			[HelpId] UniqueIdentifier Not Null,
			[HelpSubject] [App_DataDictionary].[typeTitle] Not Null,
			[HelpToolTip] [App_DataDictionary].[typeDescription] Null,
			[HelpText] NVarChar(Max) Not Null,
			[NameSpace] NVarChar(1023) Null,
			Primary Key ([HelpId]))

	Insert Into @Values
	Select	Coalesce(D.[HelpId], @HelpId, NewId()) As [HelpId],
			NullIf(Trim(D.[HelpSubject]),'') As [HelpSubject],
			NullIf(Trim(D.[HelpToolTip]),'') As [HelpToolTip],
			Case
				When D.[HelpText] Like '{\rtf1\ansi%'
					Then D.[HelpText]
				When D.[HelpText] is not null And
					D.[HelpText] Not Like '{\rtf1\ansi%'
					Then FormatMessage('{\rtf1\ansi %s}', D.[HelpText])
				When NullIf(Trim(D.[HelpText]),'') is Null And 
					NullIf(Trim(D.[HelpToolTip]),'') is not null
					Then FormatMessage('{\rtf1\ansi %s}', Trim(D.[HelpToolTip]))
				Else Null
				End As [HelpText],
			NullIf(Trim(S.[QualifiedName]),'') As [NameSpace]
	From	@Data D
			Outer Apply [AppModel].[funcParseName] (D.[NameSpace]) S
	Where	(@HelpId is Null or @HelpId = D.[HelpId]) And
			S.[IsBase] = 1

	-- Deal with Ownership, Sets up Row Level Security
	Insert Into [AppSecurity].[SecurableOwner] (
			[PrincipalId],
			[SecurableId])
	Select	S.[PrincipalId],
			V.[HelpId]
	From	@Values V
			Cross Apply [AppSecurity].[funcHelpSubjectAuthorization](V.[HelpId], 1) S
	Where	S.[IsHelpOwner] = 1 And
			S.[IsHelpAdmin] = 0 And
			S.[HasOwner] = 0
	Print FormatMessage ('Insert [AppSecurity].[SecurityOwner]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppGeneral].[HelpSubject]
	From	[AppGeneral].[HelpSubject] T
			Left Join @Values S
			On	T.[HelpId] = S.[HelpId]
			Cross Apply [AppSecurity].[funcHelpSubjectAuthorization](T.[HelpId], 1)
	Where	S.[HelpId] is Null And
			T.[HelpId] = @HelpId -- @HelpId must be specified
	Print FormatMessage ('Delete [AppGeneral].[HelpSubject]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
		From	[AppGeneral].[HelpSubject])
	Update [AppGeneral].[HelpSubject]
	Set		[HelpSubject] = S.[HelpSubject],
			[HelpToolTip] = S.[HelpToolTip],
			[HelpText] = S.[HelpText],
			[NameSpace] = S.[NameSpace]
	From	[AppGeneral].[HelpSubject] T
			Inner Join [Delta] S
			On	T.[HelpId] = S.[HelpId]
			Cross Apply [AppSecurity].[funcHelpSubjectAuthorization](T.[HelpId], 1)
	Print FormatMessage ('Update [AppGeneral].[HelpSubject]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppGeneral].[HelpSubject] (
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
			Left Join [AppGeneral].[HelpSubject] T
			On	S.[HelpId] = T.[HelpId]
			Cross Apply [AppSecurity].[funcHelpSubjectAuthorization](T.[HelpId], 1)
	Where	T.[HelpId] is Null
	Print FormatMessage ('Insert [AppGeneral].[HelpSubject]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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

	If ERROR_SEVERITY() Not In (0, 11) Exec [AppGeneral].[procThrowHelpSubject]
End Catch
GO
