CREATE PROCEDURE [AppSecurity].[procSetHelpSecurity]
		@HelpId UniqueIdentifier = Null,
		@RoleId UniqueIdentifier = Null,
		@PrincipleId UniqueIdentifier = Null,
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

	  Declare @RoleValue Table (
		[RoleId] UniqueIdentifier Not Null,
		[ObjectId] UniqueIdentifier Not Null, -- HelpId
		[IsGrant] Bit Not Null,
		[IsDeny] Bit Not Null,
		Primary Key ([RoleId], [ObjectId]))

	  Declare @OwnerValue Table (
		[PrincipleId] UniqueIdentifier Not Null,
		[ObjectId] UniqueIdentifier Not Null, -- HelpId
		Primary Key ([PrincipleId], [ObjectId]))

	Insert Into @RoleValue
	Select	D.[RoleId],
			D.[HelpId] As [ObjectId],
			D.[IsGrant],
			D.[IsDeny]
	From	@Data D
			Inner Join [AppGeneral].[HelpSubject] H
			On	D.[HelpId] = H.[HelpId]
			Inner Join [AppSecurity].[Role] R
			On	D.[RoleId] = R.[RoleId]
	Where	(@HelpId is Null Or D.[HelpId] = @HelpId) And
			(@RoleId is Null Or D.[RoleId] = @RoleId)

	Insert Into @OwnerValue
	Select	D.[PrincipleId],
			D.[HelpId] As [ObjectId]
	From	@Data D
			Inner Join [AppSecurity].[Principle] P
			On	D.[PrincipleId] = P.[PrincipleId]
			Inner Join [AppGeneral].[HelpSubject] H
			On	D.[HelpId] = H.[HelpId]
	Where	(@HelpId is Null Or D.[HelpId] = @HelpId) And
			(@PrincipleId is Null Or D.[PrincipleId] = @PrincipleId)

	-- Apply Changes
	Delete From [AppSecurity].[ObjectPermission]
	From	[AppSecurity].[ObjectPermission] T
			Inner Join [AppGeneral].[HelpSubject] H
			On	T.[ObjectId] = H.[HelpId]
			Left Join @RoleValue S
			On	T.[RoleId] = S.[RoleId] And
				T.[ObjectId] = S.[ObjectId]
	Where	S.[ObjectId] is Null And
			(@HelpId is Null Or H.[HelpId] = @HelpId) And
			(@RoleId is Null Or T.[RoleId] = @RoleId)
	Print FormatMessage ('Delete [AppSecurity].[SecurityPermission] (HelpSubject): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From	[AppSecurity].[ObjectOwner]
	From	[AppSecurity].[ObjectOwner] T
			Inner Join [AppGeneral].[HelpSubject] H
			On	T.[ObjectId] = H.[HelpId]
			Left Join @OwnerValue S
			On	T.[PrincipleId] = S.[PrincipleId] And
				T.[ObjectId] = S.[ObjectId]
	Where	S.[ObjectId] is Null And
			(@HelpId is Null Or H.[HelpId] = @HelpId) And
			(@PrincipleId is Null Or T.[PrincipleId] = @PrincipleId)
	Print FormatMessage ('Delete [AppSecurity].[SecurityOwner] (HelpSubject): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[RoleId],
				[ObjectId],
				[IsGrant],
				[IsDeny]
		From	@RoleValue
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
	Print FormatMessage ('Update [AppSecurity].[SecurityPermission] (HelpSubject): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppSecurity].[ObjectPermission] (
			[RoleId],
			[ObjectId],
			[IsGrant],
			[IsDeny])
	Select	S.[RoleId],
			S.[ObjectId],
			S.[IsGrant],
			S.[IsDeny]
	From	@RoleValue S
			Left Join [AppSecurity].[ObjectPermission] T
			On	S.[RoleId] = T.[RoleId] And
				S.[ObjectId] = T.[ObjectId]
	Where	T.[RoleId] is Null
	Print FormatMessage ('Insert [AppSecurity].[SecurityPermission] (HelpSubject): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppSecurity].[ObjectOwner] (
			[PrincipleId],
			[ObjectId])
	Select	S.[PrincipleId],
			S.[ObjectId]
	From	@OwnerValue S
			Left Join [AppSecurity].[ObjectOwner] T
			On	S.[PrincipleId] = T.[PrincipleId] And
				S.[ObjectId] = S.[ObjectId]
	Where	T.[PrincipleId] is Null
	Print FormatMessage ('Insert [AppSecurity].[SecurityOwner] (HelpSubject): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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