CREATE PROCEDURE [AppSecurity].[procSetHelpSecurity]
		@HelpId UniqueIdentifier = Null,
		@RoleId UniqueIdentifier = Null,
		@PrincipalId UniqueIdentifier = Null,
		@Data [AppSecurity].[typeHelpSecurity] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on HelpSecurity.
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

	Declare @RoleValue [AppSecurity].[typeObjectPermission]
	Declare @OwnerValue [AppSecurity].[typeObjectOwner]

	Insert Into @RoleValue (
			[RoleId],
			[ObjectId],
			[IsGrant],
			[IsDeny])
	Select	D.[RoleId],
			D.[HelpId] As [ObjectId],
			D.[IsGrant],
			D.[IsDeny]
	From	@Data D
			Inner Join [AppGeneral].[HelpSubject] T
			On	D.[HelpId] = T.[HelpId]
			Inner Join [AppSecurity].[Role] R
			On	D.[RoleId] = R.[RoleId]
	Where	(@HelpId is Null Or T.[HelpId] = @HelpId) Or
			(@RoleId is Null Or R.[RoleId] = @RoleId)
	Union	-- Everything that is not Help Subjects (or it will be deleted)
	Select	O.[RoleId],
			O.[ObjectId],
			O.[IsGrant],
			O.[IsDeny]
	From	[AppSecurity].[ObjectPermission] O
			Left Join [AppGeneral].[HelpSubject] T
			On	O.[ObjectId] = T.[HelpId]
	Where	T.[HelpId] is Null And
			(@HelpId is Null Or O.[ObjectId] = @HelpId) Or
			(@RoleId is Null Or O.[RoleId] = @RoleId)

	Insert Into @OwnerValue (
			[PrincipalId],
			[ObjectId])
	Select	D.[PrincipalId],
			D.[HelpId] As [ObjectId]
	From	@Data D
			Inner Join [AppGeneral].[HelpSubject] T
			On	D.[HelpId] = T.[HelpId]
			Inner Join [AppSecurity].[Principal] P
			On	D.[PrincipalId] = P.[PrincipalId]
	Where	(@HelpId is Null Or T.[HelpId] = @HelpId) Or
			(@PrincipalId is Null Or P.[PrincipalId] = @PrincipalId)
	Union	-- Everything that is not a Help Subject (or it will be deleted)
	Select	O.[PrincipalId],
			O.[ObjectId]
	From	[AppSecurity].[ObjectOwner] O
			Left Join [AppGeneral].[HelpSubject] T
			On	O.[ObjectId] = T.[HelpId]
	Where	T.[HelpId] is Null And
			(@HelpId is Null Or O.[ObjectId] = @HelpId) Or
			(@PrincipalId is Null Or O.[PrincipalId] = @PrincipalId)

	Exec [AppSecurity].[procSetObjectOwner]
			@PrincipalId = @PrincipalId,
			@ObjectId = @HelpId,
			@Data = @OwnerValue

	Exec [AppSecurity].[procSetObjectPermission]
			@RoleId = @RoleId,
			@ObjectId = @HelpId,
			@Data = @RoleValue

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