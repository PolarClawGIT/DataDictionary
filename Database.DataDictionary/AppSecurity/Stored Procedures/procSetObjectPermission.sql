CREATE PROCEDURE [AppSecurity].[procSetObjectPermission]
		@RoleId UniqueIdentifier = Null,
		@ObjectId UniqueIdentifier = Null,
		@Data [AppSecurity].[typeObjectPermission] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ObjectPermission.
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
	Declare @Value Table (
		[RoleId] UniqueIdentifier Not Null,
		[ObjectId] UniqueIdentifier Not Null,
		[IsGrant] Bit Not Null,
		[IsDeny] Bit Not Null,
		Primary Key ([RoleId], [ObjectId]))

	Insert Into @Value
	Select	D.[RoleId],
			D.[ObjectId],
			D.[IsGrant],
			D.[IsDeny]
	From	@Data D
			Inner Join [AppSecurity].[Role] R
			On	D.[RoleId] = R.[RoleId]
	Where	(@ObjectId is Null Or D.[ObjectId] = @ObjectId) And
			(@RoleId is Null Or D.[RoleId] = @RoleId)

	-- Apply Changes
	Delete From [AppSecurity].[ObjectPermission]
	From	[AppSecurity].[ObjectPermission] T
			Left Join @Value S
			On	T.[RoleId] = S.[RoleId] And
				T.[ObjectId] = S.[ObjectId]
	Where	S.[ObjectId] is Null And
			(@ObjectId is Null Or T.[ObjectId] = @ObjectId) And
			(@RoleId is Null Or T.[RoleId] = @RoleId)
	Print FormatMessage ('Delete [AppSecurity].[ObjectPermission]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[RoleId],
				[ObjectId],
				[IsGrant],
				[IsDeny]
		From	@Value
		Except
		Select	[RoleId],
				[ObjectId],
				[IsGrant],
				[IsDeny]
		From	[AppSecurity].[ObjectPermission])
	Update [AppSecurity].[ObjectPermission]
	Set		[IsGrant] = S.[IsGrant],
			[IsDeny] = S.[IsDeny]
	From	[AppSecurity].[ObjectPermission] T
			Inner Join [Delta] S
			On	T.[RoleId] = S.[RoleId] And
				T.[ObjectId] = S.[ObjectId]
	Print FormatMessage ('Update [AppSecurity].[ObjectPermission]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppSecurity].[ObjectPermission] (
			[RoleId],
			[ObjectId],
			[IsGrant],
			[IsDeny])
	Select	S.[RoleId],
			S.[ObjectId],
			S.[IsGrant],
			S.[IsDeny]
	From	@Value S
			Left Join [AppSecurity].[ObjectPermission] T
			On	S.[RoleId] = T.[RoleId] And
				S.[ObjectId] = T.[ObjectId]
	Where	T.[RoleId] is Null
	Print FormatMessage ('Insert [AppSecurity].[ObjectPermission]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
