CREATE PROCEDURE [AppSecurity].[procGetSecurablePermission]
		@SecurableId UniqueIdentifier = Null,
		@RoleId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on Securable Permission (Role).
*/
Select	O.[RoleId],
		O.[SecurableId],
		T.[SecurableTitle],
		O.[IsGrant],
		O.[IsDeny],
		Convert(Bit, IIF(
				S.[IsSecurityAdmin] = 1 Or
				S.[IsOwner] = 1 ,1,0))
				As [AlterValue],
		Convert(Bit, IIF(
				S.[IsSecurityAdmin] = 1 Or
				S.[IsOwner] = 1 ,1,0))
				As [AlterSecurity]
From	[AppSecurity].[SecurablePermission] O
		Inner Join [AppSecurity].[Securable] T
		On	O.[SecurableId] = T.[SecurableId]
		Cross Apply [AppSecurity].[funcAuthorization](O.[SecurableId]) S
Where	(@SecurableId is Null Or O.[SecurableId] = @SecurableId) And
		(@RoleId is Null Or O.[RoleId] = @RoleId)
GO