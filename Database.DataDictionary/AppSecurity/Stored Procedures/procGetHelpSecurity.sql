CREATE PROCEDURE [AppSecurity].[procGetHelpSecurity]
		@HelpId UniqueIdentifier = Null,
		@RoleId UniqueIdentifier = Null,
		@PrincipalId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on HelpSecurity.
*/
;With [Object] As (
	Select	T.[HelpId],
			T.[HelpSubject],
			Convert(Bit, IIF(
				(S.[IsGrant] = 1 And S.[IsDeny] = 0) Or
				S.[IsHelpAdmin] = 1 Or
				S.[IsOwner] = 1 ,1,0))
				As [AlterValue],
			Convert(Bit, IIF(
				S.[IsSecurityAdmin] = 1 Or
				S.[IsHelpAdmin] = 1 Or
				S.[IsOwner] = 1 ,1,0))
				As [AlterSecurity]
	From	[AppGeneral].[HelpSubject] T
			Cross Apply [AppSecurity].[funcAuthorization](T.[HelpId]) S)
Select	T.[HelpId],
		T.[HelpSubject],
		O.[PrincipalId] As [PrincipalId],
		Convert(UniqueIdentifier, Null) [RoleId],
		Convert(Bit, 1) As [IsGrant],
		Convert(Bit, 0) As [IsDeny],
		T.[AlterValue],
		T.[AlterSecurity]
From	[Object] T
		Left Join [AppSecurity].[ObjectOwner] O
		On	T.[HelpId] = O.[ObjectId]
Where	(@HelpId is Null Or T.[HelpId] = @HelpId) And
		(@PrincipalId is Null Or IsNull(O.[PrincipalId], @PrincipalId) = @PrincipalId)
Union
Select	T.[HelpId],
		T.[HelpSubject],
		Convert(UniqueIdentifier, Null) As [PrincipalId],
		P.[RoleId],
		P.[IsGrant],
		P.[IsDeny],
		T.[AlterValue],
		T.[AlterSecurity]
From	[Object] T
		Left Join [AppSecurity].[ObjectPermission] P
		On	T.[HelpId] = P.[ObjectId]
Where	(@HelpId is Null Or T.[HelpId] = @HelpId) And
		(@RoleId is Null Or IsNull(P.[RoleId], @RoleId) = @RoleId)
GO
