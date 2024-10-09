CREATE PROCEDURE [AppSecurity].[procSetSecurityRole]
		@RoleId UniqueIdentifier = Null,
		@Data [AppSecurity].[typeSecurityRole] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on SecurityRole.
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
			[RoleId] UniqueIdentifier Not Null,
			[RoleName] [App_DataDictionary].[typeTitle] Not Null,
			[RoleDescription] [App_DataDictionary].[typeDescription] Null,
			[IsSecurityAdmin] Bit Not Null,
			[IsHelpAdmin]     Bit Not Null,
			[IsHelpOwner]     Bit Not Null,
			[IsCatalogAdmin]  Bit Not Null,
			[IsCatalogOwner]  Bit Not Null,
			[IsLibraryAdmin]  Bit Not Null,
			[IsLibraryOwner]  Bit Not Null,
			[IsModelAdmin]    Bit Not Null,
			[IsModelOwner]    Bit Not Null,
			[IsScriptAdmin]   Bit Not Null,
			[IsScriptOwner]   Bit Not Null
			Primary Key ([RoleId]))

	Insert Into @Values
	Select	Coalesce(R.[RoleId], D.[RoleId], @RoleId, NewId()) As [RoleId],
			NullIf(Trim(D.[RoleName]),'') As [RoleName],
			NullIf(Trim(D.[RoleDescription]),'') As [RoleDescription],
			IsNull(D.[IsSecurityAdmin],0) As [IsSecurityAdmin],
			IsNull(D.[IsHelpAdmin],0) As [IsSecurityAdmin],
			IsNull(D.[IsHelpOwner],0) As [IsSecurityAdmin],
			IsNull(D.[IsCatalogAdmin],0) As [IsCatalogAdmin],
			IsNull(D.[IsCatalogOwner],0) As [IsCatalogOwner],
			IsNull(D.[IsLibraryAdmin],0) As [IsLibraryAdmin],
			IsNull(D.[IsLibraryOwner],0) As [IsLibraryOwner],
			IsNull(D.[IsModelAdmin],0) As [IsModelAdmin],
			IsNull(D.[IsModelOwner],0) As [IsModelOwner],
			IsNull(D.[IsScriptAdmin],0) As [IsScriptAdmin],
			IsNull(D.[IsScriptOwner],0) As [IsScriptOwner]
	From	@Data D
			Left Join [AppSecurity].[SecurityRole] R
			On	D.[RoleName] = R.[RoleName]
	Where	(@RoleId is Null Or D.[RoleId] = @RoleId)

	-- Apply Changes
	Delete From [AppSecurity].[SecurityMembership]
	From	[AppSecurity].[SecurityMembership] T
			Left Join @Values S
			On	T.[RoleId] = S.[RoleId]
	Where	S.[RoleId] is Null And
			(@RoleId is Null or @RoleId = T.[RoleId])
	Print FormatMessage ('Delete [AppSecurity].[SecurityMembership] (Role): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppSecurity].[SecurityRole]
	From	[AppSecurity].[SecurityRole] T
			Left Join @Values S
			On	T.[RoleId] = S.[RoleId]
	Where	S.[RoleId] is Null And
			(@RoleId is Null or @RoleId = T.[RoleId])
	Print FormatMessage ('Delete [AppSecurity].[SecurityRole]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[RoleId],
				[RoleName],
				[RoleDescription],
				[IsSecurityAdmin],
				[IsHelpAdmin],
				[IsHelpOwner],
				[IsCatalogAdmin],
				[IsCatalogOwner],
				[IsLibraryAdmin],
				[IsLibraryOwner],
				[IsModelAdmin],
				[IsModelOwner],
				[IsScriptAdmin],
				[IsScriptOwner]
		From	@Values
		Except
		Select	[RoleId],
				[RoleName],
				[RoleDescription],
				[IsSecurityAdmin],
				[IsHelpAdmin],
				[IsHelpOwner],
				[IsCatalogAdmin],
				[IsCatalogOwner],
				[IsLibraryAdmin],
				[IsLibraryOwner],
				[IsModelAdmin],
				[IsModelOwner],
				[IsScriptAdmin],
				[IsScriptOwner]
		From	[AppSecurity].[SecurityRole])
	Update [AppSecurity].[SecurityRole]
	Set		[RoleName] = S.[RoleName],
			[RoleDescription] = S.[RoleDescription],
			[IsSecurityAdmin] = S.[IsSecurityAdmin],
			[IsHelpAdmin] = S.[IsHelpAdmin],
			[IsHelpOwner] = S.[IsHelpOwner],
			[IsCatalogAdmin] = S.[IsCatalogAdmin],
			[IsCatalogOwner] = S.[IsCatalogOwner],
			[IsLibraryAdmin] = S.[IsLibraryAdmin],
			[IsLibraryOwner] = S.[IsLibraryOwner],
			[IsModelAdmin] = S.[IsModelAdmin],
			[IsModelOwner] = S.[IsModelOwner],
			[IsScriptAdmin] = S.[IsScriptAdmin],
			[IsScriptOwner] = S.[IsScriptOwner]
	From	[AppSecurity].[SecurityRole] T
			Inner Join [Delta] S
			On	T.[RoleId] = S.[RoleId]
	Print FormatMessage ('Update [AppSecurity].[SecurityRole]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppSecurity].[SecurityRole] (
			[RoleId],
			[RoleName],
			[RoleDescription],
			[IsSecurityAdmin],
			[IsHelpAdmin],
			[IsHelpOwner],
			[IsCatalogAdmin],
			[IsCatalogOwner],
			[IsLibraryAdmin],
			[IsLibraryOwner],
			[IsModelAdmin],
			[IsModelOwner],
			[IsScriptAdmin],
			[IsScriptOwner])
	Select	S.[RoleId],
			S.[RoleName],
			S.[RoleDescription],
			S.[IsSecurityAdmin],
			S.[IsHelpAdmin],
			S.[IsHelpOwner],
			S.[IsCatalogAdmin],
			S.[IsCatalogOwner],
			S.[IsLibraryAdmin],
			S.[IsLibraryOwner],
			S.[IsModelAdmin],
			S.[IsModelOwner],
			S.[IsScriptAdmin],
			S.[IsScriptOwner]
	From	@Values S
			Left Join [AppSecurity].[SecurityRole] T
			On	S.[RoleId] = T.[RoleId]
	Where	T.[RoleId] is Null
	Print FormatMessage ('Insert [AppSecurity].[SecurityRole]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
