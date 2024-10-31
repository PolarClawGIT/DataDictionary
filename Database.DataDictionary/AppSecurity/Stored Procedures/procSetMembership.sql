CREATE PROCEDURE [AppSecurity].[procSetRoleMembership]
		@RoleId UniqueIdentifier = Null,
		@PrincipalId UniqueIdentifier = Null,
		@Data [AppSecurity].[typeRoleMembership] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on Security Membership.
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
		[RoleId]          UniqueIdentifier Not Null,
		[PrincipalId]     UniqueIdentifier Not Null,
		Primary Key ([RoleId], [PrincipalId]))

	Insert Into @Values
	Select	D.[RoleId],
			D.[PrincipalId]
	From	@Data D
			Inner Join [AppSecurity].[Role] R
			On	D.[RoleId] = R.[RoleId]
			Inner Join [AppSecurity].[Principal] P
			On	D.[PrincipalId] = P.[PrincipalId]
	Where	(@RoleId is Null Or @RoleId = D.[RoleId]) And
			(@PrincipalId is Null Or @PrincipalId = D.[PrincipalId])

	-- Apply Changes
	Delete From [AppSecurity].[RoleMembership]
	From	[AppSecurity].[RoleMembership] T
			Left Join @Values S
			On	T.[RoleId] = S.[RoleId] And
				T.[PrincipalId] = S.[PrincipalId]
	Where	S.[RoleId] is Null And
			(@RoleId is Null Or @RoleId = T.[RoleId]) And
			(@PrincipalId is Null Or @PrincipalId = T.[PrincipalId])
	Print FormatMessage ('Delete [AppSecurity].[SecurityMembership]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppSecurity].[RoleMembership] (
			[RoleId],
			[PrincipalId])
	Select	S.[RoleId],
			S.[PrincipalId]
	From	@Values S
			Left Join [AppSecurity].[RoleMembership] T
			On	S.[RoleId] = T.[RoleId] And
				S.[PrincipalId] = T.[PrincipalId]
	Where	T.[RoleId] is Null
	Print FormatMessage ('Insert [AppSecurity].[SecurityMembership]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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