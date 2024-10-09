CREATE PROCEDURE [AppSecurity].[procSetSecurityMembership]
		@RoleId UniqueIdentifier = Null,
		@PrincipleId UniqueIdentifier = Null,
		@Data [AppSecurity].[typeSecurityMembership] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on SecurityMembership.
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
		[PrincipleId]     UniqueIdentifier Not Null,
		Primary Key ([RoleId], [PrincipleId]))

	Insert Into @Values
	Select	D.[RoleId],
			D.[PrincipleId]
	From	@Data D
			Inner Join [AppSecurity].[SecurityRole] R
			On	D.[RoleId] = R.[RoleId]
			Inner Join [AppSecurity].[SecurityPrinciple] P
			On	D.[PrincipleId] = P.[PrincipleId]
	Where	(@RoleId is Null Or @RoleId = D.[RoleId]) And
			(@PrincipleId is Null Or @PrincipleId = D.[PrincipleId])

	-- Apply Changes
	Delete From [AppSecurity].[SecurityMembership]
	From	[AppSecurity].[SecurityMembership] T
			Left Join @Values S
			On	T.[RoleId] = S.[RoleId] And
				T.[PrincipleId] = S.[PrincipleId]
	Where	S.[RoleId] is Null And
			(@RoleId is Null Or @RoleId = T.[RoleId]) And
			(@PrincipleId is Null Or @PrincipleId = T.[PrincipleId])
	Print FormatMessage ('Delete [AppSecurity].[SecurityMembership]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppSecurity].[SecurityMembership] (
			[RoleId],
			[PrincipleId])
	Select	S.[RoleId],
			S.[PrincipleId]
	From	@Values S
			Left Join [AppSecurity].[SecurityMembership] T
			On	S.[RoleId] = T.[RoleId] And
				S.[PrincipleId] = T.[PrincipleId]
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