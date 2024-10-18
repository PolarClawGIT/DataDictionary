CREATE PROCEDURE [AppSecurity].[procSetPrincipal]
		@PrincipalId UniqueIdentifier = Null,
		@Data [AppSecurity].[typePrincipal] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on Security Principal.
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
			[PrincipalId] UniqueIdentifier Not Null,
			[PrincipalLogin] SysName Not Null,
			[PrincipalName] [App_DataDictionary].[typeTitle] Not Null,
			[PrincipalAnnotation] [App_DataDictionary].[typeDescription] Null,
			Primary Key ([PrincipalId]))

	Insert Into @Values
	Select	Coalesce(P.[PrincipalId], D.[PrincipalId], @PrincipalId, NewId()) As [PrincipalId],
			NullIf(Trim(D.[PrincipalLogin]),'') As [PrincipalLogin],
			NullIf(Coalesce(Trim(D.[PrincipalName]),Trim(D.[PrincipalLogin])),'') As [PrincipalName],
			NullIf(Trim(D.[PrincipalAnnotation]),'') As [PrincipalAnnotation]
	From	@Data D
			Left Join [AppSecurity].[Principal] P
			On	D.[PrincipalLogin] = P.[PrincipalLogin]
	Where	(@PrincipalId is Null or @PrincipalId = D.[PrincipalId])

	-- Apply Changes
	Delete From [AppSecurity].[ObjectOwner]
	From	[AppSecurity].[ObjectOwner] T
			Left Join @Values S
			On	T.[PrincipalId] = S.[PrincipalId]
	Where	S.[PrincipalId] is Null And
			(@PrincipalId is Null or @PrincipalId = T.[PrincipalId])
	Print FormatMessage ('Delete [AppSecurity].[ObjectOwner] (Principal): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppSecurity].[RoleMembership]
	From	[AppSecurity].[RoleMembership] T
			Left Join @Values S
			On	T.[PrincipalId] = S.[PrincipalId]
	Where	S.[PrincipalId] is Null And
			(@PrincipalId is Null or @PrincipalId = T.[PrincipalId])
	Print FormatMessage ('Delete [AppSecurity].[RoleMembership] (Principal): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppSecurity].[Principal]
	From	[AppSecurity].[Principal] T
			Left Join @Values S
			On	T.[PrincipalId] = S.[PrincipalId]
	Where	S.[PrincipalId] is Null And
			(@PrincipalId is Null or @PrincipalId = T.[PrincipalId])
	Print FormatMessage ('Delete [AppSecurity].[Principal]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[PrincipalId],
				[PrincipalLogin],
				[PrincipalName],
				[PrincipalAnnotation]
		From	@Values
		Except
		Select	[PrincipalId],
				[PrincipalLogin],
				[PrincipalName],
				[PrincipalAnnotation]
		From	[AppSecurity].[Principal])
	Update [AppSecurity].[Principal]
	Set		[PrincipalLogin] = S.[PrincipalLogin],
			[PrincipalName] = S.[PrincipalName],
			[PrincipalAnnotation] = S.[PrincipalAnnotation]
	From	[AppSecurity].[Principal] T
			Inner Join [Delta] S
			On	T.[PrincipalId] = S.[PrincipalId]
	Print FormatMessage ('Update [AppSecurity].[Principal]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppSecurity].[Principal] (
			[PrincipalId],
			[PrincipalLogin],
			[PrincipalName],
			[PrincipalAnnotation])
	Select	S.[PrincipalId],
			S.[PrincipalLogin],
			S.[PrincipalName],
			S.[PrincipalAnnotation]
	From	@Values S
			Left Join [AppSecurity].[Principal] T
			On	S.[PrincipalId] = T.[PrincipalId]
	Where	T.[PrincipalId] is Null
	Print FormatMessage ('Insert [AppSecurity].[Principal]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
